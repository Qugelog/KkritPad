using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KkritPad
{
    #region Работа с файлами
    class File
    {
        static string _fileName = "Безымянный";
        static StreamReader reader;
        static StreamWriter writer;
        static string _fileContent;
        static bool _isChanged;

        public static string FileName 
        { 
            get { return _fileName; }
            set { _fileName = value; }
        }

        public static string FileContent 
        { 
            get { return _fileContent;  }
            set { _fileContent = value; }
        }

        public static bool isChanged
        {
            get { return _isChanged; }
            set { _isChanged = value; }
        }

        #region Создать файл
        internal static void newFile(TextBox textBox, SaveFileDialog saveFileDialog)
        {
            if (!isChanged)
            {
                textBox.Clear();
                Edit.Origin = "";
                return;
            }
            ConfirmForm confirmForm = new ConfirmForm();
            DialogResult answer = confirmForm.ShowDialog();

            switch(answer)
            {
                case DialogResult.Cancel:
                    break;
                case DialogResult.No:
                    textBox.Clear();
                    Edit.Origin = "";
                    break;
                case DialogResult.Yes:
                    saveFile(saveFileDialog, textBox);
                    break;
            }

        }
        #endregion

        #region Новое окно
        internal static void newWindow() 
        {
            Main mainForm = new Main();
            mainForm.Show();
        }
        #endregion

        #region Открыть файл
        internal static string openFile(OpenFileDialog openFileDialog, TextBox textBox) 
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = openFileDialog.FileName;
                reader = new StreamReader(FileName);
                FileContent = reader.ReadToEnd();
                textBox.Text = FileContent;

                reader.Close();
            }


            return FileName;
        }
        #endregion

        #region Сохранить файл
        internal static void saveFile(SaveFileDialog saveFileDialog, TextBox textBox) 
        {
            if (FileName == "" || FileName == "Безымянный") saveAs(saveFileDialog, textBox);
            else
            {
                save(FileName, textBox);
            }
        }
        #endregion

        #region Сохранить как
        internal static void saveAs(SaveFileDialog saveFileDialog, TextBox textBox) 
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = saveFileDialog.FileName;
                save(FileName, textBox);

            }

        }
        #endregion



        #region Не лезь, убьет!
        static void save(string fileName, TextBox textBox)
        {
            using (writer = new StreamWriter(fileName))
            {
                writer.WriteLine(textBox.Text);
            }
            Edit.Origin = textBox.Text;
            isChanged = false;
        }
        #endregion
    }
    #endregion

    #region Печать файла 
    public static class Printing
    {
        internal static void PageSetup(PageSetupDialog pageSetupDialog)
        {
            pageSetupDialog.ShowDialog();
        }

        internal static void Print(PrintDialog printDialog)
        {
            if(printDialog.ShowDialog() == DialogResult.OK)
            {
                printDialog.ShowDialog();
            }

        }

    }
    #endregion
}
