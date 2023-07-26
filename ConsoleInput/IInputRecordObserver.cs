namespace ConsoleInput
{
    /// <summary>
    /// Represents a device which utilizes console input-event polling.
    /// </summary>
    /// <typeparam name="T">Type-parameter has to be one of the following classes: <see cref="WinAPI.FOCUS_EVENT_RECORD"/>, <see cref="WinAPI.KEY_EVENT_RECORD"/>, <see cref="WinAPI.MOUSE_EVENT_RECORD"/>, <see cref="WinAPI.MENU_EVENT_RECORD"/> or <see cref="WinAPI.WINDOW_BUFFER_SIZE_RECORD"/>.</typeparam>
    public interface IInputRecordObserver<T> : IDevice
        where T : WinAPI.IInputRecord
    {
        /// <summary>
        /// Receives a copy of with GetConsoleInput.
        /// </summary>
        /// <param name="inputRecord">An instance representing console event data.</param>
        /// <returns><c>true</c> if the event should be pushed back into the event buffer; otherwise, <c>false</c>.</returns>
        bool HandleEvent(T inputRecord);
    }
}
