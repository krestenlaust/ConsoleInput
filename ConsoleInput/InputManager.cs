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
            foreach (var item in devices)
            {
                item.Update();
            }

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
                bool rebufferEvent = false;

                // TODO: Storing IInputRecordObservers as they are added to the manager, instead of looking every time.
                foreach (var device in devices)
                {
                    bool? rebuffer = null;

                    switch (inputBuffer[i].EventType)
                    {
                        case InputEventType.KEY_EVENT when device is IInputRecordObserver<KEY_EVENT_RECORD> keyObserver:
                            rebuffer = keyObserver.HandleEvent(inputBuffer[i].Event.KeyEvent);
                            break;
                        case InputEventType.MOUSE_EVENT when device is IInputRecordObserver<MOUSE_EVENT_RECORD> mouseObserver:
                            rebuffer = mouseObserver.HandleEvent(inputBuffer[i].Event.MouseEvent);
                            break;
                        case InputEventType.MENU_EVENT when device is IInputRecordObserver<MENU_EVENT_RECORD> menuObserver:
                            rebuffer = menuObserver.HandleEvent(inputBuffer[i].Event.MenuEvent);
                            break;
                        case InputEventType.WINDOW_BUFFER_SIZE_EVENT when device is IInputRecordObserver<WINDOW_BUFFER_SIZE_RECORD> windowBuffersizeObserver:
                            rebuffer = windowBuffersizeObserver.HandleEvent(inputBuffer[i].Event.WindowBufferSizeEvent);
                            break;
                        case InputEventType.FOCUS_EVENT when device is IInputRecordObserver<FOCUS_EVENT_RECORD> focusObserver:
                            rebuffer = focusObserver.HandleEvent(inputBuffer[i].Event.FocusEvent);
                            break;
                    }

                    if (rebuffer == true)
                    {
                        rebufferEvent = true;
                    }
                }

                if (rebufferEvent)
                {
                    otherInputBuffer[otherInputCount++] = inputBuffer[i];
                }
            }

            // Rewrite unused ConsoleInput.
            WriteConsoleInput(stdInHandle, otherInputBuffer, otherInputCount, out uint _);
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
