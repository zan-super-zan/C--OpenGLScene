using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Scene.Helpers
{
    public static class ImageLoader
    {
        public struct Texture
        {
            public int ID, width, height, nrChannels;
            public byte[] data;
        };
        public static Texture LoadTga(string filePath)
        {
            string basePath = Directory.GetCurrentDirectory();
            basePath = Directory.GetParent(basePath).Parent.Parent.FullName;

            using (BinaryReader reader = new BinaryReader(File.Open(Path.Combine(basePath, filePath), FileMode.Open)))
            {
                byte idLength = reader.ReadByte();
                byte colorMapType = reader.ReadByte();
                byte imageType = reader.ReadByte();

                reader.BaseStream.Seek(5, SeekOrigin.Current);

                ushort xOrigin = reader.ReadUInt16();
                ushort yOrigin = reader.ReadUInt16();
                ushort width = reader.ReadUInt16();
                ushort height = reader.ReadUInt16();
                byte pixelDepth = reader.ReadByte(); 
                byte imageDescriptor = reader.ReadByte();

                if (idLength > 0)
                {
                    reader.BaseStream.Seek(idLength, SeekOrigin.Current);
                }

                if (imageType != 2 && imageType != 3) 
                {
                    throw new NotSupportedException($"Unsupported TGA image type: {imageType}");
                }

                int nrChannels = pixelDepth / 8;
                if (nrChannels < 1 || nrChannels > 4)
                {
                    throw new NotSupportedException($"Unsupported pixel depth: {pixelDepth}");
                }

                byte[] imageData = new byte[width * height * nrChannels];

                reader.Read(imageData, 0, imageData.Length);

                bool isOriginBottomLeft = (imageDescriptor & 0x20) == 0;
                if (isOriginBottomLeft)
                {
                    FlipImageVertically(imageData, width, height, nrChannels);
                }

                
                return new Texture
                {
                    ID = 0, 
                    width = width,
                    height = height,
                    nrChannels = nrChannels,
                    data = imageData
                };
            }
        }
        public static Texture LoadJpg(string filePath)
        {
            string basePath = Directory.GetCurrentDirectory();
            basePath = Directory.GetParent(basePath).Parent.Parent.FullName;

            string fullPath = Path.Combine(basePath, filePath);

            using (Image<Rgba32> image = SixLabors.ImageSharp.Image.Load<Rgba32>(fullPath))
            {
                int width = image.Width;
                int height = image.Height;

                byte[] imageData = new byte[width * height * 4]; 
                image.CopyPixelDataTo(imageData);

                return new Texture
                {
                    ID = 0,
                    width = width,
                    height = height,
                    nrChannels = 4, 
                    data = imageData
                };
            }
        }
        private static void FlipImageVertically(byte[] imageData, int width, int height, int nrChannels)
        {
            int rowSize = width * nrChannels;
            byte[] tempRow = new byte[rowSize];
            for (int y = 0; y < height / 2; y++)
            {
                int topRow = y * rowSize;
                int bottomRow = (height - y - 1) * rowSize;

                Array.Copy(imageData, topRow, tempRow, 0, rowSize);
                Array.Copy(imageData, bottomRow, imageData, topRow, rowSize);
                Array.Copy(tempRow, 0, imageData, bottomRow, rowSize);
            }
        }
    }
}
