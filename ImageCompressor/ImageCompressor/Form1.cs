using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Encoder = System.Drawing.Imaging.Encoder;

namespace ImageCompressor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static long GetDirectorySize(string folderPath)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);
            return di.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length);
        }

        public int CompressedCount = 0;
        public int FileCount = 0;
        public int AllFiles = 0;
        private void btnCompress_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel1.Enabled = true;
            //var fsize = GetDirectorySize(txtSource.Text);

            //level1
            string dirLevel1 = txtSource.Text;
            string desLevel1 = txtDestination.Text;
            string[] files = Directory.GetFiles(dirLevel1);
            AllFiles += files.Length;
            foreach (var file in files)
            {
                
                string ext = Path.GetExtension(file).ToUpper();
                //if (ext == ".PNG" || ext == ".JPG")
                CompressImage(file, desLevel1, (int)cmbQuality.SelectedItem, ext);
            }

            
            string[] dirs = Directory.GetDirectories(dirLevel1);
            if (dirs.Length > 0)
            {
                //level2
                foreach (var dir in dirs)
                {
                    var folder = new DirectoryInfo(dir);
                    string desLevel2 = Path.Combine(desLevel1, folder.Name);
                    Directory.CreateDirectory(desLevel2);
                    string[] files1 = Directory.GetFiles(dir);
                    AllFiles += files1.Length;
                    foreach (var file in files1)
                    {
                        string ext = Path.GetExtension(file).ToUpper();
                        //if (ext == ".PNG" || ext == ".JPG")
                        CompressImage(file, desLevel2, (int)cmbQuality.SelectedItem, ext);
                    }

                    
                    string[] subdirs = Directory.GetDirectories(dir);
                    if (subdirs.Length > 0)
                    {
                        //level3
                        foreach (var subdir in subdirs)
                        {
                            var folder1 = new DirectoryInfo(subdir);
                            string desLevel3 = Path.Combine(desLevel2, folder1.Name);
                            Directory.CreateDirectory(desLevel3);
                            string[] files2 = Directory.GetFiles(subdir);
                            AllFiles += files2.Length;
                            foreach (var file in files2)
                            {
                                string ext = Path.GetExtension(file).ToUpper();
                                //if (ext == ".PNG" || ext == ".JPG")
                                CompressImage(file, desLevel3, (int)cmbQuality.SelectedItem, ext);
                            }

                            
                            string[] subsubdirs = Directory.GetDirectories(subdir);
                            if (subsubdirs.Length > 0)
                            {
                                //level4
                                foreach (var subsubdir in subsubdirs)
                                {
                                    var folder2 = new DirectoryInfo(subsubdir);
                                    string desLevel4 = Path.Combine(desLevel3, folder2.Name);
                                    Directory.CreateDirectory(desLevel4);
                                    string[] files3 = Directory.GetFiles(subsubdir);
                                    AllFiles += files3.Length;
                                    foreach (var file in files3)
                                    {
                                        string ext = Path.GetExtension(file).ToUpper();
                                        //if (ext == ".PNG" || ext == ".JPG")
                                        CompressImage(file, desLevel4, (int)cmbQuality.SelectedItem, ext);
                                    }

                                    
                                    string[] subsubsubdirs = Directory.GetDirectories(subsubdir);
                                    if (subsubsubdirs.Length > 0)
                                    {
                                        //level5
                                        foreach (var subsubsubdir in subsubsubdirs)
                                        {
                                            var folder3 = new DirectoryInfo(subsubsubdir);
                                            string desLevel5 = Path.Combine(desLevel4, folder3.Name);
                                            Directory.CreateDirectory(desLevel5);
                                            string[] files4 = Directory.GetFiles(subsubsubdir);
                                            AllFiles += files4.Length;
                                            foreach (var file in files4)
                                            {
                                                string ext = Path.GetExtension(file).ToUpper();
                                                //if (ext == ".PNG" || ext == ".JPG")
                                                CompressImage(file, desLevel5, (int)cmbQuality.SelectedItem, ext);
                                            }

                                            
                                            string[] subsubsubsubdirs = Directory.GetDirectories(subsubsubdir);
                                            if (subsubsubsubdirs.Length > 0)
                                            {
                                                //level6
                                                foreach (var subsubsubsubdir in subsubsubsubdirs)
                                                {
                                                    var folder4 = new DirectoryInfo(subsubsubsubdir);
                                                    string desLevel6 = Path.Combine(desLevel5, folder4.Name);
                                                    Directory.CreateDirectory(desLevel6);
                                                    string[] files5 = Directory.GetFiles(subsubsubsubdir);
                                                    AllFiles += files5.Length;
                                                    foreach (var file in files5)
                                                    {
                                                        string ext = Path.GetExtension(file).ToUpper();
                                                        //if (ext == ".PNG" || ext == ".JPG")
                                                        CompressImage(file, desLevel6, (int)cmbQuality.SelectedItem, ext);
                                                    }

                                                    //level7
                                                    string[] subsubsubsubsubdirs = Directory.GetDirectories(subsubsubsubdir);
                                                    if (subsubsubsubsubdirs.Length > 0)
                                                    {
                                                        foreach (var subsubsubsubsubdir in subsubsubsubsubdirs)
                                                        {
                                                            var folder5 = new DirectoryInfo(subsubsubsubsubdir);
                                                            string desLevel7 = Path.Combine(desLevel6, folder5.Name);
                                                            Directory.CreateDirectory(desLevel7);
                                                            string[] files6 = Directory.GetFiles(subsubsubsubsubdir);
                                                            AllFiles += files5.Length;
                                                            foreach (var file in files6)
                                                            {
                                                                string ext = Path.GetExtension(file).ToUpper();
                                                                //if (ext == ".PNG" || ext == ".JPG")
                                                                CompressImage(file, desLevel7, (int)cmbQuality.SelectedItem, ext);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //var nsize = GetDirectorySize(txtDestination.Text);
            txtDestination.Text = "";
            txtSource.Text = "";
            panel1.Visible = false;
            panel1.Enabled = false;
            MessageBox.Show(CompressedCount + " has been compressed! \nAll Files:"+ FileCount);
        }
        
        public void CompressImage(string SoucePath, string DestPath, int quality, string format)
        {

            var FileName = Path.GetFileName(SoucePath);
            var dest = DestPath;
            DestPath = DestPath + "\\" + FileName;


            if (format !=".PNG" && format!=".JPG")
            {
                File.Copy(SoucePath, DestPath, true);
            }
            else
            {
                try
                {
                    using (Bitmap bmp1 = new Bitmap(SoucePath))
                    {

                        var f = ImageFormat.Jpeg;
                        if (format.ToLower().Contains("png"))
                        {
                            f = ImageFormat.Png;
                        }

                        ImageCodecInfo jpgEncoder = GetEncoder(f);

                        Encoder QualityEncoder = Encoder.Quality;

                        EncoderParameters myEncoderParameters = new EncoderParameters(1);

                        EncoderParameter myEncoderParameter = new EncoderParameter(QualityEncoder, quality);

                        myEncoderParameters.Param[0] = myEncoderParameter;
                        bmp1.Save(DestPath, jpgEncoder, myEncoderParameters);
                        CompressedCount++;
                        label4.Text = CompressedCount + "- "+FileName + " has been compressed successfully!";
                    }
                }
                catch (Exception e)
                {
                    
                }
            }

            FileCount++;
            progressBar1.Value = int.Parse(((FileCount * 100 / AllFiles)).ToString());
        }
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 10; i <= 100; i = i + 5)
            {
                cmbQuality.Items.Add(i);
            }
            cmbQuality.SelectedIndex = 4;
        }

        private void btnSouceBrowse_Click(object sender, EventArgs e)
        {
            OpenFolderDialog(txtSource);
        }

        private void OpenFolderDialog(TextBox Filepath)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;

            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                Filepath.Text = folderDlg.SelectedPath;
                Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }

        private void btnDestFolder_Click(object sender, EventArgs e)
        {
            OpenFolderDialog(txtDestination);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void cmbQuality_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
