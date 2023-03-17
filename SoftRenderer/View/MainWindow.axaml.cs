using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia;
using System;
using SoftRenderer.View;
using Avalonia.Media;
using Avalonia.Platform;

namespace SoftRenderer
{
    public partial class MainWindow : Window
    {
        WriteableBitmap WriteableBitmap;

        public MainWindow()
        {
            InitializeComponent();
            this.Width = RenderWindow.Instance.Size.X;
            this.Height = RenderWindow.Instance.Size.Y;  
            WriteableBitmap = new WriteableBitmap(new PixelSize(RenderWindow.Instance.Size.X, RenderWindow.Instance.Size.Y), new Vector(100, 100), Avalonia.Platform.PixelFormat.Rgba8888);
            MyImage.Source = WriteableBitmap;
            MyImage.Width = this.Width;
            MyImage.Height = this.Height;
            RenderWindow.Instance.Print += SetPixel;

            LastTime = DateTime.Now;
            RenderWindow.Instance.InitInternal();
        }

        private void SetPixel(int x, int y, Color color)
        {
            unsafe
            {
                byte* p = (byte*)buffer.Address;
                byte* p2 = p + ((y * buffer.Size.Width) + x) * 4;
                *p2 = color.R;
                *(p2 + 1) = color.G;
                *(p2 + 2) = color.B;
                *(p2 + 3) = color.A;
            }
        }
        ILockedFramebuffer buffer;
        DateTime LastTime;
        protected override void HandlePaint(Rect rect)
        {
            buffer = WriteableBitmap.Lock();
            base.HandlePaint(rect);
            RenderWindow.Instance.RenderInternal((float)(DateTime.Now - LastTime).TotalSeconds);
            buffer.Dispose();
        }


        protected override bool HandleClosing()
        {
            RenderWindow.Instance.ClosingInternal();
            return base.HandleClosing();
        }
    }
}