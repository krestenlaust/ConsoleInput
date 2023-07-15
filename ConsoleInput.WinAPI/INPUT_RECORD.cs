using System.Runtime.InteropServices;

namespace ConsoleInput.WinAPI
{
    [StructLayout(LayoutKind.Sequential)]
    public struct INPUT_RECORD
    {
        public InputEventType EventType;
        public INPUT_RECORD_UNION Event;
    }
}
