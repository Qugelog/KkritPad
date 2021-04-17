using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KkritPad
{
    public static class Edit

    {
       static string _origin;

        public static string Origin
        {
            get { return _origin; }
            set { _origin = value; }

        }
        internal static void getDateTime(TextBox textBox)
        {
            textBox.Text += DateTime.Now.ToString("dd.MM.yyyy : hh:mm:ss");
        }

        internal static bool isEqualToOrigin(string textToCompare)
        {
            if (textToCompare == Origin) return true;
            return false;
        }

        #region Выделить всё
        internal static void selectAll(TextBox textBox)
        {
            textBox.SelectAll();
        }
        #endregion

        #region Удалить 
        internal static void Delete(TextBox textBox)
        {
            int start = textBox.SelectionStart;
            int finish = start + textBox.SelectionLength - 1;
            string newText = textBox.Text.Substring(0, start) + textBox.Text.Substring(finish + 1);

            textBox.Text = newText;
            textBox.Select(start, 0);

        }
        #endregion

    }
}
