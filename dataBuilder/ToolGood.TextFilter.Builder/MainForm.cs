using System;
using System.IO;
using System.Windows.Forms;
using ToolGood.RcxCrypto;
using ToolGood.TextFilter.DataBuilder;
using ToolGood.TextFilter.DataBuilder2;

namespace ToolGood.TextFilter.Builder
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.textBox1.Text = "TextClassify.sav";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() {
                FileName = "TextClassify.sav"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                this.textBox1.Text = openFileDialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK) {
                this.textBox2.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (File.Exists(this.textBox1.Text)==false) {
                MessageBox.Show("请选择敏感词库！");
                return;
            }
            this.button3.Text = "生成中...";
            Application.DoEvents();

            var filePath = Path.GetFullPath(this.textBox1.Text);
            var outFilePath = Path.GetFullPath(Path.Combine(this.textBox2.Text, @$"TextFilter-{DateTime.Now.ToString("yyyyMMdd")}.data"));

            var bigFile = false;
            Startup startup = new Startup();

            startup.Init(filePath, "", false, bigFile);
            startup.Build();

            if (bigFile) {
                WriteOutFile("temp/out_big.temp", outFilePath);
            } else {
                WriteOutFile("temp/out_small.temp", outFilePath);
            }
            this.button3.Text = "生 成";
            MessageBox.Show("完成！");
        }
        private void WriteOutFile(string path, string outPath)
        {
            var bytes = File.ReadAllBytes(path);

            string pwd = "012345679";
            bytes = CompressionUtil.GzipCompress(bytes);

            bytes = Rcy.Encrypt(bytes, pwd);

            outPath = Path.GetFullPath(outPath);
            Directory.CreateDirectory(Path.GetDirectoryName(outPath));
            File.WriteAllBytes(outPath, bytes);
        }



        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else e.Effect = DragDropEffects.None;
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            var files = (System.Array)e.Data.GetData(DataFormats.FileDrop);
            foreach (var item in files) {
                var file = item.ToString();
                if (File.Exists(file)) {
                    this.textBox1.Text = file;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
