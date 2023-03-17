using StbImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SoftRenderer
{
    public class Texture
    {
        byte[] Data;
        public int Width { private set; get; }
        public int Height { private set; get; }
        public static Texture LoadTexture(string filename)
        {
            using(var fs = new StreamReader(filename))
            {
                ImageResult image = ImageResult.FromStream(fs.BaseStream, ColorComponents.RedGreenBlueAlpha);
                var data = image.Data;
                var texture = new Texture(data, image.Width, image.Height);
                return texture;
            }
        }

        Texture(byte[] data, int width, int height)
        {
            Data = data;
            Width = width;
            Height = height;
        }
        public Vector4 GetColor(float x, float y)
        {
            int xx = (int)x;
            x = x - xx;
            int yy = (int)y;
            y = y - yy;
            if (x < 0)
            {
                x = 1 + x;
            }
            if (y < 0)
            {
                y = 1 + y;
            }

            xx = (int)(x * Width);
            yy = (int)(y * Height);
            byte r, g, b, a;
            r = Data[(yy * Width + xx) * 4];
            g = Data[(yy * Width + xx) * 4 + 1];
            b = Data[(yy * Width + xx) * 4 + 2];
            a = Data[(yy * Width + xx) * 4 + 3];

            return new Vector4(r / 255f, g / 255f, b / 255f, a / 255f);

        }
    }
}
