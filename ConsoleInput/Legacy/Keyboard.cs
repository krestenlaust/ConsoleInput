namespace ConsoleInput
{
    using System;
    using System.Collections.Generic;
    using ConsoleInput.WinAPI;

    /// <summary>
    /// Contains logic for handling keyboard states based on event data.
    /// </summary>
    [Obsolete("This API isn't supported any longer. Please use new InputManager and KeyboardV2 API.")]
    public static partial class Keyboard
    {
        static Dictionary<VirtualKey, Keystate> stateCurrent = new Dictionary<VirtualKey, Keystate>();
        static Dictionary<VirtualKey, Keystate> statePrevious = new Dictionary<VirtualKey, Keystate>();

        static Dictionary<VirtualKey, bool> keyboardKeyDown = new Dictionary<VirtualKey, bool>();
        // private static Dictionary<VK, ushort> keyboardRepeatCount = new Dictionary<VK, ushort>();

        internal enum Keystate
        {
            Down,
            Up,
            Press,
        }

        /// <summary>
        /// Returns true if <c>key</c> is held down.
        /// </summary>
        /// <param name="key">The key to lookup.</param>
        /// <returns>State of keydown.</returns>
        public static bool KeyDown(VirtualKey key)
        {
            if (!stateCurrent.TryGetValue(key, out Keystate state))
                return false;

            return state == Keystate.Down || state == Keystate.Press;
        }

        /// <summary>
        /// Returns true if <c>key</c> is just released.
        /// </summary>
        /// <param name="key">The key to lookup.</param>
        /// <returns>State of keyup.</returns>
        public static bool KeyUp(VirtualKey key)
        {
            if (!stateCurrent.TryGetValue(key, out Keystate state))
            {
                return false;
            }

            return state == Keystate.Up;
        }

        /// <summary>
        /// Returns true during the first update when the key was pressed, then false.
        /// </summary>
        /// <param name="key">The key to lookup.</param>
        /// <returns>State of keypress.</returns>
        public static bool KeyPress(VirtualKey key)
        {
            if (!stateCurrent.TryGetValue(key, out Keystate state))
            {
                return false;
            }

            return state == Keystate.Press;
        }

        /// <summary>
        /// Called internally by <c>Input</c> update button states.
        /// </summary>
        internal static void Update()
        {
            stateCurrent = new Dictionary<VirtualKey, Keystate>();

            foreach (KeyValuePair<VirtualKey, bool> item in keyboardKeyDown)
            {
                if (statePrevious.TryGetValue(item.Key, out Keystate prevState))
                {
                    // key down
                    if (item.Value)
                    {
                        // key is down now and was pressed previously or just pressed
                        if (prevState == Keystate.Press || prevState == Keystate.Up)
                        {
                            stateCurrent[item.Key] = Keystate.Down;
                        }
                        else
                        {
                            stateCurrent[item.Key] = Keystate.Down;
                        }
                    }
                    else
                    {
                        if (prevState == Keystate.Up)
                        {
                            stateCurrent.Remove(item.Key);
                        }
                        else
                        {
                            stateCurrent[item.Key] = Keystate.Up;
                        }
                    }
                }

                // Key was not held down before but is now.
                else if (item.Value)
                {
                    stateCurrent[item.Key] = Keystate.Press;
                }
            }

            keyboardKeyDown = new Dictionary<VirtualKey, bool>();
            statePrevious = new Dictionary<VirtualKey, Keystate>(stateCurrent);
        }

        /// <summary>
        /// Called internally by <c>Input</c> when new keyboard events are registered. Updates keyboard state accordingly.
        /// </summary>
        /// <param name="keyEvent">The key event retrived by ReadConsoleInput.</param>
        internal static void HandleKeyboardEvent(KEY_EVENT_RECORD keyEvent)
        {
            /*
            if (keyEvent.bKeyDown)
            {
                if (keyboardRepeatCount.TryGetValue(keyEvent.wVirtualKeyCode, out ushort value))
                {
                    keyboardRepeatCount[keyEvent.wVirtualKeyCode] = (ushort)(keyEvent.wRepeatCount + value);
                }
                else
                {
                    keyboardRepeatCount[keyEvent.wVirtualKeyCode] = keyEvent.wRepeatCount;
                }
            }*/

            keyboardKeyDown[(VirtualKey)keyEvent.wVirtualKeyCode] = keyEvent.bKeyDown;
        }
    }
}
