using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace YTMusic_Desktop
{
    public partial class Form1 : BaseForm
    {      
        public ChromiumWebBrowser chromeBrowser;

        public void InitializeChromium()
        {
            CefSettings settings = new CefSettings();
            settings.CachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\YTMusic";
            settings.Locale = "pl-PL";
            settings.MultiThreadedMessageLoop = true;
            Cef.Initialize(settings);       
            chromeBrowser = new ChromiumWebBrowser("https://music.youtube.com");
            chromeBrowser.MenuHandler = new MenuHandler();
            chromeBrowser.BackColor = Color.Black;
            chromeBrowser.ForeColor = Color.Black;
            chromeBrowser.TitleChanged += ChromeBrowser_TitleChanged;
            chromeBrowser.Dock = DockStyle.Fill;
            this.ItemContainer.Controls.Add(chromeBrowser);         
            KeyboardEvent.MediaStopEvent += KeyboardEvent_MediaStopEvent;
            KeyboardEvent.MediaPreviousTrackEvent += KeyboardEvent_MediaPreviousTrackEvent;
            KeyboardEvent.MediaPlayPauseEvent += KeyboardEvent_MediaPlayPauseEvent;
            KeyboardEvent.MediaNextTrackEvent += KeyboardEvent_MediaNextTrackEvent;
            //chromeBrowser.Visible = false;
        }

        private void KeyboardEvent_MediaStopEvent(object sender, EventArgs e)
        {            
            chromeBrowser.ExecuteScriptAsync("document.getElementsByClassName('play-pause-button style-scope ytmusic-player-bar')[0].click();");
            //chromeBrowser.ExecuteScriptAsync("document.getElementById('play-pause-button').click();");
        }

        private void KeyboardEvent_MediaPreviousTrackEvent(object sender, EventArgs e)
        {     
            chromeBrowser.ExecuteScriptAsync("document.getElementsByClassName('previous-button style-scope ytmusic-player-bar')[0].click();");
        }

        private void KeyboardEvent_MediaPlayPauseEvent(object sender, EventArgs e)
        {
            chromeBrowser.ExecuteScriptAsync("document.getElementsByClassName('play-pause-button style-scope ytmusic-player-bar')[0].click();");           
        }

        private void KeyboardEvent_MediaNextTrackEvent(object sender, EventArgs e)
        {
            chromeBrowser.ExecuteScriptAsync("document.getElementsByClassName('next-button style-scope ytmusic-player-bar')[0].click();");
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

            MinimizeState = WindowState;
            //ContextMenuStrip cms = new ContextMenuStrip();

            //notifyIcon1.ContextMenuStrip = cms;     
            chromeBrowser.IsBrowserInitializedChanged += ChromeBrowser_IsBrowserInitializedChanged;
        }

        private void ChromeBrowser_IsBrowserInitializedChanged(object sender, IsBrowserInitializedChangedEventArgs e)
        {
            Console.WriteLine("JUŻ=============");
         
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(updateColor));
            //thread.Start();
        }

        private void updateColor()
        {
            while (true)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    
                }));
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            chromeBrowser.Size = this.Size;
        }

        

        private void notifyIcon1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if(this.WindowState == FormWindowState.Minimized)
                {
                    this.WindowState = MinimizeState;
                }
            }
            if (e.Button == MouseButtons.Right)
            {

            }
        }

        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MINIMIZE = 0xF020;

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = m.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MINIMIZE)
                    {
                        MinimizeState = WindowState;
                    }
                    // If you don't want to do the default action then break
                    break;
            }
            base.WndProc(ref m);
        }
    }
}
