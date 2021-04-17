using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KkritPad
{
    public partial class Main : Form
    {
        string _title = "Безымянный - KkritPad";
        public Main()
        {
            InitializeComponent();
            this.Text = this.Title;
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string path = File.openFile(openFileDialog, textBox);

            if (path != "")
            {
                string[] pathElements = path.Split('\\');
                Title = pathElements[pathElements.Length - 1] + "KKRIT_PAD";
                this.Text = Title;

                // Сохраняем копию оригинала
                Edit.Origin = textBox.Text;
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void getDateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Edit.getDateTime(textBox);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.saveAs(saveFileDialog, textBox);
            removeChangesMaker();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            File.isChanged = !Edit.isEqualToOrigin(textBox.Text);
            if (File.isChanged) this.Text = "*" + this.Title;
        }

        private void saveFile_Click(object sender, EventArgs e)
        {
            File.saveFile(saveFileDialog, textBox);
            removeChangesMaker();
        }

        private void removeChangesMaker()
        {
            if (!File.isChanged)
            {
                this.Text = this.Title;
            }
        }

        private void createFile_Click(object sender, EventArgs e)
        {
            File.newFile(textBox, saveFileDialog);
            removeChangesMaker();
        }

        private void newWindow_Click(object sender, EventArgs e)
        {
            File.newWindow();
        }

        private void fontSettings_Click(object sender, EventArgs e)
        {
            Format.FontSetup(fontDialog, textBox);
        }

        private void printDoc_Click(object sender, PrintEventArgs e)
        {

        }

        private void printDoc_Click(object sender, EventArgs e)
        {
            Printing.Print(printDialog);
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            string toPrint = textBox.Text; // Вот этот текст мы печатаем
            Font font = Format.MainFont; // Наш шрифт

            e.Graphics.PageUnit = GraphicsUnit.Millimeter; // Всё делаем в миллиметрах

            /* 
             * Координаты верхнего левого угла
            */
            int leftMargin = pageSetupDialog.PageSettings.Margins.Left / 4;
            int rightMargin = pageSetupDialog.PageSettings.Margins.Right / 4;
            int topMargin = pageSetupDialog.PageSettings.Margins.Top / 4;

            SizeF textSize = new Size();
            if (!pageSetupDialog.PageSettings.Landscape) // Книжная ориентация 
            {
                /* 
                 * Измеряем размер строки в заданном шрифте. 
                 * 210мм - ширина A4 в книжной ориентации 
                */

                textSize = e.Graphics.MeasureString(toPrint, font, 210 - leftMargin - rightMargin);
            } 
            else
            {
                /* 
                 * Измеряем размер строки в заданном шрифте. 
                 * 297мм - ширина A4 в альбомной ориентации 
                */
                textSize = e.Graphics.MeasureString(toPrint, font, 297 - leftMargin - rightMargin);
            }

            // Кажется, тут не совсем хорошо... Надо будет подумать над этим
            e.Graphics.DrawString(toPrint, font, Brushes.Black, new RectangleF(new PointF(leftMargin, topMargin), textSize)); 
        }

        private void pageSetup_Click(object sender, EventArgs e)
        {
            Printing.PageSetup(pageSetupDialog);
        }

        private void wordWrap_Click(object sender, EventArgs e)
        {
            wordWarp.Checked = !wordWarp.Checked;
        }

        private void wordWarp_CheckStateChanged(object sender, EventArgs e)
        {
            if (wordWarp.CheckState == CheckState.Checked)
            {
                textBox.WordWrap = true;
                textBox.ScrollBars = ScrollBars.Vertical;
            }
            else
            {
                textBox.WordWrap = false;
                textBox.ScrollBars = ScrollBars.Both;
            }
        }

        private void undo_Click(object sender, EventArgs e)
        {
            textBox.Undo();
        }

        private void selectAll_Click(object sender, EventArgs e)
        {
            Edit.selectAll(textBox);
        }

        private void copyText_Click(object sender, EventArgs e)
        {
            textBox.Copy();
        }

        private void pasteText_Click(object sender, EventArgs e)
        {
            textBox.Paste();
        }

        private void removeText_Click(object sender, EventArgs e)
        {
            Edit.Delete(textBox);
        }
    }
}
