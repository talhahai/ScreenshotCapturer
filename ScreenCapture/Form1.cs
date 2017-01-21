using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScreenCapture
{
    public partial class Form1 : Form
    {
        string Folder;
        bool RangeSelected = false;
        int RangeStartX, RangeStartY, RangeEndX, RangeEndY;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            System.Threading.Thread.Sleep(250);
            Bitmap screenshot;
            if (RangeSelected == true)
                screenshot = new Bitmap(RangeEndX - RangeStartX, RangeEndY - RangeStartY, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            else
                screenshot = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics screenGraph = Graphics.FromImage(screenshot);
            if (RangeSelected == true)
                screenGraph.CopyFromScreen(RangeStartX, RangeStartY, 0, 0, new Size(RangeEndX - RangeStartX, RangeEndY - RangeStartY));
            else
                screenGraph.CopyFromScreen(Screen.PrimaryScreen.WorkingArea.X, Screen.PrimaryScreen.WorkingArea.Y, 0, 0, SystemInformation.VirtualScreen.Size, CopyPixelOperation.SourceCopy);
            screenshot.Save(Folder + "\\Screen Shot " + DateTime.Now.ToString("dddd, MMMM dd, yyyy - hh;mm;ss tt") + ".png", System.Drawing.Imaging.ImageFormat.Png);
            RangeSelected = false;
            button3.Text = "SET RANGE";
            this.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            while (true)
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    Folder = folderBrowserDialog1.SelectedPath;
                    button2.Text = "CHANGE FOLDER";
                    button1.Enabled = true;
                    button3.Enabled = true;
                    break;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "SET RANGE")
                button3.Text = "SET START";

            else if (button3.Text == "SET START")
            {
                RangeStartX = Cursor.Position.X;
                RangeStartY = Cursor.Position.Y;
                button3.Text = "SET END";
            }

            else if (button3.Text == "SET END")
            {
                RangeEndX = Cursor.Position.X;
                RangeEndY = Cursor.Position.Y;
                button3.Text = "RANGE DONE";

                RangeSelected = true;
            }
        }
    }
}