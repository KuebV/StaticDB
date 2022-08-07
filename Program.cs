using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace StaticDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            switch (args[0])
            {
                case "-encode":
                    List<string> argList = args.ToList();
                    argList.RemoveAt(0);
                    string data = string.Join(' ', argList);
                    Encode(data, 255);
                    break;
                case "-decode":
                    Decode(args[1]);
                    break;
            }
        }

        public static void Decode(string fileName)
        {
            Bitmap bmp = new Bitmap(fileName);
            string finishedString = "";
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color pixel = bmp.GetPixel(j, i);
                    int r = pixel.R;
                    int g = pixel.G == 1 ? 0 : pixel.G;
                    int b = pixel.B == 1 ? 0 : pixel.B;

                    int totalColor = r + g + b;

                    if (pixel.A != 250)
                    {
                        char character = Convert.ToChar(totalColor);
                        finishedString += character.ToString();
                    }          
                }
            }

            Console.WriteLine("Decoded String: \n" + finishedString);
        }


        public static void Encode(string input, int colorThreshold)
        {
            string data = input;

            // Default is 255
            int threshold = colorThreshold;
            if (data.ContainsUTF())
                Console.WriteLine("WARNING: Input contains a UTF-8 Character");

            int nums = 0;
            for (int i = 1; i < data.Length; i++)
                if (data.Length % i == 0)
                    nums++;

            decimal height = 0;
            decimal width = 0;

            double t = 0;
            // THIS NUMBER IS A PRIME NUMBER
            if (nums == 2)
                t = Math.Ceiling(Math.Sqrt(data.Length));

            if (t * t < data.Length)
            {
                bool fixPrime = true;
                while (fixPrime)
                {
                    if (t * t <= data.Length)
                        t++;
                    else
                        fixPrime = false;
                }
            }

            //Handling a prime number
            if (nums == 2)
            {
                Console.WriteLine("Data is Prime: \n" + t);
                height = (decimal)t;
                width = (decimal)t;
            }
            else
            {
                decimal numSquared = (decimal)Math.Round(Math.Sqrt(data.Length));
                Console.WriteLine("Data Squared:\nUn-Optimized: " + numSquared);
                if (numSquared * numSquared < data.Length)
                {
                    bool optimizeSize = true;
                    while (optimizeSize)
                    {
                        if (numSquared * numSquared < data.Length)
                            numSquared++;
                        else
                            optimizeSize = false;
                    }
                }

                Console.WriteLine("Optimized: " + numSquared);

                height = Math.Round(numSquared);
                width = Math.Round(numSquared);

            }


            Console.WriteLine("Image will be saved as " + height + "x" + width);

            Bitmap bmp = new Bitmap((int)width, (int)Math.Ceiling(height));


            int index = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int r = 0, g = 1, b = 1, a = 255;

                    //If there's extra space, just make the rest of the pixels white
                    if (index > data.Length - 1)
                    {
                        bmp.SetPixel(x, y, Color.FromArgb(250, 32, 1, 1));
                        continue;
                    }

                    else
                    {
                        int value = data[index];
                        r = value;
                        if (r > threshold)
                        {
                            int excess = r - threshold;
                            g = excess;
                            r = threshold;
                            a--;
                        }

                        if (g > threshold)
                        {
                            int excess = g - threshold;
                            b = excess;
                            g = threshold;
                            a--;
                        }


                        bmp.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                    }
                    index++;

                }
            }
            long seconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            bmp.Save($"{seconds}.png");
            Console.WriteLine("Image has been saved as " + seconds + ".png");
        }

    }


    public static class Extensions
    {
        public static bool ContainsUTF(this string input) => input.Any(c => c > 765);
    }

}
