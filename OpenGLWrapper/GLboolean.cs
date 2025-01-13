using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLWrapper
{
    public struct GLboolean : IEquatable<GLboolean>
    {

        public byte Value;
        public GLboolean(byte value)
        {
            Value = value;
        }

        public static readonly GLboolean True = new GLboolean(1);
        public static readonly GLboolean False = new GLboolean(0);
        public bool Equals(GLboolean other) => Value.Equals(other.Value);
        public override bool Equals(object obj) => obj is GLboolean b && Equals(b);
        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => $"{(this ? "True" : "False")} ({Value})";

        public static implicit operator bool(GLboolean b) => b.Value != 0;
        public static implicit operator uint(GLboolean b) => b.Value;
        public static implicit operator GLboolean(bool b) => b ? True : False;
        public static implicit operator GLboolean(byte value) => new GLboolean(value);

        public static bool operator ==(GLboolean left, GLboolean right) => left.Value == right.Value;
        public static bool operator !=(GLboolean left, GLboolean right) => left.Value != right.Value;
    }
}
