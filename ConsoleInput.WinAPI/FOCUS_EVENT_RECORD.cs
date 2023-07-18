﻿using System.Runtime.InteropServices;

namespace ConsoleInput.WinAPI
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FOCUS_EVENT_RECORD : IInputRecord
    {
        public uint bSetFocus;
    }
}
