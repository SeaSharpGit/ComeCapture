using ComeCapture.Controls;

namespace ComeCapture.Models
{
    /// <summary>
    /// 图片移动的极限值
    /// </summary>
    public class Limit
    {
        public int Left { get; set; } = MainWindow.ScreenWidth;
        public int Right { get; set; } = 0;
        public int Top { get; set; } = MainWindow.ScreenHeight;
        public int Bottom { get; set; } = 0;

        public Limit Clone()
        {
            return MemberwiseClone() as Limit;
        }

        public Limit()
        {

        }
    }

    /// <summary>
    /// 注册名称以及对应图片极限值
    /// </summary>
    public class NameAndLimit
    {
        public string Name { get; set; }
        public Limit Limit { get; set; }

        public NameAndLimit(string name)
        {
            Name = name;
            Limit = MainWindow.Current.MainImage.Limit.Clone();
        }
    }
}
