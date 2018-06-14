using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace FileManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


            FolderBrowserDialog path = new FolderBrowserDialog();

            if (path.ShowDialog() == DialogResult.OK)

            {
                textBoxpath.Text = path.SelectedPath.ToString();

            }

            foreach (var file in Directory.EnumerateFiles(path.SelectedPath))
            {

                BitmapMetadata md = null;
                FileInfo fileInfo = new FileInfo(file);
                textBoxlog.AppendText(file.ToString() + " ");

                try
                {
                    FileStream fs = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);
                    BitmapSource img = BitmapFrame.Create(fs);
                    md = (BitmapMetadata)img.Metadata;

                    


                    textBoxlog.AppendText(md.DateTaken);

                    DateTimeConverter converter = new DateTimeConverter();

                    var date = converter.ConvertFrom(md.DateTaken.ToString());

                    var data_string = date.ToString().Split(' ');

                     
                    DirectoryInfo di = Directory.CreateDirectory(path.SelectedPath.ToString() + "/" + data_string[0]);


                    fs.Close();
                    System.IO.File.Move(file.ToString(), di.FullName.ToString() + "/" + fileInfo.Name);

                }

                
                catch 

                {
                    textBoxlog.AppendText("SKIPPED");



                }

               
                textBoxlog.AppendText(Environment.NewLine);                         

                

            }
        }
    }
}
