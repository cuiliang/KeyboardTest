using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

            foreach (Keys key in (Keys[]) Enum.GetValues(typeof(Keys)))
            {
                if (KeyboardHelper.IsKeyDown(key))
                {
                    sb.AppendLine(key.ToString());
                }
            }

            if ((Control.MouseButtons & MouseButtons.Left) != 0)
            {
                sb.AppendLine("Mouse: Left");
            }

            if ((Control.MouseButtons & MouseButtons.Middle) != 0)
            {
                sb.AppendLine("Mouse: Middle");
            }
            if ((Control.MouseButtons & MouseButtons.Right) != 0)
            {
                sb.AppendLine("Mouse: Right");
            }
            if ((Control.MouseButtons & MouseButtons.XButton1) != 0)
            {
                sb.AppendLine("Mouse: XButton1");
            }
            if ((Control.MouseButtons & MouseButtons.XButton2) != 0)
            {
                sb.AppendLine("Mouse: XButton2");
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
