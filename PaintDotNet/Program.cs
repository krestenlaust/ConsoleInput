using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleInput;
using ConsoleInput.Devices;

namespace PaintDotNet
{
    /// <summary>
    /// Example class using new 2.0 API.
    /// </summary>
    static class Program
    {
        static List<ConsoleColor> consoleColours;

        static void Main(string[] args)
        {
            consoleColours = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            Console.CursorVisible = false;

            var mouse = new ConsoleMouse();
            var keyboard = new ConsoleKeyboard();

            var inputManager = new InputManager();
            inputManager.AddDevice(mouse);
            inputManager.AddDevice(keyboard);

            Console.WriteLine("Try pressing/dragging the primary or secondary mouse button!");

            while (true)
            {
                inputManager.Update();

                Console.Title = $"{mouse.IsButtonPressed(MouseButton.Left)} {mouse.IsButtonDown(MouseButton.Left)} {mouse.IsButtonReleased(MouseButton.Left)}";

                if (mouse.IsButtonPressed(MouseButton.MouseWheel))
                {
                    Console.SetCursorPosition(mouse.X, mouse.Y);
                    Console.Write("Pressed");
                }

                if (mouse.IsButtonReleased(MouseButton.MouseWheel))
                {
                    Console.SetCursorPosition(mouse.X, mouse.Y);
                    Console.Write("Release");
                }

                if (mouse.IsButtonDown(MouseButton.Left))
                {
                    Console.SetCursorPosition(mouse.X, mouse.Y);
                    Console.Write('X');
                }

                if (mouse.IsButtonPressed(MouseButton.Right))
                {
                    RotateColor();
                }

                if (keyboard.IsButtonReleased(KeyboardButton.Space))
                {
                    RotateColor();
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
