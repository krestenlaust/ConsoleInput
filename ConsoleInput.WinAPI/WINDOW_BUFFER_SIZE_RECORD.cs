using System.Runtime.InteropServices;

namespace ConsoleInput.WinAPI
{
    public struct WINDOW_BUFFER_SIZE_RECORD
    {
        public COORD dwSize;

        public WINDOW_BUFFER_SIZE_RECORD(short x, short y)
        {
            dwSize = new COORD(x, y);
        }
    }
}
