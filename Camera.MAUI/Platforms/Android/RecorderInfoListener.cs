using Android.Content;
using Android.Media;
using Android.Runtime;
using Android.Views;
using Java.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Icu.Text.ListFormatter;
using static Android.Media.MediaRecorder;

namespace Camera.MAUI.Platforms.Android
{


    internal class RecorderInfoListener : Java.Lang.Object, IOnInfoListener
    {
        private MauiCameraView mauiCameraView { get; set; }
        private string dir { get; set; }
        private string file { get; set; }
        private Action<string> callback { get; set; }
        private int maxDuration { get; set; }
        public RecorderInfoListener(MauiCameraView mauiCameraView, string dir, Action<string> callback, string file, int maxDuration)
        {
            this.mauiCameraView = mauiCameraView;
            this.dir = dir;
            this.file = file;
            this.callback = callback;
            this.maxDuration = maxDuration;
        }
        public void OnInfo(MediaRecorder mr, [GeneratedEnum] MediaRecorderInfo what, int extra)
        {
            if (what == MediaRecorderInfo.MaxDurationReached)
            {
                // if SetMaxDuration()
                mauiCameraView.mediaRecorder.Stop();
                mauiCameraView.mediaRecorder.Reset();
                mauiCameraView.mediaRecorder.Release();

                Task.Run(() => callback(file));

                mauiCameraView.InitMediaRecorder(dir, callback, maxDuration, true);
            }
        }
    }
}
