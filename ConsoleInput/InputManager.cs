using System;
using System.Collections.Generic;
using ConsoleInput.WinAPI;
using static ConsoleInput.WinAPI.InputEventHandling;

namespace ConsoleInput
{
    public class InputManager : IInputManager
    {
        readonly ICollection<IDevice> devices = new List<IDevice>();
        readonly IntPtr stdInHandle;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputManager"/> class.
        /// </summary>
        public InputManager()
        {
            stdInHandle = GetStdHandle(STD_INPUT_HANDLE);
        }

        /// <summary>
        /// Adds the input device, and recalculates the preferred console mode.
        /// </summary>
        /// <param name="device"></param>
        public void AddDevice(IDevice device)
        {
            devices.Add(device);

            if (device is IRequireConsoleMode)
            {
                ReconfigureConsoleMode();
            }
        }

        /// <summary>
        /// Adds the input device, and recalculates the preferred console mode.
        /// </summary>
        /// <param name="device"></param>
        public void RemoveDevice(IDevice device)
        {
            devices.Remove(device);

            if (device is IRequireConsoleMode)
            {
                ReconfigureConsoleMode();
            }
        }

        /// <inheritdoc/>
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
                foreach (var device in devices)
                {
                    
                }
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

        /// <summary>
        /// Calculates the new console mode by all the devices with requirements.
        /// </summary>
        void ReconfigureConsoleMode()
        {
            uint aggregatedConsoleMode = 0;

            foreach (var device in devices)
            {
                if (device is IRequireConsoleMode consoleModeGetter)
                {
                    aggregatedConsoleMode |= consoleModeGetter.GetConsoleMode();
                }
            }

            SetConsoleMode(stdInHandle, aggregatedConsoleMode);
        }
    }
}
