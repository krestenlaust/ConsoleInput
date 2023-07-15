using System.Runtime.InteropServices;

namespace ConsoleInput.WinAPI
{
    [StructLayout(LayoutKind.Explicit)]
    public struct INPUT_RECORD_UNION
    {
        [FieldOffset(0)]
        public KEY_EVENT_RECORD KeyEvent;
        [FieldOffset(0)]
        public MOUSE_EVENT_RECORD MouseEvent;
        [FieldOffset(0)]
        public WINDOW_BUFFER_SIZE_RECORD WindowBufferSizeEvent;
        [FieldOffset(0)]
        public MENU_EVENT_RECORD MenuEvent;
        [FieldOffset(0)]
        public FOCUS_EVENT_RECORD FocusEvent;
    }
}
