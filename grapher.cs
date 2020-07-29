using System;
using SkiaSharp;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace _2020_07_26_Console_Grapher
{
    public class grapher : Form
    {
        //private string function = "";
        private int width = 500, height = 500 ;
        private SKImageInfo image_info;
        private SKSurface surface;
        private SKCanvas canvas;
        private SKPaint paint;
        private SKImage image;
        private SKData data;
        private MemoryStream memory_stream;
        private Bitmap bitmap;
        private SKPath graph;
        private SKPoint[] points;

        public grapher(/*string function*/)
        {
            //this.function = function;
            this.Size = new Size(500, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximumSize = new Size(500, 500);
            this.MinimumSize = new Size(500, 500);

            try
            {
                image_info = new SKImageInfo(width, height);
                surface = SKSurface.Create(image_info);

                canvas = surface.Canvas;
                canvas.Clear(SKColors.White);

                paint = new SKPaint();
                paint.Color = SKColors.Black;
                paint.IsAntialias = true;
                paint.StrokeWidth = 1;
                paint.Style = SKPaintStyle.Stroke;

                points = get_points(-1 * width, height);
                graph = new SKPath();
                graph.AddPoly(points);

                canvas.DrawPath(graph,paint);

                image = surface.Snapshot();
                data = image.Encode(SKEncodedImageFormat.Png, 500);
                memory_stream = new MemoryStream(data.ToArray());
                bitmap = new Bitmap(memory_stream, false);
                this.BackgroundImage = bitmap;
                this.BackgroundImageLayout = ImageLayout.None;              
            }
            catch (System.Exception error)
            {
                Console.WriteLine("" + error);
            }
        }

        public SKPoint[] get_points(int x_intercept_one, int x_intercept_two)
        {
            int length_of_coordinate_array = (int)(Math.Sqrt(Math.Pow(x_intercept_two-x_intercept_one, 2)));

            if (length_of_coordinate_array%2 != 0)
            {
                length_of_coordinate_array++;
            }

            SKPoint[] coordinates = new SKPoint[length_of_coordinate_array];

            int x = x_intercept_one;

            for(int i = 0; i < coordinates.Length; i++)
            {
                coordinates[i] = new SKPoint(x, /*function*/-(float)(5 * Math.Pow(x, 2) + 4)); //TODO: take the string function, which is a long string holding the points, and decode that string in points the drawing function can use.
                x++;
            }
            return fit_to_graph(coordinates);
        }

        public SKPoint[] fit_to_graph(SKPoint[] coordinates)
        {
            SKPoint[] translated_coordinates = new SKPoint[coordinates.Length];

            for(int i = 0; i < coordinates.Length; i++)
            {
                if(coordinates[i].X <= 0 && coordinates[i].Y <= 0)
                {
                    translated_coordinates[i] = new SKPoint(this.Width/2 + coordinates[i].X, this.Height/2 + coordinates[i].Y);
                }
                else if(coordinates[i].X >= 0 && coordinates[i].Y <= 0)
                {
                    translated_coordinates[i] = new SKPoint(this.Width/2 + coordinates[i].X, this.Height/2 + coordinates[i].Y);    
                }
                else if(coordinates[i].X <= 0 && coordinates[i].Y >= 0)
                {
                    translated_coordinates[i] = new SKPoint(this.Width/2 + coordinates[i].X, this.Height/2 + coordinates[i].Y);    
                }
                else if(coordinates[i].X >= 0 && coordinates[i].Y >= 0)
                {
                    translated_coordinates[i] = new SKPoint(this.Width/2 + coordinates[i].X, this.Height/2 + coordinates[i].Y);    
                }
            }
            return translated_coordinates;
        }

        public int[] function_to_y_coordinates(string function)
        {
            int[] y_coordinates = new int[(int)(Math.Sqrt(Math.Pow(-1*width-height, 2)))];
            return y_coordinates;
        }
    }
}