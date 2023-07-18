using System.Runtime.InteropServices;

namespace ConsoleInput.WinAPI
{
    [StructLayout(LayoutKind.Explicit)]
    public struct MOUSE_EVENT_RECORD : IInputRecord
    {
        [FieldOffset(0)]
        public COORD dwMousePosition;
        [FieldOffset(4)]
        public uint dwButtonState;
        [FieldOffset(8)]
        public uint dwControlKeyState;
        [FieldOffset(12)]
        public MouseEventFlags dwEventFlags;
    }
}
