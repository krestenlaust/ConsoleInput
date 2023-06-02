using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleInput;

namespace PaintDotNet
{
    class Program
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

                if (Mouse.MouseDown[0])
                {
                    Console.SetCursorPosition(Mouse.X, Mouse.Y);
                    Console.Write('X');
                }

                if (Mouse.MousePress[1])
                {
                    RotateColor();
                }                

                if (Keyboard.KeyUp(Keyboard.VirtualKey.SPACE))
                {
                    RotateColor();
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
