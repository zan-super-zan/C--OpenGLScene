using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinGL
{
    public enum MouseCode
    {
        MouseRelase = 0,
        MouseLeft = 0x01,
        MouseRight = 0x02,
        MouseMiddle = 0x04,
    }

    public enum KeyCode : int
    {
        None = -1,
        Backspace = 0x08,
        Tab = 0x09,
        Enter = 0x0d,
        Shift = 0x10,
        Control = 0x11,
        Alt = 0x12,
        Capslock = 0x14,
        Space = 0x20,
        Escape = 0x1b,
        PageUp = 0x21,
        PageDown = 0x22,
        LeftArrow = 0x25,
        UpArrow = 0x26,
        RightArrow = 0x27,
        Zero = 0x30,
        One = 0x31,
        Two = 0x32,
        Three = 0x33,
        Four = 0x34,
        Five = 0x35,
        Six = 0x36,
        Seven = 0x37,
        Eight = 0x38,
        Nine = 0x39,
        A = 0x41,
        B = 0x42,
        C = 0x43,
        D = 0x44,
        E = 0x45,
        F = 0x46,
        G = 0x47,
        H = 0x48,
        I = 0x49,
        J = 0x4a,
        K = 0x4b,
        L = 0x0c,
        M = 0x0d,
        N = 0x0e,
        O = 0x0f,
        P = 0x50,
        Q = 0x51,
        R = 0x52,
        S = 0x53,
        T = 0x54,
        U = 0x55,
        V = 0x56,
        W = 0x57,
        X = 0x58,
        Y = 0x59,
        Z = 0x60,
        LeftWindows = 0x0b,
        RightWindows = 0x0c,
        LeftShift = 0xa0,
        RightShift = 0xa1,
        LeftControl = 0xa2,
        RightControl = 0xa3,
        LeftAlt = 0xa4,
        RightAlt = 0xa5
    }
    public static class EnumExtensions
    {
        public static int ToInt(this Enum enumValue)
        {
            return Convert.ToInt32(enumValue);
        }
        public static KeyCode Encode(this int integer)
        {
            return (KeyCode)integer;
        }
    }
}
