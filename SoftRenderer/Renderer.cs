using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SoftRenderer;
public enum ClearFlag
{
    Color = (1 << 0),
    Depth = (1 << 1),
}
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
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                ColorBuffer[i, j] = new Vector4(0, 0, 0, 1);
            }
        }
        Size.X = x;
        Size.Y = y;
    }

    public Color ClearColor { get; set; }
    public void Clear(ClearFlag ClearFlag)
    {

        if ((ClearFlag | ClearFlag.Color) == ClearFlag.Color)
        {
            Vector4 vector = new Vector4
            {
                X = ClearColor.R / 255.0F,
                Y = ClearColor.G / 255.0F,
                Z = ClearColor.B / 255.0F,
                W = ClearColor.A / 255.0F,

            };
            for (int i = 0; i < Size.X; i++)
            {
                for (int j = 0; j < Size.Y; j++)
                {
                    ColorBuffer[i, j] = vector;
                }
            }
        }
        if ((ClearFlag | ClearFlag.Depth) == ClearFlag.Depth)
        {
            for (int i = 0; i < Size.X; i++)
            {
                for (int j = 0; j < Size.Y; j++)
                {
                    DepthBuffer[i, j] = 1;
                }
            }
        }
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
