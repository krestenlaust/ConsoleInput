using System;
using ConsoleInput.Logic;
using ConsoleInput.WinAPI;

namespace ConsoleInput.Devices
{
    /// <summary>
    /// A device representing the physical keyboard. Handles keyboard events forwarded through the console input system.
    /// </summary>
    public class ConsoleKeyboard : IDevice, IInputRecordObserver<KEY_EVENT_RECORD>, IButtonDevice<KeyboardButton>
    {
        readonly DataFlipFlopArray dff;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleKeyboard"/> class.
        /// </summary>
        public ConsoleKeyboard()
        {
            dff = new DataFlipFlopArray(Enum.GetNames(typeof(KeyboardButton)).Length);
        }

        /// <inheritdoc/>
        public bool IsButtonDown(KeyboardButton button) => dff.Signals[GetIndex(button)];

        /// <inheritdoc/>
        public bool IsButtonPressed(KeyboardButton button) => dff.IsRisingEdge(GetIndex(button));

        /// <inheritdoc/>
        public bool IsButtonReleased(KeyboardButton button) => dff.IsFallingEdge(GetIndex(button));

        /// <inheritdoc/>
        public void Update()
        {
            dff.ClockSignal();
        }

        /// <summary>
        /// Called internally by <c>InputManager</c> when new keyboard events are registered. Updates keyboard state accordingly.
        /// </summary>
        /// <param name="keyEvent">The key event retrived by ReadConsoleInput.</param>
        /// <returns><inheritdoc/></returns>
        public bool HandleEvent(KEY_EVENT_RECORD keyEvent)
        {
            dff.Signals[GetIndex((KeyboardButton)keyEvent.wVirtualKeyCode)] = keyEvent.bKeyDown;

            return false;
        }

        static int GetIndexFromEnum(Type enumType, object enumValue) => Array.IndexOf(Enum.GetValues(enumType), enumValue);

        static object GetEnumFromIndex(Type enumType, int index) => Enum.GetValues(enumType).GetValue(index);

        int GetIndex(KeyboardButton button) => GetIndexFromEnum(button.GetType(), button);

        KeyboardButton GetButton(int index) => (KeyboardButton)GetEnumFromIndex(typeof(KeyboardButton), index);
    }
}
