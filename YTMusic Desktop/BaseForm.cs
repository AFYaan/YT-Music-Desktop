using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTMusic_Desktop.UI;

namespace YTMusic_Desktop
{
    public partial class BaseForm : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public Panel ItemContainer;
        public FormWindowState MinimizeState;

        public BaseForm()
        {
            InitializeComponent();
            ItemContainer = this.container;

            this.minimizeButton.MouseClickEvent += MinimizeButton_MouseClickEvent;
            this.maximizeButton.MouseClickEvent += MaximizeButton_MouseClickEvent;
            this.exitButton.MouseClickEvent += ExitButton_MouseClickEvent;
        }

        public void SetTopbarColor(Color color)
        {
            panel1.BackColor = color;
        }

        private void ExitButton_MouseClickEvent(object sender, EventArgs e)
        {
            Environment.Exit(0x0);
        }

        private void MaximizeButton_MouseClickEvent(object sender, EventArgs e)
        {          
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
            }     
        }

        private void MinimizeButton_MouseClickEvent(object sender, EventArgs e)
        {
            MinimizeState = WindowState;
            WindowState = FormWindowState.Minimized;
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {

        }

        public override string Text {
            get
            {
                return base.Text;
            }
            set
            {
                label1.Text = value;
                base.Text = value;
                label1.Location = new Point((this.Width / 2) - (label1.Size.Width / 2), label1.Location.Y);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            label1.Location = new Point((this.Width / 2) - (label1.Size.Width / 2), label1.Location.Y);
        }


        private void MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDown(e);
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDown(e);
        }
    }


}
