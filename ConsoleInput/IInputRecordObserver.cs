namespace ConsoleInput
{
    /// <summary>
    /// <see cref="T"/> has to be one of the following classes:
    /// <see cref="WinAPI.FOCUS_EVENT_RECORD"/>, <see cref="WinAPI.KEY_EVENT_RECORD"/>, <see cref="WinAPI.MOUSE_EVENT_RECORD"/>, <see cref="WinAPI.MENU_EVENT_RECORD"/> or <see cref="WinAPI.WINDOW_BUFFER_SIZE_RECORD"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IInputRecordObserver<T> : IDevice
        where T : WinAPI.IInputRecord
    {
        /// <summary>
        /// Receives a copy of with GetConsoleInput.
        /// Returns whether to return INPUT_RECORD to inputbuffer.
        /// </summary>
        /// <param name="inputRecord"></param>
        /// <returns></returns>
        bool HandleEvent(T inputRecord);
    }
}
