﻿using System.Runtime.InteropServices;

namespace ConsoleInput.WinAPI
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MENU_EVENT_RECORD : IInputRecord
    {
        public uint dwCommandId;
    }
}
