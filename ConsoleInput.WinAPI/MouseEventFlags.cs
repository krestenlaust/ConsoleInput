namespace ConsoleInput.WinAPI
{
    public enum MouseEventFlags // https://docs.microsoft.com/en-us/windows/console/mouse-event-record-str
    {
        DOUBLE_CLICK = 0x0002,
        MOUSE_MOVED = 0x0001,
        MOUSE_HWHEELED = 0x0008,
        MOUSE_WHEELED = 0x0004,
    }
}
