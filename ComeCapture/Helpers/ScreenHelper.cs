using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ComeCapture.Helpers
{
    public static class ScreenHelper
    {
        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        public static extern IntPtr ReleaseDC(
            IntPtr hWnd,
            IntPtr hDc
            );

        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(
            IntPtr hdc, // handle to DC
            int nIndex // index of capability
            );

        public static System.Drawing.Size GetPhysicalDisplaySize()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            int physicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.Desktopvertres);
            int physicalScreenWidth = GetDeviceCaps(desktop, (int)DeviceCap.Desktophorzres);
            ReleaseDC(IntPtr.Zero, desktop);
            g.Dispose();
            return new System.Drawing.Size(physicalScreenWidth, physicalScreenHeight);
        }

        public enum DeviceCap
        {
            Desktopvertres = 117,
            Desktophorzres = 118
        }
    }
}
