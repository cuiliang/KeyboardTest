using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput.Native;
using Quicker.Utilities;

namespace KeyboardTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateKeys();
        }

        private void UpdateKeys()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Any key down? ");
            sb.AppendLine(KeyboardHelper.IsAnyKeyDown().ToString());

            foreach (VirtualKeyCode key in (VirtualKeyCode[]) Enum.GetValues(typeof(VirtualKeyCode)))
            {
                if (KeyboardHelper.IsKeyDown(key))
                {
                    sb.AppendLine(key.ToString());
                }
            }

            textBox1.Text = sb.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateKeys();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateKeys();
        }
    }
}
