using ComeCapture.Controls;
using ComeCapture.Helpers;
using ComeCapture.Models;
using System.Text;

namespace ComeCapture
{
    public class AppModel : EntityBase
    {
        public AppModel()
        {
            _Current = this;
        }

        #region 属性 Current
        private static AppModel _Current = null;
        public static AppModel Current
        {
            get
            {
                return _Current;
            }
        }
        #endregion

        #region 属性 MaskLeftWidth
        private int _MaskLeftWidth = MainWindow.ScreenWidth;
        public int MaskLeftWidth
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
        private int _MaskRightWidth = 0;
        public int MaskRightWidth
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
        private int _MaskTopWidth = 0;
        public int MaskTopWidth
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
        private int _MaskTopHeight = 0;
        public int MaskTopHeight
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
        private int _MaskBottomHeight = 0;
        public int MaskBottomHeight
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

        private static StringBuilder sb = new StringBuilder();
        public void ChangeShowSize()
        {
            sb.Clear();
            sb.Append((int)MainImage.Current.Width);
            sb.Append(" × ");
            sb.Append((int)MainImage.Current.Height);
            ShowSize = sb.ToString();
        }
        #endregion

        #region 属性 ShowSizeLeft
        private int _ShowSizeLeft = 0;
        public int ShowSizeLeft
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
        private int _ShowSizeTop = 0;
        public int ShowSizeTop
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
