using System;
using SFML.Graphics;
using SFML.Window;

namespace ZuliaFractalSFML
{
    class Program
    {
        static RenderWindow window;

        public static void DrawFractal(VertexArray target_vertexArray, uint windowWidth, uint windowHight)
        {
            double cRe = -0.70176, cIm = -0.3842;
            double newRe, newIm, oldRe, oldIm;
            double zoom = 1, moveX = 0, moveY = 0;
            int maxIterations = 1000;

            for (int x = 0; x < windowWidth; x++)
            {
                for (int y = 0; y < windowHight; y++)
                {
                    newRe = 1.5 * (x - windowWidth / 2) / (0.5 * zoom * windowWidth) + moveX;
                    newIm = (y - windowHight / 2) / (0.5 * zoom * windowHight) + moveY;

                    int i;

                    //начинается процесс итерации
                    for (i = 0; i < maxIterations; i++)
                    {
                        //Запоминаем значение предыдущей итерации
                        oldRe = newRe;
                        oldIm = newIm;

                        // в текущей итерации вычисляются действительная и мнимая части 
                        newRe = oldRe * oldRe - oldIm * oldIm + cRe;
                        newIm = 2 * oldRe * oldIm + cIm;

                        // если точка находится вне круга с радиусом 2 - прерываемся
                        if ((newRe * newRe + newIm * newIm) > 4) break;
                    }

                    Vertex tmp = new Vertex();

                    tmp.Color = new Color(255, (byte)((i * 1) % 255), 255, (byte)((i * 9) % 255));
                    tmp.Position = new SFML.System.Vector2f(x, y);
                    target_vertexArray.Append(tmp);
                }
            }
        }

        private static void window_Closed(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

        static void Main(string[] args)
        {
            uint windowWidth = 1920;
            uint windowHight = 1080;

            window = new RenderWindow(new VideoMode(windowWidth, windowHight), "ZuliaFractalSFML");
            window.SetActive();
            window.Closed += new EventHandler(window_Closed);

            VertexArray vertexArray = new VertexArray(PrimitiveType.Points);

            DrawFractal(vertexArray, windowWidth, windowHight);

            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(Color.Black);
                window.Draw(vertexArray);
                window.Display();
            }

        }
    }
}
