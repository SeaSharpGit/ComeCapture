using ComeCapture.Controls;

namespace ComeCapture.Models
{
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
            Limit = MainImage.Current.Limit.Clone();
        }
    }
}
