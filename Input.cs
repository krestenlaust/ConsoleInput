using System;
using System.Runtime.InteropServices;

namespace ConsoleInput
{
    public static class Input
    {
        private const int STD_INPUT_HANDLE = -10;
        private const uint ENABLE_EXTENDED_FLAGS = 0x0080;
        private const uint ENABLE_MOUSE_INPUT = 0x0010;

        /*
        public enum VirtualKeys : ushort
        {
            LeftButton = 0x01,
            RightButton = 0x02,
            Cancel = 0x03,
            MiddleButton = 0x04,
            ExtraButton1 = 0x05,
            ExtraButton2 = 0x06,
            Back = 0x08,
            Tab = 0x09,
            Clear = 0x0C,
            Return = 0x0D,
            Shift = 0x10,
            Control = 0x11,
            Menu = 0x12,

            Pause = 0x13,

            CapsLock = 0x14,

            Kana = 0x15,

            Hangeul = 0x15,

            Hangul = 0x15,

            Junja = 0x17,

            Final = 0x18,

            Hanja = 0x19,

            Kanji = 0x19,

            Escape = 0x1B,

            Convert = 0x1C,

            NonConvert = 0x1D,

            Accept = 0x1E,

            ModeChange = 0x1F,

            Space = 0x20,

            Prior = 0x21,

            Next = 0x22,

            End = 0x23,

            Home = 0x24,

            Left = 0x25,

            Up = 0x26,

            Right = 0x27,

            Down = 0x28,

            Select = 0x29,

            Print = 0x2A,

            Execute = 0x2B,

            Snapshot = 0x2C,

            Insert = 0x2D,

            Delete = 0x2E,
            Help = 0x2F,
            N0 = 0x30,
            N1 = 0x31,
            N2 = 0x32,
            N3 = 0x33,
            N4 = 0x34,
            N5 = 0x35,
            N6 = 0x36,
            N7 = 0x37,
            N8 = 0x38,
            N9 = 0x39,
            A = 0x41,
            B = 0x42,
            C = 0x43,
            D = 0x44,
            E = 0x45,
            F = 0x46,
            G = 0x47,
            H = 0x48,
            I = 0x49,
            J = 0x4A,
            K = 0x4B,
            L = 0x4C,
            M = 0x4D,
            N = 0x4E,
            O = 0x4F,
            P = 0x50,
            Q = 0x51,
            R = 0x52,
            S = 0x53,
            T = 0x54,
            U = 0x55,
            V = 0x56,
            W = 0x57,
            X = 0x58,
            Y = 0x59,
            Z = 0x5A,
            LeftWindows = 0x5B,
            RightWindows = 0x5C,
            Application = 0x5D,
            Sleep = 0x5F,
            Numpad0 = 0x60,
            Numpad1 = 0x61,
            Numpad2 = 0x62,
            Numpad3 = 0x63,
            Numpad4 = 0x64,
            Numpad5 = 0x65,
            Numpad6 = 0x66,
            Numpad7 = 0x67,
            Numpad8 = 0x68,
            Numpad9 = 0x69,
            Multiply = 0x6A,
            Add = 0x6B,
            Separator = 0x6C,
            Subtract = 0x6D,
            Decimal = 0x6E,
            Divide = 0x6F,
            F1 = 0x70,
            F2 = 0x71,
            F3 = 0x72,
            F4 = 0x73,
            F5 = 0x74,
            F6 = 0x75,
            F7 = 0x76,
            F8 = 0x77,
            F9 = 0x78,
            F10 = 0x79,
            F11 = 0x7A,
            F12 = 0x7B,
            F13 = 0x7C,
            F14 = 0x7D,
            F15 = 0x7E,
            F16 = 0x7F,
            F17 = 0x80,
            F18 = 0x81,
            F19 = 0x82,
            F20 = 0x83,
            F21 = 0x84,
            F22 = 0x85,
            F23 = 0x86,
            F24 = 0x87,
            NumLock = 0x90,
            ScrollLock = 0x91,
            NEC_Equal = 0x92,

            Fujitsu_Jisho = 0x92,

            Fujitsu_Masshou = 0x93,

            Fujitsu_Touroku = 0x94,

            Fujitsu_Loya = 0x95,

            Fujitsu_Roya = 0x96,

            LeftShift = 0xA0,

            RightShift = 0xA1,

            LeftControl = 0xA2,

            RightControl = 0xA3,

            LeftMenu = 0xA4,

            RightMenu = 0xA5,

            BrowserBack = 0xA6,

            BrowserForward = 0xA7,

            BrowserRefresh = 0xA8,

            BrowserStop = 0xA9,

            BrowserSearch = 0xAA,

            BrowserFavorites = 0xAB,

            BrowserHome = 0xAC,

            VolumeMute = 0xAD,

            VolumeDown = 0xAE,

            VolumeUp = 0xAF,

            MediaNextTrack = 0xB0,

            MediaPrevTrack = 0xB1,

            MediaStop = 0xB2,

            MediaPlayPause = 0xB3,

            LaunchMail = 0xB4,

            LaunchMediaSelect = 0xB5,

            LaunchApplication1 = 0xB6,

            LaunchApplication2 = 0xB7,

            OEM1 = 0xBA,

            OEMPlus = 0xBB,

            OEMComma = 0xBC,

            OEMMinus = 0xBD,

            OEMPeriod = 0xBE,

            OEM2 = 0xBF,

            OEM3 = 0xC0,

            OEM4 = 0xDB,

            OEM5 = 0xDC,

            OEM6 = 0xDD,

            OEM7 = 0xDE,

            OEM8 = 0xDF,

            OEMAX = 0xE1,

            OEM102 = 0xE2,

            ICOHelp = 0xE3,

            ICO00 = 0xE4,

            ProcessKey = 0xE5,

            ICOClear = 0xE6,

            Packet = 0xE7,

            OEMReset = 0xE9,

            OEMJump = 0xEA,

            OEMPA1 = 0xEB,

            OEMPA2 = 0xEC,

            OEMPA3 = 0xED,

            OEMWSCtrl = 0xEE,

            OEMCUSel = 0xEF,

            OEMATTN = 0xF0,

            OEMFinish = 0xF1,

            OEMCopy = 0xF2,

            OEMAuto = 0xF3,

            OEMENLW = 0xF4,

            OEMBackTab = 0xF5,

            ATTN = 0xF6,

            CRSel = 0xF7,

            EXSel = 0xF8,

            EREOF = 0xF9,

            Play = 0xFA,

            Zoom = 0xFB,

            Noname = 0xFC,

            PA1 = 0xFD,

            OEMClear = 0xFE
        }
        */
        [StructLayout(LayoutKind.Sequential)]
        internal struct FOCUS_EVENT_RECORD
        {
            public uint bSetFocus;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct MENU_EVENT_RECORD
        {
            public uint dwCommandId;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct COORD
        {
            public short X;
            public short Y;

            public COORD(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        };
        internal struct WINDOW_BUFFER_SIZE_RECORD
        {
            public COORD dwSize;

            public WINDOW_BUFFER_SIZE_RECORD(short x, short y)
            {
                dwSize = new COORD(x, y);
            }
        }
        internal enum MouseEventFlags // https://docs.microsoft.com/en-us/windows/console/mouse-event-record-str
        {
            DOUBLE_CLICK = 0x0002,
            MOUSE_MOVED = 0x0001,
            MOUSE_HWHEELED = 0x0008,
            MOUSE_WHEELED = 0x0004
        }
        [StructLayout(LayoutKind.Explicit)]
        internal struct MOUSE_EVENT_RECORD
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
        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
        internal struct KEY_EVENT_RECORD
        {
            [FieldOffset(0), MarshalAs(UnmanagedType.Bool)]
            public bool bKeyDown;
            [FieldOffset(4), MarshalAs(UnmanagedType.U2)]
            public ushort wRepeatCount;
            [FieldOffset(6), MarshalAs(UnmanagedType.U2)]
            public Keyboard.VK wVirtualKeyCode;
            [FieldOffset(8), MarshalAs(UnmanagedType.U2)]
            public ushort wVirtualScanCode;
            [FieldOffset(10)]
            public char UnicodeChar;
            [FieldOffset(12), MarshalAs(UnmanagedType.U4)]
            public uint dwControlKeyState;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct INPUT_RECORD_UNION
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
        };
        internal enum InputEventType
        {
            FOCUS_EVENT = 0x0010,
            KEY_EVENT = 0x0001,
            MENU_EVENT = 0x0008,
            MOUSE_EVENT = 0x0002,
            WINDOW_BUFFER_SIZE_EVENT = 0x0004
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT_RECORD
        {
            public InputEventType EventType;
            public INPUT_RECORD_UNION Event;
        };

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetNumberOfConsoleInputEvents(IntPtr hConsoleInput, out uint lpcNumberOfEvents);
        [DllImport("kernel32.dll", EntryPoint = "ReadConsoleInputW", CharSet = CharSet.Unicode)]
        private static extern bool ReadConsoleInput(IntPtr hConsoleInput, [Out] INPUT_RECORD[] lpBuffer, uint nLength, out uint lpNumberOfEventsRead);
        [DllImport("kernel32.dll", EntryPoint = "WriteConsoleInputW", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool WriteConsoleInput(IntPtr hConsoleInput, INPUT_RECORD[] lpBuffer, uint nLength, out uint lpNumberOfEventsWritten);

        internal static IntPtr StdInHandle;

        public static bool IgnoreKeyboard;
        public static bool IgnoreMouse;

        public static void Setup(bool quickSelect = false)
        {
            StdInHandle = GetStdHandle(STD_INPUT_HANDLE);

            SetConsoleMode(StdInHandle, (quickSelect ? 0 : ENABLE_EXTENDED_FLAGS) | ENABLE_MOUSE_INPUT);
        }

        public static void Update()
        {
            GetNumberOfConsoleInputEvents(StdInHandle, out uint numberOfEvents);
            INPUT_RECORD[] inputBuffer = new INPUT_RECORD[numberOfEvents];

            if (numberOfEvents > 0)
            {
                ReadConsoleInput(StdInHandle, inputBuffer, numberOfEvents, out numberOfEvents);
            }

            INPUT_RECORD[] otherInputBuffer = new INPUT_RECORD[numberOfEvents];
            uint otherInputCount = 0;

            for (int i = 0; i < numberOfEvents; i++)
            {
                switch (inputBuffer[i].EventType)
                {
                    case InputEventType.KEY_EVENT when !IgnoreKeyboard:
                        Keyboard.HandleKeyboardEvent(inputBuffer[i].Event.KeyEvent);
                        break;
                    case InputEventType.MOUSE_EVENT when !IgnoreMouse:
                        Mouse.HandleMouseEvent(inputBuffer[i].Event.MouseEvent);
                        break;
                    default:
                        otherInputBuffer[otherInputCount++] = inputBuffer[i];
                        break;
                }
            }

            WriteConsoleInput(StdInHandle, otherInputBuffer, otherInputCount, out uint _);

            Mouse.Update();
            Keyboard.Update();
        }

    }
}
