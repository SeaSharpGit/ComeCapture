using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ComeCapture.Service
{
    public class KeyboardHook
    {
        #region 属性 Current
        private static KeyboardHook _Current = null;
        public static KeyboardHook Current
        {
            get
            {
                if (_Current == null)
                {
                    _Current = new KeyboardHook();
                }
                return _Current;
            }
            set
            {
                _Current = value;
            }
        }
        #endregion

        #region 开启键盘钩子
        public static void Open()
        {
            Debug.WriteLine("开启键盘钩子");
            //启动键盘钩子   
            if (hKeyboardHook == 0)
            {
                KeyboardHookProcedure = new HookProc(Current.KeyboardHookProc);
                Process curProcess = Process.GetCurrentProcess();
                ProcessModule curModule = curProcess.MainModule;
                var intPtr = GetModuleHandle(curModule.ModuleName);
                hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProcedure, intPtr, 0);
            }
        }
        #endregion

        #region 捕获热键
        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN)
            {
                var MyKeyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
                //组合键：Alt+A
                if (MyKeyboardHookStruct.vkCode == 65 &&
                   (int)System.Windows.Forms.Control.ModifierKeys == (int)System.Windows.Forms.Keys.Alt)
                {
                    if (MainWindow.Current == null)
                    {
                        var win = new MainWindow();
                        win.Show();
                    }
                }
            }
            return 0;
        }
        #endregion

        #region 关闭键盘钩子
        public void Close()
        {
            if (hKeyboardHook != 0)
            {
                UnhookWindowsHookEx(hKeyboardHook);
                hKeyboardHook = 0;
            }
        }
        #endregion

        #region 键盘常量
        //装置钩子的函数   
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        //卸下钩子的函数   
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        //获取某个进程的句柄函数  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        /// 声明回调函数委托
        public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);
        /// 委托实例
        private static HookProc KeyboardHookProcedure;
        /// 键盘钩子句柄
        static int hKeyboardHook = 0;
        /// 普通按键消息
        private const int WM_KEYDOWN = 0x100;
        /// 系统按键消息
        private const int WM_SYSKEYDOWN = 0x104;
        //鼠标常量   
        public const int WH_KEYBOARD_LL = 13;

        //键盘钩子的结构
        [StructLayout(LayoutKind.Sequential)]
        public class KeyboardHookStruct
        {
            public int vkCode; //表示一个在1到254间的虚似键盘码   
            public int scanCode; //表示硬件扫描码   
            public int flags;
            public int time;
            public int dwExtraInfo;
        }
        #endregion

    }
}
