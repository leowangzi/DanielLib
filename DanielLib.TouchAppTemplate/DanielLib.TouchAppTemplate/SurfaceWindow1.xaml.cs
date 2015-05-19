using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;
using DanielLib.Utilities.AudioHandler;
using DanielLib.Utilities.Extentions;
using System.Threading;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.IO;
using DanielLib.Utilities.ImageHandler;
using Touchtech.Surface.Presentation.Controls;
using DanielLib.Utilities.LogHandler;

namespace DanielLib.TouchAppTemplate
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : OptimizedSurfaceWindow
    {
        #region SurfaceWindow 参数

        #region SurfaceWindow 全局参数

        public static SurfaceWindow1 Instance { get; private set; }
        private int ScreenWidth = 1920;

        #endregion

        #region SurfaceWindow 图像资源参数

        private ObservableCollection<BitmapSource> imageFiles = new ObservableCollection<BitmapSource>();
        private String ImageDirectory = @"Resources\\Background";

        #endregion

        #region SurfaceWindow 音效资源参数

        public static readonly Dictionary<String, String> audioFiles;
        private AudioManager audioManager;
        private double Volume = 0.5;
        private String SoundDirectory = @"Resources\\Sound";
        public delegate void AudioEventHandler(object sender, AudioEventArgs e);
        public static readonly RoutedEvent AudioEvent = EventManager.RegisterRoutedEvent("Audio", RoutingStrategy.Bubble, typeof(AudioEventHandler), typeof(SurfaceWindow1));
        public event AudioEventHandler Audio
        {
            add { AddHandler(AudioEvent, value); }
            remove { RemoveHandler(AudioEvent, value); }
        }

        #endregion

        #endregion

        #region SurfaceWindow 构造函数

        /// <summary>
        /// Default constructor.
        /// </summary>
        static SurfaceWindow1()
        {
            audioFiles = new Dictionary<String, String>();
            audioFiles.Add("Load", "load.mp3");
            //...
        }
        public SurfaceWindow1()
        {
            InitializeComponent();
            //MainWindow Instance
            Instance = this;
            AddWindowAvailabilityHandlers();

            //加载音效资源;
            LoadAudioResource();
            //加载图像资源;
            //LoadImageResource();

            //Sample
            //RaiseEvent(new AudioEventArgs(SurfaceWindow1.AudioEvent, this) { Audio = "Load"});
            //Img_background.ImageSource = DanielLib.Utilities.ImageHandler.LoadImageFromDisk.GetImage(@"E:\MyLib\DanielLib\DanielLib.TouchAppTemplate\DanielLib.TouchAppTemplate\Resources\背景1.jpg");
            //DebugLogHandler.logger.Debug("调试信息");
            GlobalState.ApplicationStateChanged += new GlobalState.ApplicationStateChangedEventHandler(GlobalState_ApplicationStateChanged);
        }

        #endregion

        #region SurfaceWindow Common私有成员

        void GlobalState_ApplicationStateChanged(object sender, ApplicationStateChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region SurfaceWindow 加载资源

        private void LoadAudioResource()
        {
            audioManager = new AudioManager(this.ScreenWidth, this.Volume, this.SoundDirectory, audioFiles);
            this.Audio += new AudioEventHandler(Main_Audio);
        }

        private void LoadImageResource()
        {
            imageFiles.Clear();

            if (!System.IO.Path.IsPathRooted(this.ImageDirectory))
            {
                this.ImageDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.ImageDirectory);
            }
            if (Directory.Exists(this.ImageDirectory))
            {
                DirectoryInfo dir = new DirectoryInfo(this.ImageDirectory);
                foreach (FileInfo file in dir.GetFiles("*.*", SearchOption.AllDirectories))
                {
                    if (SaveImageToDisk.IsImageExt(file))
                    {
                        imageFiles.Add(LoadImageFromDisk.GetImage(file.FullName));
                    }
                }
            }
            this.Dispatcher.BeginInvoke((Action)(() => {
                Img_one.Source = imageFiles[0];
                Img_one.Loaded += (sender, args) =>
                    {
                        //MessageBox.Show(sender.ToString());
                    };
            }));
        }

        #endregion

        #region SurfaceWindow AudioManager

        void Main_Audio(object sender, AudioEventArgs e)
        {
            this.audioManager.PlayAudio(e);
        }

        #endregion

        #region SurfaceWindow WindowHandler

        /// <summary>
        /// Occurs when the window is about to close. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Remove handlers for window availability events
            RemoveWindowAvailabilityHandlers();
        }

        /// <summary>
        /// Adds handlers for window availability events.
        /// </summary>
        private void AddWindowAvailabilityHandlers()
        {
            // Subscribe to surface window availability events
            ApplicationServices.WindowInteractive += OnWindowInteractive;
            ApplicationServices.WindowNoninteractive += OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable += OnWindowUnavailable;
        }

        /// <summary>
        /// Removes handlers for window availability events.
        /// </summary>
        private void RemoveWindowAvailabilityHandlers()
        {
            // Unsubscribe from surface window availability events
            ApplicationServices.WindowInteractive -= OnWindowInteractive;
            ApplicationServices.WindowNoninteractive -= OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable -= OnWindowUnavailable;
        }

        /// <summary>
        /// This is called when the user can interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowInteractive(object sender, EventArgs e)
        {
            //TODO: enable audio, animations here
        }

        /// <summary>
        /// This is called when the user can see but not interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowNoninteractive(object sender, EventArgs e)
        {
            //TODO: Disable audio here if it is enabled

            //TODO: optionally enable animations here
        }

        /// <summary>
        /// This is called when the application's window is not visible or interactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowUnavailable(object sender, EventArgs e)
        {
            //TODO: disable audio, animations here
        }

        #endregion
    }
}