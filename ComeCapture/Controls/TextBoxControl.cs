using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ComeCapture.Controls
{
    [TemplatePart(Name = "PART_TextBox", Type = typeof(TextBox))]
    public class TextBoxControl : Control
    {
        public TextBox _TextBox;

        static TextBoxControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBoxControl), new FrameworkPropertyMetadata(typeof(TextBoxControl)));
        }

        public TextBoxControl()
        {
            AddHandler(GotFocusEvent, new RoutedEventHandler((sender, e) =>
            {
                MyFocus = true;
            }));
            AddHandler(LostFocusEvent, new RoutedEventHandler((sender, e) =>
            {
                MyFocus = false;
            }));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _TextBox = GetTemplateChild("PART_TextBox") as TextBox;
            _TextBox.MaxWidth = MainImage.Current.Width - MainImage.Current.point.X - 3;
            _TextBox.MaxHeight = MainImage.Current.Height - MainImage.Current.point.Y - 3;
        }

        #region BorderColor DependencyProperty
        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }
        public static readonly DependencyProperty BorderColorProperty =
                DependencyProperty.Register("BorderColor", typeof(Color), typeof(TextBoxControl),
                new PropertyMetadata(Colors.Transparent, new PropertyChangedCallback(TextBoxControl.OnBorderColorPropertyChanged)));

        private static void OnBorderColorPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is TextBoxControl)
            {
                (obj as TextBoxControl).OnBorderColorValueChanged();
            }
        }

        protected void OnBorderColorValueChanged()
        {

        }
        #endregion

        #region MyFocus DependencyProperty
        public bool MyFocus
        {
            get { return (bool)GetValue(MyFocusProperty); }
            set { SetValue(MyFocusProperty, value); }
        }
        public static readonly DependencyProperty MyFocusProperty =
                DependencyProperty.Register("MyFocus", typeof(bool), typeof(TextBoxControl),
                new PropertyMetadata(true, new PropertyChangedCallback(TextBoxControl.OnMyFocusPropertyChanged)));

        private static void OnMyFocusPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is TextBoxControl)
            {
                (obj as TextBoxControl).OnMyFocusValueChanged();
            }
        }

        protected void OnMyFocusValueChanged()
        {
            if (!MyFocus)
            {
                if (string.IsNullOrEmpty(_TextBox.Text))
                {
                    MainWindow.Current.MainCanvas.Children.Remove(this);
                }
                else
                {
                    MainImage.Current.ResetLimit(Canvas.GetLeft(this), Canvas.GetTop(this), (int)(Canvas.GetLeft(this) + ActualWidth), (int)(Canvas.GetTop(this) + ActualHeight));
                    MainWindow.Register(this);
                }
                MainImage.Current._Text = null;
            }
        }
        #endregion


    }
}
