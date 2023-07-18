using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ConsoleInput;
using ConsoleInput.Devices;

namespace PaintDotNet
{
    class Program
    {
        static List<ConsoleColor> ConsoleColours;

        static void Main(string[] args)
        {
            ConsoleColours = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            Console.CursorVisible = false;

            ConsoleMouse mouse = new ConsoleMouse();
            ConsoleKeyboard keyboard = new ConsoleKeyboard();

            IInputManager inputManager = new InputManager();
            inputManager.AddDevice(mouse);
            inputManager.AddDevice(keyboard);

            Console.WriteLine("Try pressing/dragging the primary or secondary mouse button!");

            while (true)
            {
                inputManager.Update();

                Console.Title = $"{mouse.IsButtonPressed(MouseButton.Left)} {mouse.IsButtonDown(MouseButton.Left)} {mouse.IsButtonReleased(MouseButton.Left)}";

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
            Console.ForegroundColor = ConsoleColours.SkipWhile(x => x != currentColor).Skip(1).DefaultIfEmpty(ConsoleColours[0]).FirstOrDefault();

            string colorName = Enum.GetName(typeof(ConsoleColor), Console.ForegroundColor);
            Console.Title = $"Selected Color: {colorName}";
        }
    }
}
