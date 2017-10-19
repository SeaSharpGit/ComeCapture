using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ComeCapture.Controls
{
    public class EllipseTool: StackPanel
    {
        static EllipseTool()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EllipseTool), new FrameworkPropertyMetadata(typeof(EllipseTool)));
        }

        public EllipseTool()
        {
            Current = this;
        }

        public static EllipseTool Current = null;

        #region LineThickness DependencyProperty
        public double LineThickness
        {
            get { return (double)GetValue(LineThicknessProperty); }
            set { SetValue(LineThicknessProperty, value); }
        }
        public static readonly DependencyProperty LineThicknessProperty =
                DependencyProperty.Register("LineThickness", typeof(double), typeof(EllipseTool),
                new PropertyMetadata(5.0, new PropertyChangedCallback(EllipseTool.OnLineThicknessPropertyChanged)));

        private static void OnLineThicknessPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is EllipseTool)
            {
                (obj as EllipseTool).OnLineThicknessValueChanged();
            }
        }

        protected void OnLineThicknessValueChanged()
        {

        }
        #endregion

        #region LineBrush DependencyProperty
        public SolidColorBrush LineBrush
        {
            get { return (SolidColorBrush)GetValue(LineBrushProperty); }
            set { SetValue(LineBrushProperty, value); }
        }
        public static readonly DependencyProperty LineBrushProperty =
                DependencyProperty.Register("LineBrush", typeof(SolidColorBrush), typeof(EllipseTool),
                new PropertyMetadata(new SolidColorBrush(Colors.Red), new PropertyChangedCallback(EllipseTool.OnLineBrushPropertyChanged)));

        private static void OnLineBrushPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is EllipseTool)
            {
                (obj as EllipseTool).OnLineBrushValueChanged();
            }
        }

        protected void OnLineBrushValueChanged()
        {

        }
        #endregion

    }
}
