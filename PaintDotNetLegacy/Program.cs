﻿using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleInput;

namespace PaintDotNetLegacy
{
    /// <summary>
    /// Example class using legacy API (pre-2.0).
    /// </summary>
    [Obsolete]
    static class Program
    {
        static List<ConsoleColor> consoleColours;

        static void Main(string[] args)
        {
            consoleColours = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();

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
            Console.ForegroundColor = consoleColours.SkipWhile(x => x != currentColor).Skip(1).DefaultIfEmpty(consoleColours[0]).FirstOrDefault();

            string colorName = Enum.GetName(typeof(ConsoleColor), Console.ForegroundColor);
            Console.Title = $"Selected Color: {colorName}";
        }
    }
}
