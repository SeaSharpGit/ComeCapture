using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using ComeCapture.Service;
using System.Windows.Media;
using System.Diagnostics;
using System.Windows.Controls;
using ComeCapture.Controls;
using System.Collections.Generic;
using ComeCapture.Models;
using System.Threading;

namespace ComeCapture
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public static double ScreenWidth = SystemParameters.PrimaryScreenWidth;
        public static double ScreenHeight = SystemParameters.PrimaryScreenHeight;
        public static int MinSize = 10;
        public static MainWindow Current = null;

        //画图注册名称集合
        public List<NameAndLimit> list = new List<NameAndLimit>();
        //画图注册名称
        public int num = 1;

        //是否截图开始
        private bool _IsMouseDown = false;
        //是否截图完毕
        private bool _IsCapture = false;
        //关闭时是否有返回值(Show/ShowDialog)
        private bool _IsShowDialog = false;
        //背景图（获取RGB时使用）
        public static Bitmap _Bitmap;

        private int _X0 = 0;
        private int _Y0 = 0;

        public MainWindow()
        {
            Current = this;
            InitializeComponent();
            DataContext = AppModel.Current;
            Left = 0;
            Top = 0;
            Width = ScreenWidth;
            Height = ScreenHeight;
            Background = new ImageBrush(GetFullBitmapSource());
            WpfHelper.MainDispatcher = Dispatcher;
            MainCanvas.Children.Add(MainImage.Current);
            MainCanvas.Children.Add(ImageEditBar.Current);
            MainCanvas.Children.Add(SizeColorBar.Current);
            IsVisibleChanged += (sender, e) =>
            {
                Activate();
            };
        }

        public new bool? ShowDialog()
        {
            _IsShowDialog = true;
            return base.ShowDialog();
        }

        #region 注册画图
        public static void Register(object control)
        {
            var name = "Draw" + Current.num.ToString();
            Current.MainCanvas.RegisterName(name, control);
            Current.list.Add(new NameAndLimit(name));
            Current.num++;
        }
        #endregion

        #region 撤销
        public void OnRevoke()
        {
            if (list.Count > 0)
            {
                var name = list[list.Count - 1].Name;
                var obj = MainCanvas.FindName(name);
                if (obj != null)
                {
                    MainCanvas.Children.Remove(obj as UIElement);
                    MainCanvas.UnregisterName(name);
                    list.RemoveAt(list.Count - 1);
                    MainImage.Current.Limit = list.Count == 0 ? new Limit() : list[list.Count - 1].Limit;
                }
            }
        }
        #endregion

        #region 保存
        public void OnSave()
        {
            var dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "截图" + DateTime.Now.ToString("yyyyMMddhhmmss");
            dlg.Filter = "png|*.png";
            dlg.AddExtension = true;
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == true)
            {
                Hidden();
                Thread t = new Thread(new ThreadStart(() =>
                {
                    Thread.Sleep(150);
                    WpfHelper.SafeRun(() =>
                    {
                        var source = GetBitmapSource();
                        if (source != null)
                        {
                            ImageHelper.SaveToPng(source, dlg.FileName);
                        }
                        Close();
                    });
                }));
                t.IsBackground = true;
                t.Start();
            }
        }
        #endregion

        #region 退出截图
        public void OnCancel()
        {
            Close();
        }
        #endregion

        #region 完成截图
        public void OnOK()
        {
            Hidden();
            Thread t = new Thread(new ThreadStart(() =>
            {
                Thread.Sleep(50);
                WpfHelper.SafeRun(() =>
                {
                    var source = GetBitmapSource();
                    if (source != null)
                    {
                        Clipboard.SetImage(source);
                    }
                    if (_IsShowDialog)
                    {
                        DialogResult = true;
                    }
                    else
                    {
                        Close();
                    }
                });
            }));
            t.IsBackground = true;
            t.Start();
        }
        #endregion

        #region 截图前隐藏窗口
        private void Hidden()
        {
            //隐藏尺寸RGB框
            if (AppModel.Current.MaskTopHeight < 40)
            {
                SizeRGB.Visibility = Visibility.Collapsed;
            }
            var need = SizeColorBar.Current.Selected == Tool.Null ? 30 : 67;
            if (AppModel.Current.MaskBottomHeight < need && AppModel.Current.MaskTopHeight < need)
            {
                ImageEditBar.Current.Visibility = Visibility.Collapsed;
                SizeColorBar.Current.Visibility = Visibility.Collapsed;
            }
            MainImage.Current.ZoomThumbVisibility = Visibility.Collapsed;
        }
        #endregion

        #region 鼠标及键盘事件
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_IsCapture)
            {
                return;
            }
            var point = e.GetPosition(this);
            _X0 = (int)point.X;
            _Y0 = (int)point.Y;
            _IsMouseDown = true;
            Canvas.SetLeft(MainImage.Current, _X0);
            Canvas.SetTop(MainImage.Current, _Y0);
            AppModel.Current.MaskLeftWidth = _X0;
            AppModel.Current.MaskRightWidth = ScreenWidth - _X0;
            AppModel.Current.MaskTopHeight = _Y0;
            Show_Size.Visibility = Visibility.Visible;
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!_IsMouseDown || _IsCapture)
            {
                return;
            }
            _IsMouseDown = false;
            if (MainImage.Current.Width >= MinSize && MainImage.Current.Height >= MinSize)
            {
                _IsCapture = true;
                ImageEditBar.Current.Visibility = Visibility.Visible;
                ImageEditBar.Current.ResetCanvas();
                Cursor = Cursors.Arrow;
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            var point = e.GetPosition(this);
            AppModel.Current.ChangeShowRGB(point);
            if (_IsCapture)
            {
                return;
            }
            if (Show_RGB.Visibility == Visibility.Collapsed)
            {
                Show_RGB.Visibility = Visibility.Visible;
            }

            if (_IsMouseDown)
            {
                var w = (int)point.X - _X0;
                var h = (int)point.Y - _Y0;
                if (w < MinSize || h < MinSize)
                {
                    return;
                }
                if (MainImage.Current.Visibility != Visibility.Visible)
                {
                    MainImage.Current.Visibility = Visibility.Visible;
                }
                AppModel.Current.MaskRightWidth = ScreenWidth - point.X;
                AppModel.Current.MaskTopWidth = w;
                AppModel.Current.MaskBottomHeight = ScreenHeight - point.Y;
                AppModel.Current.ChangeShowSize();
                MainImage.Current.Width = w;
                MainImage.Current.Height = h;
            }
            else
            {
                AppModel.Current.ShowSizeLeft = point.X;
                AppModel.Current.ShowSizeTop = ScreenHeight - point.Y < 30 ? point.Y - 30 : point.Y + 10;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
        #endregion

        #region 截图
        /// <summary>
        /// 截图区域截图
        /// </summary>
        public BitmapSource GetBitmapSource()
        {
            return GetBitmapSource((int)AppModel.Current.MaskLeftWidth + 1, (int)AppModel.Current.MaskTopHeight + 1, (int)MainImage.Current.ActualWidth - 2, (int)MainImage.Current.ActualHeight - 2);
        }

        /// <summary>
        /// 全屏截图
        /// </summary>
        public BitmapSource GetFullBitmapSource()
        {
            return GetBitmapSource(0, 0, Convert.ToInt32(ScreenWidth), Convert.ToInt32(ScreenHeight));
        }

        public static BitmapSource GetBitmapSource(int x, int y, int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                return null;
            }
            var bounds = GetPhysicalDisplaySize();
            var screenWidth = bounds.Width;
            var screenHeight = bounds.Height;

            var scaleWidth = (screenWidth * 1.0) / ScreenWidth;
            var scaleHeight = (screenHeight * 1.0) / ScreenHeight;

            var w = (int)(width * scaleWidth);
            var h = (int)(height * scaleHeight);
            var l = (int)(x * scaleWidth);
            var t = (int)(y * scaleHeight);

            _Bitmap = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            var bmpGraphics = Graphics.FromImage(_Bitmap);
            bmpGraphics.CopyFromScreen(l, t, 0, 0, _Bitmap.Size);
            return Imaging.CreateBitmapSourceFromHBitmap(
                _Bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }
        #endregion

        #region 屏幕分辨率
        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(
        IntPtr hdc, // handle to DC
        int nIndex // index of capability
        );

        static System.Drawing.Size GetPhysicalDisplaySize()
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
        #endregion


    }
}
