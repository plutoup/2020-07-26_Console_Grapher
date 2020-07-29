/*

TODO : Add a saving function and deletion function for polish.

*/




using System;
using SkiaSharp;
using System.IO;
using System.Drawing;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;

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

        public grapher()
        {
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

                //points = get_points(-198, height);
                graph = new SKPath();
                points = coordinates();
                graph.AddPoly(points, false);

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
                coordinates[i] = new SKPoint(x,-(float)(5 * Math.Sin(x) + 4));
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

        public SKPoint[] coordinates()
        {
            string current_directory = Directory.GetCurrentDirectory() + "\\";
            string target_file = @"data.json";
            string data = File.ReadAllText(target_file);
            
            IDictionary values = JsonConvert.DeserializeObject<IDictionary>(data);
            JArray casting_numbers = new JArray(values["y_coordinates"]);

            float x_intercept_one = (float)-500;
            float x_intercept_two = (float)500;

            int length_of_coordinate_array = (int)(Math.Sqrt(Math.Pow(x_intercept_two-x_intercept_one, 2)));

            if (length_of_coordinate_array%2 != 0)
            {
                length_of_coordinate_array++;
            }

            float x = x_intercept_one;
            SKPoint[] coordinates = new SKPoint[length_of_coordinate_array];
            List<float[]> numbers = casting_numbers.ToObject<List<float[]>>();
            //graph.MoveTo((float)numbers[0].GetValue(numbers[0].GetLength(0) - 1) - 1, (float)numbers[0].GetValue(numbers[0].GetLength(0) - 1));

            for(int i = 0; i < coordinates.Length - 2; i++)
            {
                coordinates[i] = new SKPoint(x, (float)numbers[0].GetValue(i));
                x++;
            }
            return fit_to_graph(coordinates);
        }
    }
}