using ComeCapture.Controls;
using ComeCapture.Helpers;
using ComeCapture.Models;

namespace ComeCapture
{
    public class AppModel : EntityBase
    {
        #region 属性 Current
        private static AppModel _Current = null;
        public static AppModel Current
        {
            get
            {
                if (_Current == null)
                {
                    _Current = new AppModel();
                }
                return _Current;
            }
            set
            {
               _Current = value;
            }
        }
        #endregion

        #region 属性 MaskLeftWidth
        private double _MaskLeftWidth = MainWindow.ScreenWidth;
        public double MaskLeftWidth
        {
            get
            {
                return _MaskLeftWidth;
            }
            set
            {
                _MaskLeftWidth = value;
                ShowSizeLeft = value;
                RaisePropertyChanged(() => MaskLeftWidth);
            }
        }
        #endregion

        #region 属性 MaskRightWidth
        private double _MaskRightWidth = 0;
        public double MaskRightWidth
        {
            get
            {
                return _MaskRightWidth;
            }
            set
            {
                _MaskRightWidth = value;
                RaisePropertyChanged(() => MaskRightWidth);
            }
        }
        #endregion

        #region 属性 MaskTopWidth
        private double _MaskTopWidth = 0;
        public double MaskTopWidth
        {
            get
            {
                return _MaskTopWidth;
            }
            set
            {
                _MaskTopWidth = value;
                RaisePropertyChanged(() => MaskTopWidth);
            }
        }
        #endregion

        #region 属性 MaskTopHeight
        private double _MaskTopHeight = 0;
        public double MaskTopHeight
        {
            get
            {
                return _MaskTopHeight;
            }
            set
            {
                _MaskTopHeight = value;
                ShowSizeTop = MaskTopHeight < 40 ? MaskTopHeight : MaskTopHeight - 40;
                RaisePropertyChanged(() => MaskTopHeight);
            }
        }
        #endregion

        #region 属性 MaskBottomHeight
        private double _MaskBottomHeight = 0;
        public double MaskBottomHeight
        {
            get
            {
                return _MaskBottomHeight;
            }
            set
            {
                _MaskBottomHeight = value;
                RaisePropertyChanged(() => MaskBottomHeight);
            }
        }
        #endregion

        #region 属性 ShowSize
        private string _ShowSize = "0 × 0";
        public string ShowSize
        {
            get
            {
                return _ShowSize;
            }
            set
            {
                _ShowSize = value;
                RaisePropertyChanged(() => ShowSize);
            }
        }

        public void ChangeShowSize()
        {
            ShowSize = (int)MainImage.Current.Width + " × " + (int)MainImage.Current.Height;
        }
        #endregion

        #region 属性 ShowSizeLeft
        private double _ShowSizeLeft = 0;
        public double ShowSizeLeft
        {
            get
            {
                return _ShowSizeLeft;
            }
            set
            {
                _ShowSizeLeft = value;
                RaisePropertyChanged(() => ShowSizeLeft);
            }
        }
        #endregion

        #region 属性 ShowSizeTop
        private double _ShowSizeTop = 0;
        public double ShowSizeTop
        {
            get
            {
                return _ShowSizeTop;
            }
            set
            {
                _ShowSizeTop = value;
                RaisePropertyChanged(() => ShowSizeTop);
            }
        }
        #endregion

        #region 属性 ShowRGB
        private string _ShowRGB = string.Empty;
        public string ShowRGB
        {
            get
            {
                return _ShowRGB;
            }
            set
            {
                _ShowRGB = value;
                RaisePropertyChanged(() => ShowRGB);
            }
        }

        public void SetRGB(System.Windows.Point point)
        {
            ShowRGB = ImageHelper.GetRGB(point);
        }
        #endregion

    }
}
