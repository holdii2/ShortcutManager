using System;
using System.Runtime.InteropServices;

namespace ShortcutManager.Classes
{
    public static class ExternalFunctions
    {
        [DllImport("User32.dll")]
        public static extern bool RegisterHotKey(
            [In] IntPtr hWnd,
            [In] int id,
            [In] uint fsModifiers,
            [In] uint vk);

        [DllImport("User32.dll")]
        public static extern bool UnregisterHotKey(
            [In] IntPtr hWnd,
            [In] int id);
    }
}