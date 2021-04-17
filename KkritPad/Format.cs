using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KkritPad
{
    class Format
    {
        static Font _mainFont;

        public static Font MainFont { get; private set; }

        internal static void FontSetup(FontDialog fontDialog, TextBox textBox)
        {
            if(fontDialog.ShowDialog() == DialogResult.OK)
            {
                MainFont = fontDialog.Font;
            }
            textBox.Font = MainFont;
        }
    }
}
