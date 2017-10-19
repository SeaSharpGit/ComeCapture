namespace ComeCapture.Models
{
    /// <summary>
    /// 图片移动的极限值
    /// </summary>
    public class Limit
    {
        public double Left { get; set; } = MainWindow.ScreenWidth;
        public double Right { get; set; } = 0;
        public double Top { get; set; } = MainWindow.ScreenHeight;
        public double Bottom { get; set; } = 0;

        public Limit Clone()
        {
            return MemberwiseClone() as Limit;
        }

        public Limit()
        {

        }
    }

    
}
