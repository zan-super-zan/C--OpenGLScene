using OpenGLWrapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RotatinCubeScene
{
    public unsafe class Shader
    {
        public uint Id { get; set; }
        public string VertexPath { get; set; } = string.Empty;
        public string FragmentPath { get; set; } = string.Empty;

        public Shader() { }
        public Shader(string vertexPath, string fragmentPath)
        {
            VertexPath = vertexPath;
            FragmentPath = fragmentPath;
            Create();
        }

        public void Create()
        {
            string basePath = Directory.GetCurrentDirectory();
            basePath = Directory.GetParent(basePath).Parent.Parent.FullName;

            string vertexCode = File.ReadAllText(Path.Combine(basePath, VertexPath));
            string fragmentCode = File.ReadAllText(Path.Combine(basePath, FragmentPath));

            IntPtr vertexStringPtr = Marshal.StringToHGlobalAnsi(vertexCode);
            byte*[] vertexStringArray = { (byte*)vertexStringPtr.ToPointer() };

            uint vertexShader = GL.CreateShader(ShaderType.VertexShader);
            fixed (byte** stringArrayPtr = vertexStringArray)
            {
                int[] lengthArray = { vertexCode.Length };
                fixed (int* lengthPtr = lengthArray)
                {
                    GL.ShaderSource(vertexShader, 1, stringArrayPtr, lengthPtr);
                }
            }

            IntPtr fragmentStringPtr = Marshal.StringToHGlobalAnsi(fragmentCode);
            byte*[] fragmentStringArray = { (byte*)fragmentStringPtr.ToPointer() };


            uint fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            fixed (byte** stringArrayPtr = fragmentStringArray)
            {
                int[] lengthArray = { vertexCode.Length };
                fixed (int* lengthPtr = lengthArray)
                {
                    GL.ShaderSource(fragmentShader, 1, stringArrayPtr, lengthPtr);
                }
            }

            GL.CompileShader(vertexShader);
            GL.CompileShader(fragmentShader);

            PrintIVSuccess(vertexShader, ShaderParameter.CompileStatus);
            PrintIVSuccess(fragmentShader, ShaderParameter.CompileStatus);

            Id = GL.CreateProgram();
            GL.AttachShader(Id, vertexShader);
            GL.AttachShader(Id, fragmentShader);
            GL.LinkProgram(Id);

            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            Marshal.FreeHGlobal(vertexStringPtr);
            Marshal.FreeHGlobal(fragmentStringPtr);
        }
        public void Reload()
        {
            GL.DeleteProgram(Id);
            Create();
        }
        public void Use()
        {
            GL.UseProgram(Id);
        }
        public void SetInt(string name, int value)
        {
            int location = -1;
            fixed (byte* namePtr = Encoding.UTF8.GetBytes(name + '\0'))
            {
                location = GL.GetUniformLocation(Id, namePtr);
            }
            GL.Uniform1i(location, value);
        }
        public void SetFloat(string name, float value)
        {
            int location = -1;
            fixed (byte* namePtr = Encoding.UTF8.GetBytes(name + '\0'))
            {
                location = GL.GetUniformLocation(Id, namePtr);
            }
            GL.Uniform1f(location, value);
        }
        public void SetBool(string name, bool value)
        {
            int location = -1;
            fixed (byte* namePtr = Encoding.UTF8.GetBytes(name + '\0'))
            {
                location = GL.GetUniformLocation(Id, namePtr);
            }
            GL.Uniform1i(location, Convert.ToInt32(value));
        }
        public void SetVec2(string name, Vector2 vec)
        {
            int location = -1;
            fixed (byte* namePtr = Encoding.UTF8.GetBytes(name + '\0'))
            {
                location = GL.GetUniformLocation(Id, namePtr);
            }
            GL.Uniform2f(location, vec.X, vec.Y);
        }
        public void SetVec3(string name, Vector3 vec)
        {
            int location = -1;
            fixed (byte* namePtr = Encoding.UTF8.GetBytes(name + '\0'))
            {
                location = GL.GetUniformLocation(Id, namePtr);
            }
            GL.Uniform3f(location, vec.X, vec.Y, vec.Z);
        }
        public void SetVec4(string name, Vector4 vec)
        {
            int location = -1;
            fixed(byte* namePtr = Encoding.UTF8.GetBytes(name + '\0'))
            {
                location = GL.GetUniformLocation(Id, namePtr);
            }
            GL.Uniform4f(location, vec.X, vec.Y, vec.Z, vec.W);
        }
        public void SetMat4(string name, Matrix4x4 mat)
        {
            int location = -1;
            fixed (byte* namePtr = Encoding.UTF8.GetBytes(name + '\0'))
            {
                location = GL.GetUniformLocation(Id, namePtr);
            }
            if (location == -1)
            {
                Console.WriteLine($"Warning: Uniform '{name}' not found in shader.");
                return;
            }
            GL.UniformMatrix4fv(location, 1, false, &mat.M11);
        }
        private void PrintIVSuccess(uint shader, ShaderParameter status)
        {
            int shaderSuccess = -1;
            string infoLog = string.Empty;
            GL.GetShaderiv(shader, ShaderParameter.CompileStatus, &shaderSuccess);
            if (shaderSuccess == -1)
            {
                fixed (char* infoLogPtr = infoLog)
                {
                    GL.GetShaderInfoLog(shader, 512, null, (byte*)infoLogPtr);
                    Debug.Fail(infoLog);
                }
                Console.WriteLine($"Shader compilation error: {infoLog}.");
            }
        }
        private static float[] ConvertToColumnMajor(Matrix4x4 matrix)
        {
            return new float[]
            {
                matrix.M11, matrix.M21, matrix.M31, matrix.M41,
                matrix.M12, matrix.M22, matrix.M32, matrix.M42,
                matrix.M13, matrix.M23, matrix.M33, matrix.M43,
                matrix.M14, matrix.M24, matrix.M34, matrix.M44
            };
        }
    }
}
