using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using ZXing;
using ZXing.Common;

namespace BarcodeScanner
{
    public partial class MainPage : UserControl
    {
        private DispatcherTimer m_timer = new DispatcherTimer();
        private readonly IBarcodeReader reader = new BarcodeReader();
        private WriteableBitmap currentBarcode;

        ImageBrush ib;
        CaptureSource source;
        private WriteableBitmap _capimage;
        public WriteableBitmap CapturedImage
        {
            get
            {
                return _capimage;
            }
            set { _capimage = value; }
        }


        public MainPage()
        {
            InitializeComponent();

            this.m_timer.Tick += btnFreeze_Click;
            this.m_timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
        }

        void source_CaptureImageCompleted(object sender, CaptureImageCompletedEventArgs e)
        {
            ib.ImageSource = e.Result;
            CapturedImage = e.Result;
        }

        [ScriptableMember]
        public void StartCamera()
        {
            source = new CaptureSource();
            VideoCaptureDevice vcd = CaptureDeviceConfiguration.GetDefaultVideoCaptureDevice();
            source.VideoCaptureDevice = vcd;
            VideoBrush vb = new VideoBrush();
            ib = new ImageBrush();
            vb.SetSource(source);
            rectangle2.Fill = ib;

            if (CaptureDeviceConfiguration.RequestDeviceAccess())
            {
                source.Start();
                this.m_timer.Start();
            }
        }

        [ScriptableMember]
        public void StopCamera()
        {
            rectangle2.Fill = null;
        }

        private void btnFreeze_Click(object o, EventArgs sender)
        {
            source.CaptureImageCompleted += new EventHandler<CaptureImageCompletedEventArgs>(source_CaptureImageCompleted);
            source.CaptureImageAsync();

            var result = reader.Decode(CapturedImage);
            if (result != null)
            {
                source.Stop();
                this.m_timer.Stop();

                HtmlPage.Window.Invoke("upcScanned", result.Text);
            }
        }
    }
}
