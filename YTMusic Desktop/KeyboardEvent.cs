using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTMusic_Desktop
{
    public class KeyboardEvent
    {
        public static event EventHandler MediaStopEvent;
        public static event EventHandler MediaPreviousTrackEvent;
        public static event EventHandler MediaPlayPauseEvent;
        public static event EventHandler MediaNextTrackEvent;


        public static void OnMediaStop()
        {
            if (MediaStopEvent != null)
            {
                MediaStopEvent(null, EventArgs.Empty);
            }
        }

        public static void OnMediaPreviousTrack()
        {
            if (MediaPreviousTrackEvent != null)
            {
                MediaPreviousTrackEvent(null, EventArgs.Empty);
            }
        }

        public static void OnMediaPlayPause()
        {
            if (MediaPlayPauseEvent != null)
            {
                MediaPlayPauseEvent(null, EventArgs.Empty);
            }
        }

        public static void OnMediaNextTrack()
        {
            if (MediaNextTrackEvent != null)
            {
                MediaNextTrackEvent(null, EventArgs.Empty);
            }
        }

    }
}
