using System;
using System.Windows.Forms;

namespace ToolGood.TextFilter.Builder
{
    public partial class FirstForm : Form
    {
        Timer timer;
        public FirstForm()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 3 * 1000;
            timer.Tick += Timer_Tick;
            timer.Start();

            this.label4.Text = "授权："+"测试";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            this.Close();
        }
    }
}
