using OpenGLWrapper;
using Scene.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Scene.Helpers.ImageLoader;

namespace RotatinCubeScene
{
    public unsafe class Cube
    {
        private uint _cubeVAO, _cubeVBO;
        private Shader _cubeShader;
        private float _rotationAngle = 1.0f;
        private int _screenWidth, _screenHeight;
        private uint _cubeTexture;

        public Cube(int screenWidth, int screenHeihgt)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeihgt;
        }

        private float[] _cubeVertices =
        {
            // vertices ----------- uv
            -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,

            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,

            -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

             0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 0.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  1.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,

            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f
        };


        public void Init()
        {
            GL.GenVertexArrays(1, out _cubeVAO);
            GL.GenBuffers(1, out _cubeVBO);

            GL.BindVertexArray(_cubeVAO);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _cubeVBO);
            fixed (float* verticesPtr = _cubeVertices)
            {
                GL.BufferData(BufferTarget.ArrayBuffer, (nuint)(_cubeVertices.Length * sizeof(float)), verticesPtr, BufferUsageHint.StaticDraw);
            }

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), null); // Position
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), (void*)(3 * sizeof(float))); // Texture Coords
            GL.EnableVertexAttribArray(1);

            GL.BindVertexArray(0);

            _cubeShader = new Shader("shaders/shader.vert", "shaders/shader.frag");

            Texture tex = ImageLoader.LoadJpg("assets/textures/material.jpg");
            GL.GenTextures(1, out _cubeTexture);
            GL.BindTexture(TextureTarget.Texture2D, _cubeTexture);

            fixed (byte* imageDataPtr = tex.data)
            {
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, (uint)tex.width, (uint)tex.height, 0, GLPixelFormat.Rgba, GLPixelType.UnsignedByte, imageDataPtr);
            }
            GL.TexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameteri(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

        }


        public void Update(float deltaTime, Vector3 cubePosition, Camera camera)
        {
            
            _rotationAngle += deltaTime * 50.0f; 
            if (_rotationAngle >= 360.0f) _rotationAngle -= 360.0f;


            Matrix4x4 model = Matrix4x4.CreateRotationY((float)_rotationAngle * (float)(Math.PI / 180.0f)) * Matrix4x4.CreateTranslation(cubePosition);

            _cubeShader.Use();

            _cubeShader.SetMat4("model", model);
            _cubeShader.SetMat4("view", camera.ViewMatrix);
            _cubeShader.SetMat4("projection", camera.ProjectionMatrix);
        }
        public void Render()
        {
            _cubeShader.Use();

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, _cubeTexture);
            _cubeShader.SetInt("image", 0);

            GL.BindVertexArray(_cubeVAO);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
            GL.BindVertexArray(0);
        }
    }
}
