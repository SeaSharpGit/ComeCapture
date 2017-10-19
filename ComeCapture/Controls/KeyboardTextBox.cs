using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ComeCapture.Controls
{
    public class KeyboardTextBox : Control
    {
        static KeyboardTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(KeyboardTextBox), new FrameworkPropertyMetadata(typeof(KeyboardTextBox)));
        }

        public KeyboardTextBox()
        {
            AddHandler(PreviewKeyDownEvent, new KeyEventHandler(Keyboard_KeyDown));
            DataContext = AppModel.Current;
        }

        /// <summary>
        /// 只允许输入数字以及字母
        /// </summary>
        private void Keyboard_KeyDown(object sender, KeyEventArgs e)
        {
            var key = (int)e.Key;
            var textbox = e.OriginalSource as TextBox;
            //A-Z
            if (key >= 44 && key <= 69)
            {
                textbox.Text = e.Key.ToString();
                textbox.Select(1, 0);
            }
            else if (key >= 34 && key <= 43) //0 - 9,
            {
                textbox.Text = e.Key.ToString().Substring(1);
                textbox.Select(1, 0);
            }
            e.Handled = true;
        }

        
    }
}
