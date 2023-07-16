using System.Collections.Generic;
using ConsoleInput.WinAPI;
using static ConsoleInput.WinAPI.InputEventHandling;

namespace ConsoleInput
{
    public class InputManager
    {
        readonly ICollection<IDevice> devices;

        public InputManager(params IDevice[] devices)
        {
            this.devices = devices;

            stdInHandle = GetStdHandle(STD_INPUT_HANDLE);

            uint dwMode = 0;
            foreach (var item in devices)
            {
                if (item is IRequireConsoleMode consoleModeGetter)
                {
                    dwMode |= consoleModeGetter.GetConsoleMode();
                }
            }

            SetConsoleMode(stdInHandle, dwMode);
        }

        public void Update()
        {
            GetNumberOfConsoleInputEvents(stdInHandle, out uint numberOfEvents);
            INPUT_RECORD[] inputBuffer = new INPUT_RECORD[numberOfEvents];

            if (numberOfEvents > 0)
            {
                ReadConsoleInput(stdInHandle, inputBuffer, numberOfEvents, out numberOfEvents);
            }

            INPUT_RECORD[] otherInputBuffer = new INPUT_RECORD[numberOfEvents];
            uint otherInputCount = 0;

            for (int i = 0; i < numberOfEvents; i++)
            {
                switch (inputBuffer[i].EventType)
                {
                    case InputEventType.KEY_EVENT when !IgnoreKeyboard:
                        Keyboard.HandleKeyboardEvent(inputBuffer[i].Event.KeyEvent);
                        break;
                    case InputEventType.MOUSE_EVENT when !IgnoreMouse:
                        Mouse.HandleMouseEvent(inputBuffer[i].Event.MouseEvent);
                        break;
                    default:
                        otherInputBuffer[otherInputCount++] = inputBuffer[i];
                        break;
                }
            }

            // Rewrite unused ConsoleInput.
            WriteConsoleInput(stdInHandle, otherInputBuffer, otherInputCount, out uint _);

            foreach (var item in devices)
            {
                item.Update();
            }
        }
    }
}
