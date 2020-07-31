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
        private SKPaint graph_paint;
        private SKPaint coordinates_lines_paint;
        private SKImage image;
        private SKData data;
        private MemoryStream memory_stream;
        private Bitmap bitmap;
        private SKPath graph;
        private SKPath coordinates_lines;
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

                coordinates_lines_paint = new SKPaint();
                coordinates_lines_paint.Color = SKColor.Parse("#d55e00");
                coordinates_lines_paint.IsAntialias = true;
                coordinates_lines_paint.StrokeWidth = 1;
                coordinates_lines_paint.Style = SKPaintStyle.Stroke;

                graph_paint = new SKPaint();
                graph_paint.Color = SKColors.Black;
                graph_paint.IsAntialias = true;
                graph_paint.StrokeWidth = 1;
                graph_paint.Style = SKPaintStyle.Stroke;

                //points = get_points(-198, height);
                coordinates_lines = new SKPath();
                graph = new SKPath();
                points = coordinates();
                graph_coordinates_lines();
                graph_lines(points);
                
                canvas.DrawPath(coordinates_lines,coordinates_lines_paint);
                canvas.DrawPath(graph,graph_paint);

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

            for(int i = 0; i < coordinates.GetLength(0); i++)
            {
                    coordinates[i] = new SKPoint(x, -1 * (float)numbers[0].GetValue(i));
                    x++;
            }
            return fit_to_graph(coordinates);
        }

        public void graph_lines(SKPoint[] points)
        {
            graph.MoveTo(points[0]);
            for(int i = 1; i < points.GetLength(0); i++)
            {
                graph.LineTo(points[i]);
            }
        }

        public void graph_coordinates_lines()
        {   
            coordinates_lines.MoveTo(new SKPoint(0,width/2));
            coordinates_lines.LineTo(new SKPoint(width,(int)height/2));
            coordinates_lines.Close();
            coordinates_lines.MoveTo(new SKPoint(width/2,0));
            coordinates_lines.LineTo(new SKPoint((int)width/2,height));
            coordinates_lines.Close();
        }
    }
}