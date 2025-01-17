using OpenGLWrapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WinGL;

namespace RotatinCubeScene
{
    public unsafe class Application : Window
    {
        public const int ScreenWidth = 1080, ScreenHeight = 720;

        private Stopwatch _stopwatch = new Stopwatch();
        private float _lastFrameTime = 0.0f;
        private SkyBox _skyBox;
        private Cube _cubeOne, _cubeTwo, _cubeThree;
        private Camera _camera;
        public Application()
            : base(ScreenWidth, ScreenHeight, "Ray Tracer")
        {
            OnUpdate += Update;
            OnRender += Draw;
            OnInit += Init;
        }
 
        
        public override void Init()
        {

            Input.Instance.Initialize(this);

            _camera = new Camera(ScreenWidth, ScreenHeight);

            _skyBox = new SkyBox();
            _skyBox.Init();

            _cubeOne = new Cube(ScreenWidth, ScreenHeight);
            _cubeOne.Init();

            _cubeTwo = new Cube(ScreenWidth, ScreenHeight);
            _cubeTwo.Init();

            _cubeThree = new Cube(ScreenWidth, ScreenHeight);
            _cubeThree.Init();


            _stopwatch.Start();
            GL.Enable(EnableCap.DepthTest);

            unsafe
            {
                byte* version = GL.GetString(StringName.Version);
                IntPtr sp = (IntPtr)version;
                string result = Marshal.PtrToStringAnsi(sp);

                Console.WriteLine(result);

                Marshal.FreeHGlobal(sp);
            }
        }
        public override void Update()
        {
            float currentFrameTime = (float)_stopwatch.Elapsed.TotalSeconds;
            float deltaTime = currentFrameTime - _lastFrameTime;
            _lastFrameTime = currentFrameTime;

            _cubeOne.Update(deltaTime, new Vector3(-2.0f, 1.0f, -7.0f), _camera);
            _cubeTwo.Update(deltaTime, new Vector3(0.0f, 1.0f, -7.0f), _camera);
            _cubeThree.Update(deltaTime, new Vector3(2.0f, 1.0f, -7.0f), _camera);

            _camera.Update(deltaTime);
        }
        public override void Draw()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

            _skyBox.Render();

            _cubeOne.Render();
            _cubeTwo.Render();
            _cubeThree.Render();
        }
        public void Open()
        {
            Show();
        }
    }
}
