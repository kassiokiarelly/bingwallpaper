using System;
using System.IO;
using System.Runtime.InteropServices;

namespace BingWallpaper.SystemInterop
{
    public class WindowsInterop : ISystemInterop
    {
        [Flags]
        private enum Spif
        {
            UpdateIniFile = 0x01
        }

        [Flags]
        private enum Action
        {
            SetDesktopWallpaper = 0x0014
        }

        // For setting a string parameter
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SystemParametersInfo(Action action, uint uiParam, string pvParam, Spif fWinIni);
        
        public bool SetWallpaper(string filePath)
        {
            if (File.Exists(filePath))
            {
                var actionResult = SystemParametersInfo(Action.SetDesktopWallpaper, 0, filePath, Spif.UpdateIniFile);
                return actionResult;
            }
            return false;
        }
    }
}