using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleInput;

namespace PaintDotNetLegacy
{
    static class Program
    {
        static List<ConsoleColor> ConsoleColours;

        static void Main(string[] args)
        {
            ConsoleColours = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();

            Console.CursorVisible = false;
            Input.Setup(true, true);

            Console.WriteLine("Try pressing/dragging the primary or secondary mouse button!");

            while (true)
            {
                Input.Update();

                Console.Title = $"{Mouse.MouseUp[0]} {Mouse.MouseDown[0]} {Mouse.MousePress[0]}";

                if (Mouse.GetMouseDown(MouseButton.Left))
                {
                    Console.SetCursorPosition(Mouse.X, Mouse.Y);
                    Console.Write('X');
                }

                if (Mouse.GetMousePressed(MouseButton.Right))
                {
                    RotateColor();
                }

                if (Keyboard.KeyUp(Keyboard.VirtualKey.SPACE))
                {
                    RotateColor();
                }

                if (Keyboard.KeyDown(Keyboard.VirtualKey.ESCAPE))
                {
                    break;
                }
            }
        }

        static void RotateColor()
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColours.SkipWhile(x => x != currentColor).Skip(1).DefaultIfEmpty(ConsoleColours[0]).FirstOrDefault();

            string colorName = Enum.GetName(typeof(ConsoleColor), Console.ForegroundColor);
            Console.Title = $"Selected Color: {colorName}";
        }
    }
}
