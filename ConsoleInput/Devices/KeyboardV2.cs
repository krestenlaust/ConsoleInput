using ConsoleInput.Logic;
using ConsoleInput.WinAPI;
using System;

namespace ConsoleInput.Devices
{
    public class KeyboardV2 : IDevice, IInputRecordObserver<KEY_EVENT_RECORD>, IButtonDevice<KeyboardButton>
    {
        DataFlipFlopArray Dff;

        public KeyboardV2()
        {
            Dff = new DataFlipFlopArray(Enum.GetNames(typeof(KeyboardButton)).Length);
        }

        public bool IsButtonDown(KeyboardButton button) => Dff.Signals[GetIndex(button)];

        public bool IsButtonPressed(KeyboardButton button) => Dff.IsRisingEdge(GetIndex(button));

        public bool IsButtonReleased(KeyboardButton button) => Dff.IsFallingEdge(GetIndex(button));

        public void Update()
        {
            Dff.ClockSignal();
        }

        int GetIndex(KeyboardButton button) => GetIndexFromEnum(button.GetType(), button);

        KeyboardButton GetButton(int index) => (KeyboardButton)GetEnumFromIndex(typeof(KeyboardButton), index);

        static int GetIndexFromEnum(Type enumType, object enumValue) => Array.IndexOf(Enum.GetValues(enumType), enumValue);

        static object GetEnumFromIndex(Type enumType, int index) => Enum.GetValues(enumType).GetValue(index);

        /// <summary>
        /// Called internally by <c>InputManager</c> when new keyboard events are registered. Updates keyboard state accordingly.
        /// </summary>
        /// <param name="keyEvent">The key event retrived by ReadConsoleInput.</param>
        public bool HandleEvent(KEY_EVENT_RECORD keyEvent)
        {
            Dff.Signals[GetIndex((KeyboardButton)keyEvent.wVirtualKeyCode)] = keyEvent.bKeyDown;

            return false;
        }
    }
}
