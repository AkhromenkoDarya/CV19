using System;
using System.Threading;
using System.Windows.Forms;

namespace CV19WinFormsTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, System.EventArgs e)
        {
            new Thread(ComputeValue).Start();
        }

        private void ComputeValue()
        {
            var value = LongProcess(DateTime.Now);
            SetResultValue(value);

            //if (ResultLabel.InvokeRequired)
            //{
            //    ResultLabel.Invoke(new Action(() => ResultLabel.Text = value));
            //}
            //else
            //{
            //    ResultLabel.Text = value;
            //}
        }

        private void SetResultValue(string value)
        {
            if (ResultLabel.InvokeRequired)
            {
                ResultLabel.Invoke(new Action<string>(SetResultValue), value);
            }
            else
            {
                ResultLabel.Text = value;
            }
        }

        private static string LongProcess(DateTime time)
        {
            Thread.Sleep(5000);

            return $"Value: {time}";
        }
    }
}
