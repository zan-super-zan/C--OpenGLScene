using OpenGLWrapper;
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace WinGL
{
    public abstract class Window : IDisposable
    {
        // Events for callbacks
        public event Action<int, int> OnResize;
        public event Action<KeyCode> OnKeyDown;
        public event Action<KeyCode> OnKeyUp;
        public event Action<MouseCode> OnMouseDown;
        public event Action<MouseCode> OnMouseUp;
        public event Action<int, int> OnMouseMove;
        public event Action OnRender;
        public event Action OnUpdate;
        public event Action OnInit;

        // Window properties
        public int Width { get; private set; }
        public int Height { get; private set; }
        public string Title { get; private set; }

        // Internal handles
        private IntPtr _hWnd;
        private IntPtr _hDC;
        private IntPtr _hGLRC;


        private Thread _messageThread;
        private bool _isRunning;

        private Win32.WndProc _wndProcDelegate;

        private ManualResetEvent _showEvent = new ManualResetEvent(false);

        public Window(int width, int height, string title)
        {
            Width = width;
            Height = height;
            Title = title;

            _messageThread = new Thread(CreateWindow)
            {
                IsBackground = true
            };
            _messageThread.Start();

            while (_hWnd == IntPtr.Zero)
                Thread.Sleep(1);
        }
        public void Show()
        {
            _isRunning = true;
            _showEvent.Set();
        }

        private void CreateWindow()
        {
            _wndProcDelegate = WindowProc;

            Win32.WNDCLASSEX wndClass = new Win32.WNDCLASSEX
            {
                cbSize = (uint)Marshal.SizeOf(typeof(Win32.WNDCLASSEX)),
                style = Win32.CS_OWNDC | Win32.CS_HREDRAW | Win32.CS_VREDRAW,
                lpfnWndProc = _wndProcDelegate,
                cbClsExtra = 0,
                cbWndExtra = 0,
                hInstance = Marshal.GetHINSTANCE(typeof(Window).Module),
                hIcon = IntPtr.Zero,
                hCursor = Win32.LoadCursor(IntPtr.Zero, Win32.IDC_ARROW),
                hbrBackground = (IntPtr)(Win32.COLOR_WINDOW + 1),
                lpszMenuName = null,
                lpszClassName = "CustomOpenGLWindowClass",
                hIconSm = IntPtr.Zero
            };

            ushort classAtom = Win32.RegisterClassEx(ref wndClass);
            if (classAtom == 0)
                throw new Exception("Failed to register window class.");

            _hWnd = Win32.CreateWindowEx(
                0,
                "CustomOpenGLWindowClass",
                Title,
                Win32.WS_OVERLAPPEDWINDOW | Win32.WS_VISIBLE,
                100,
                100,
                Width,
                Height,
                IntPtr.Zero,
                IntPtr.Zero,
                wndClass.hInstance,
                IntPtr.Zero
            );

            if (_hWnd == IntPtr.Zero)
                throw new Exception("Failed to create window.");

            _hDC = Win32.GetDC(_hWnd);
            if (_hDC == IntPtr.Zero)
                throw new Exception("Failed to get device context.");

            SetupOpenGL();
            MessageLoop();
        }

        private void SetupOpenGL()
        {
            Win32.PIXELFORMATDESCRIPTOR pfd = new Win32.PIXELFORMATDESCRIPTOR
            {
                nSize = (ushort)Marshal.SizeOf(typeof(Win32.PIXELFORMATDESCRIPTOR)),
                nVersion = 1,
                dwFlags = 0x00000004 | 0x00000020 | 0x00000001, 
                iPixelType = 0, 
                cColorBits = 32,
                cDepthBits = 24,
                cStencilBits = 8,
                iLayerType = 0 
            };

            int pixelFormat = Win32.ChoosePixelFormat(_hDC, ref pfd);
            if (pixelFormat == 0)
                throw new Exception("Failed to choose pixel format.");

            if (!Win32.SetPixelFormat(_hDC, pixelFormat, ref pfd))
                throw new Exception("Failed to set pixel format.");

            _hGLRC = Win32.wglCreateContext(_hDC);
            if (_hGLRC == IntPtr.Zero)
                throw new Exception("Failed to create OpenGL context.");

            if (!Win32.wglMakeCurrent(_hDC, _hGLRC))
                throw new Exception("Failed to make OpenGL context current.");

            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
        }

        private void MessageLoop()
        {
            Win32.MSG msg;
            OnInit?.Invoke();
            while (_isRunning)
            {
                while (Win32.PeekMessage(out msg, IntPtr.Zero, 0, 0, 1))
                {
                    Win32.TranslateMessage(ref msg);
                    Win32.DispatchMessage(ref msg);

                    if (msg.message == Win32.WM_DESTROY)
                    {
                        _isRunning = false;
                        break;
                    }
                }

                OnUpdate?.Invoke();
                OnRender?.Invoke();

                Win32.SwapBuffers(_hDC);
            }

            Cleanup();
        }

        private IntPtr WindowProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            switch (msg)
            {

                case Win32.WM_DESTROY:
                    break;

                case Win32.WM_SIZE:
                    int width = LOWORD(lParam);
                    int height = HIWORD(lParam);
                    Width = width;
                    Height = height;
                    OnResize?.Invoke(width, height);
                    GL.Viewport(0, 0, (uint)width, (uint)height);
                    break;

                case Win32.WM_RBUTTONDOWN:
                    int button = wParam.ToInt32();
                    OnMouseDown?.Invoke((MouseCode)button);
                    break;
                case Win32.WM_KEYDOWN:
                    int key = wParam.ToInt32();
                    OnKeyDown?.Invoke(key.Encode());
                    break;

                case Win32.WM_RBUTTONUP:
                    button = wParam.ToInt32();
                    OnMouseUp?.Invoke((MouseCode)button);
                    break;
                case Win32.WM_KEYUP:
                    int keypress = wParam.ToInt32();
                    OnKeyUp?.Invoke(keypress.Encode());
                    break;

                case Win32.WM_MOUSEMOVE:
                    int x = LOWORD(lParam);
                    int y = HIWORD(lParam);
                    OnMouseMove?.Invoke(x, y);
                    break;

                default:
                    return Win32.DefWindowProc(hWnd, msg, wParam, lParam);
            }

            return IntPtr.Zero;
        }

        private int LOWORD(IntPtr ptr)
        {
            return (short)((ulong)ptr & 0xFFFF);
        }

        private int HIWORD(IntPtr ptr)
        {
            return (short)(((ulong)ptr >> 16) & 0xFFFF);
        }

        private void Cleanup()
        {
            if (_hGLRC != IntPtr.Zero)
            {
                Win32.wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);
                Win32.wglDeleteContext(_hGLRC);
                _hGLRC = IntPtr.Zero;
            }

            if (_hDC != IntPtr.Zero)
            {
                Win32.ReleaseDC(_hWnd, _hDC);
                _hDC = IntPtr.Zero;
            }

            if (_hWnd != IntPtr.Zero)
            {
                Win32.DestroyWindow(_hWnd);
                _hWnd = IntPtr.Zero;
            }
        }

        public void Close()
        {
            if (_hWnd != IntPtr.Zero)
                Win32.DestroyWindow(_hWnd);
        }
        public void WaitForClose()
        {
            if (_messageThread != null && _messageThread.IsAlive)
                _messageThread.Join();
        }

        public abstract void Init();
        public abstract void Update();
        public abstract void Draw();

        public void Dispose()
        {
            Close();
            WaitForClose();
        }
    }
}
