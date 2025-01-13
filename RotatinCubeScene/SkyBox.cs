using OpenGLWrapper;
using Scene.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RotatinCubeScene
{
    public unsafe class SkyBox
    {
        public uint SkyBoxTextureId { get; set; }
        private Shader _skyBoxShader;
        private uint _VAO, _VBO;

        private readonly float[] _vertices =
        {
             -1.0f,  1.0f, -1.0f,
             -1.0f, -1.0f, -1.0f,
              1.0f, -1.0f, -1.0f,
              1.0f, -1.0f, -1.0f,
              1.0f,  1.0f, -1.0f,
             -1.0f,  1.0f, -1.0f,

             -1.0f, -1.0f,  1.0f,
             -1.0f, -1.0f, -1.0f,
             -1.0f,  1.0f, -1.0f,
             -1.0f,  1.0f, -1.0f,
             -1.0f,  1.0f,  1.0f,
             -1.0f, -1.0f,  1.0f,

              1.0f, -1.0f, -1.0f,
              1.0f, -1.0f,  1.0f,
              1.0f,  1.0f,  1.0f,
              1.0f,  1.0f,  1.0f,
              1.0f,  1.0f, -1.0f,
              1.0f, -1.0f, -1.0f,

             -1.0f, -1.0f,  1.0f,
             -1.0f,  1.0f,  1.0f,
              1.0f,  1.0f,  1.0f,
              1.0f,  1.0f,  1.0f,
              1.0f, -1.0f,  1.0f,
             -1.0f, -1.0f,  1.0f,

             -1.0f,  1.0f, -1.0f,
              1.0f,  1.0f, -1.0f,
              1.0f,  1.0f,  1.0f,
              1.0f,  1.0f,  1.0f,
             -1.0f,  1.0f,  1.0f,
             -1.0f,  1.0f, -1.0f,

             -1.0f, -1.0f, -1.0f,
             -1.0f, -1.0f,  1.0f,
              1.0f, -1.0f, -1.0f,
              1.0f, -1.0f, -1.0f,
             -1.0f, -1.0f,  1.0f,
              1.0f, -1.0f,  1.0f
        };

        private uint LoadSkyboxTextures()
        {
            string[] faces = new string[]
            {
                "assets/skybox/right.jpg",  // Positive X
                "assets/skybox/left.jpg",   // Negative X
                "assets/skybox/top.jpg",    // Positive Y
                "assets/skybox/bottom.jpg", // Negative Y
                "assets/skybox/front.jpg",  // Positive Z
                "assets/skybox/back.jpg"    // Negative Z
            };
            uint textureID;
            GL.GenTextures(1, out textureID);
            GL.BindTexture(TextureTarget.TextureCubeMap, textureID);

            for (int i = 0; i < faces.Length; i++)
            {
                var texture = ImageLoader.LoadJpg(faces[i]);

                if (texture.data == null)
                {
                    Console.WriteLine($"Failed to load texture: {faces[i]}");
                    continue;
                }

                unsafe
                {
                    fixed (byte* dataPtr = texture.data)
                    {
                        GL.TexImage2D(
                            TextureTarget.TextureCubeMapPositiveX + i,
                            0,
                            PixelInternalFormat.Rgba,
                            (uint)texture.width,
                            (uint)texture.height,
                            0,
                            texture.nrChannels == 3 ? GLPixelFormat.Rgb : GLPixelFormat.Rgba,
                            GLPixelType.UnsignedByte,
                            dataPtr);
                    }
                }
            }

            GL.TexParameteri(TextureTarget.TextureCubeMap, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameteri(TextureTarget.TextureCubeMap, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameteri(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameteri(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameteri(TextureTarget.TextureCubeMap, TextureParameterName.TextureWrapR, (int)TextureWrapMode.ClampToEdge);

            return textureID;
        }

        public void Init()
        {
            _skyBoxShader = new Shader("shaders/skyBoxShader.vert", "shaders/skyBoxShader.frag");
            SkyBoxTextureId = LoadSkyboxTextures();

            GL.GenVertexArrays(1, out _VAO);
            GL.GenBuffers(1, out _VBO);

            GL.BindVertexArray(_VAO);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _VBO);
            fixed (float* verticesPtr = _vertices)
            {
                GL.BufferData(BufferTarget.ArrayBuffer, (nuint)(_vertices.Length * sizeof(float)), verticesPtr, BufferUsageHint.StaticDraw);
            }

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), null);
            GL.EnableVertexAttribArray(0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }
        public void Render()
        {
            GL.DepthFunc(DepthFunction.Lequal);

            _skyBoxShader.Use();

            Matrix4x4 view = new Matrix4x4(
                1.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 1.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 1.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 1.0f
            );
            _skyBoxShader.SetMat4("View", view);

            float fov = MathF.PI / 4.0f;
            float aspectRatio = 16.0f / 9.0f; 
            float nearPlane = 0.1f;
            float farPlane = 100.0f;

            float tanHalfFov = MathF.Tan(fov / 2.0f);
            Matrix4x4 projection = new Matrix4x4(
                1.0f / (aspectRatio * tanHalfFov), 0.0f, 0.0f, 0.0f,
                0.0f, 1.0f / tanHalfFov, 0.0f, 0.0f,
                0.0f, 0.0f, -(farPlane + nearPlane) / (farPlane - nearPlane), -1.0f,
                0.0f, 0.0f, -(2.0f * farPlane * nearPlane) / (farPlane - nearPlane), 0.0f
            );
            _skyBoxShader.SetMat4("Proj", projection);

            GL.BindVertexArray(_VAO);
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.TextureCubeMap, SkyBoxTextureId);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

            GL.BindVertexArray(0);
            GL.DepthFunc(DepthFunction.Less);
        }
    }
}
