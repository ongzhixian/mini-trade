using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTrade.ConsoleApp.Services;

internal class DrawTest
{

    public void DoDraw()
    {
        int width = 800;
        int height = 600;

        //SKBitmap bitmap = new SKBitmap(width, height);
        //using (SKCanvas canvas = new SKCanvas(bitmap))
        //{
        //    //··· // call drawing function
        //    canvas.Clear();
        //    canvas.DrawText(TEXT, 0, -bounds.Top, textPaint);

        //}
        const string TEXT = "Hello, Bitmap!";

        using (SKPaint textPaint = new SKPaint { TextSize = 48 })
        {
            SKRect bounds = new SKRect();

            textPaint.MeasureText(TEXT, ref bounds);

            SKBitmap helloBitmap = new SKBitmap((int)bounds.Right, (int)bounds.Height);

            using (SKCanvas bitmapCanvas = new SKCanvas(helloBitmap))
            {
                bitmapCanvas.Clear();
                bitmapCanvas.DrawText(TEXT, 0, -bounds.Top, textPaint);
            }

            helloBitmap.Encode(SKEncodedImageFormat.Png, 0).ToArray();

            using (System.IO.FileStream fs = new FileStream(@"C:\data\oanda\test.png", FileMode.OpenOrCreate))
            {
                fs.Write(helloBitmap.Encode(SKEncodedImageFormat.Png, 0).ToArray());
            }



        }
    }
}
