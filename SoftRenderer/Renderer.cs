using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SoftRenderer;

public class Renderer
{
    public Point Size;
    public Renderer(int x, int y)
    {
        ColorBuffer = new Vector4[x,y];
        DepthBuffer = new float[x, y];
        for(int i = 0; i < x;i++)
        {
            for (int j = 0; j < y;j++)
            {
                DepthBuffer[i, j] = 1;
            }
        }
        Size.X = x;
        Size.Y = y;
    }
    public Vector4[,] ColorBuffer { get; set; }
    public float[,] DepthBuffer { get; set; }
    public Element CreateElement()
    {
        return new Element(this);
    }

    public void OutPutToFile(string filename)
    {

        Bitmap image = new Bitmap(Size.X, Size.Y);

        for (int x = 0; x < Size.X; x ++)
        {
            for(int y = 0; y < Size.Y; y ++)
            {
                var color = ColorBuffer[x, y];
                image.SetPixel(x, y, Color.FromArgb((int)(color.W * 255), (int)(color.X * 255), (int)(color.Y * 255), (int)(color.Z * 255)));
            }
        }

        image.Save(filename);

        for (int x = 0; x < Size.X; x++)
        {
            for (int y = 0; y < Size.Y; y++)
            {
                var depth = DepthBuffer[x, y];
                image.SetPixel(x, y, Color.FromArgb(255, (int)(depth * 255), (int)(depth * 255), (int)(depth * 255)));
            }
        }
        var strs = filename.Split('.');

        filename = strs[0] + "_d." + strs[1];
        image.Save(filename);
    }



}
