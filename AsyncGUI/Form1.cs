using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void buttonStart_Click(object sender, EventArgs e)
        {
            labelStatus.Text = "Задача запущена";
            buttonStart.Enabled = false;
            try
            {
                var result = await Task.WhenAll(new[] {
                    Task.Run(() => LongJob(4)),
                    Task.Run(() => LongJob(2)),
                    Task.Run(() => LongJob(1)),
                });
                // WaitAll, WhenAll, WaitAny
                textBox1.Text = await File.ReadAllTextAsync("D:\\1.TXT");
                labelStatus.Text = $"Задача выполнена. Результат: {string.Join(",", result)}";
            }
            catch (Exception ex)
            {
                labelStatus.Text = "Произошла ошибка: " + ex.Message;
            }
            finally
            {
                buttonStart.Enabled = true;
            }
        }

        int LongJob(int order)
        {
            Thread.Sleep(1000 * order);
            var ms = DateTime.Now.Millisecond;
            if (order < 2)
                throw new Exception("Ошибка");
            return ms + order * 100000;
        }

    }
}
