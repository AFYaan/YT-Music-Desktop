using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace YTMusic_Desktop.UI
{
    public class TopbarButton : Control
    {
        PictureBox pictureBox;
        public event EventHandler MouseClickEvent;
        private bool exitButton = false;
        public TopbarButton()
        {
            this.Size = new Size(26, 24);
            BackColor = Color.FromArgb(29, 29, 29);
            exitButton = false;

            pictureBox = new PictureBox();
            pictureBox.Size = new Size(14, 14);
            pictureBox.Location = new Point((this.Width / 2) - (pictureBox.Width / 2), (this.Height / 2) - (pictureBox.Height / 2));
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.MouseEnter += PictureBox_MouseEnter;
            pictureBox.MouseLeave += PictureBox_MouseLeave;
            pictureBox.MouseClick += PictureBox_MouseClick;

            this.Controls.Add(pictureBox);
        }

        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            OnClick();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            OnClick();
        }

        private void OnClick()
        {
            if (MouseClickEvent != null)
            {
                MouseClickEvent(this, EventArgs.Empty);
            }
        }

        private new void MouseEnter()
        {
            BackColor = Color.FromArgb(50, 50, 50);
        }

        private new void MouseLeave()
        {
            BackColor = Color.FromArgb(29, 29, 29);
        }

        private void PictureBox_MouseEnter(object sender, EventArgs e)
        {
            MouseEnter();
        }

        private void PictureBox_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave();
        }         

        protected override void OnMouseEnter(EventArgs e)
        {
            MouseEnter();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            MouseLeave();
        }

        [Description("Image"), Category("UI")]
        public Image Image
        {
            get { return pictureBox.Image; }
            set { pictureBox.Image = value; }
        }
    }
}
