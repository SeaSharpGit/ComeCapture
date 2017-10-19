using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComeCapture.Controls
{
    public class ImageEditor : Control
    {
        public enum EditTool
        {
            Select,
            Rectangle,
            Line,
            Text
        }

        static ImageEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageEditor),
                new FrameworkPropertyMetadata(typeof(ImageEditor)));
        }

        #region LineColor DependencyProperty
        public Color LineColor
        {
            get { return (Color)GetValue(LineColorProperty); }
            set { SetValue(LineColorProperty, value); }
        }
        public static readonly DependencyProperty LineColorProperty =
                DependencyProperty.Register("LineColor", typeof(Color), typeof(ImageEditor),
                new PropertyMetadata(Colors.Black, new PropertyChangedCallback(ImageEditor.OnLineColorPropertyChanged)));

        private static void OnLineColorPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is ImageEditor)
            {
                (obj as ImageEditor).OnLineColorValueChanged();
            }
        }

        protected void OnLineColorValueChanged()
        {

        }
        #endregion

        #region LineThickness DependencyProperty
        public double LineThickness
        {
            get { return (double)GetValue(LineThicknessProperty); }
            set { SetValue(LineThicknessProperty, value); }
        }
        public static readonly DependencyProperty LineThicknessProperty =
                DependencyProperty.Register("LineThickness", typeof(double), typeof(ImageEditor),
                new PropertyMetadata(1.0, new PropertyChangedCallback(ImageEditor.OnLineThicknessPropertyChanged)));

        private static void OnLineThicknessPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is ImageEditor)
            {
                (obj as ImageEditor).OnLineThicknessValueChanged();
            }
        }

        protected void OnLineThicknessValueChanged()
        {

        }
        #endregion

        #region Tool DependencyProperty
        public EditTool Tool
        {
            get { return (EditTool)GetValue(ToolProperty); }
            set { SetValue(ToolProperty, value); }
        }
        public static readonly DependencyProperty ToolProperty =
                DependencyProperty.Register("Tool", typeof(EditTool), typeof(ImageEditor),
                new PropertyMetadata(EditTool.Select, new PropertyChangedCallback(ImageEditor.OnToolPropertyChanged)));

        private static void OnToolPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is ImageEditor)
            {
                (obj as ImageEditor).OnToolValueChanged();
            }
        }

        protected void OnToolValueChanged()
        {

        }
        #endregion


    }

   
}
