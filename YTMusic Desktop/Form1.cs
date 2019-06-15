using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace YTMusic_Desktop
{
    public partial class Form1 : Form
    {
        
        public ChromiumWebBrowser chromeBrowser;

        public void InitializeChromium()
        {
            CefSettings settings = new CefSettings();
            settings.CachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\YTMusic";
            settings.Locale = "pl-PL";
            Cef.Initialize(settings);
            
            chromeBrowser = new ChromiumWebBrowser("https://music.youtube.com");

            chromeBrowser.MenuHandler = new MenuHandler();
            chromeBrowser.BackColor = Color.Black;
            chromeBrowser.ForeColor = Color.Black;
            chromeBrowser.TitleChanged += ChromeBrowser_TitleChanged;
            this.Controls.Add(chromeBrowser);

            chromeBrowser.Dock = DockStyle.Fill;

        }

        private void ChromeBrowser_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            string title = String.Empty;

            if(e.Title != "YouTube Music")
            {
                title = "YouTube Music | " + e.Title.Replace(" - YouTube Music", "");
            }
            else
            {
                title = e.Title;
            }

            this.Invoke((MethodInvoker)delegate ()
            {
                this.Text = title;
            });

        }

        public Form1()
        {
            InitializeComponent();

            InitializeChromium();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
           
         //load
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            chromeBrowser.Size = this.Size;
        }
    }
}
