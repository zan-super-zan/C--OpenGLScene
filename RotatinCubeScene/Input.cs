using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinGL;

namespace RotatinCubeScene
{
    public class Input
    {
        private static Input _instance;
        public static Input Instance => _instance ??= new Input();

        public double MouseX { get; private set; }
        public double MouseY { get; private set; }

        private List<KeyCode> _pressedKeys = new List<KeyCode>();
        private List<MouseCode> _pressedMouseKeys = new List<MouseCode>();

        public void Initialize(Window window)
        {
            window.OnKeyDown += KeyDown;
            window.OnKeyUp += KeyUp;
            window.OnMouseMove += MouseMove;
            window.OnMouseDown += MousePressed;
            window.OnMouseUp += MouseRelease;
        }

        private void MouseMove(int x, int y)
        {
            MouseX = x;
            MouseY = y;
        }

        private void KeyUp(KeyCode code)
        {
            while(_pressedKeys.Contains(code)) 
                _pressedKeys.Remove(code);
        }

        private void KeyDown(KeyCode code)
        {
            if(!_pressedKeys.Contains(code)) 
                _pressedKeys.Add(code);
        }

        private void MousePressed(MouseCode code)
        {
            if (!_pressedMouseKeys.Contains(code))
                _pressedMouseKeys.Add(code);
        }
        private void MouseRelease(MouseCode code)
        {
            if(code == MouseCode.MouseRelase)
                _pressedMouseKeys.Clear();
        }

        public bool IsKeyDown(KeyCode code)
        {
            return _pressedKeys.Contains(code);
        }
        public bool IsMousePress(MouseCode code)
        {
            return _pressedMouseKeys.Contains(code);
        }
    }
}
