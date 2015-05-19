using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.IO;
using DanielLib.Utilities.LogHandler;

namespace DanielLib.Utilities.AudioHandler
{
    public class AudioManager
    {
        private static int screenWidth;
        private static double audioVolume;
        private static String audioFilePath;
        private Dictionary<String, String> soundFiles;
        private Dictionary<String, MediaPlayer> audioFiles = new Dictionary<String, MediaPlayer>();

        public AudioManager(int _screenWidth, double _audioVolume, String _audioDirectoryLocation, Dictionary<String, String> _soundFiles)
        {
            AudioManager.screenWidth = _screenWidth;
            AudioManager.audioVolume = _audioVolume;
            AudioManager.audioFilePath = _audioDirectoryLocation;
            this.soundFiles = _soundFiles;

            if (audioVolume > 0)
            {
                foreach (var item in this.soundFiles)
                {
                    CreateMediaPlayer(item.Key);
                }
            }
        }

        public void UnLoad()
        {
            audioFiles.Clear();
        }

        public void PlayAudio(AudioEventArgs e)
        {
            if (e != null)
            {
                if (audioFiles == null || !audioFiles.ContainsKey(e.Audio))
                {
                    return;
                }
                MediaPlayer player = audioFiles[e.Audio];
                player.Stop();
                if (e.XPosition > 0 && e.XPosition <= screenWidth)
                {
                    player.Balance = (e.XPosition / (screenWidth / 2)) - 1;
                }
                player.Play();
            }
        }

        internal void CreateMediaPlayer(String _key)
        {
            try
            {
                String fileName = soundFiles[_key];
                if (!String.IsNullOrEmpty(fileName))
                {
                    fileName = Path.Combine(audioFilePath, fileName);
                    if (!Path.IsPathRooted(fileName))
                    {
                        fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                        if (File.Exists(fileName))
                        {
                            MediaPlayer player = new MediaPlayer();
                            player.Open(new Uri(fileName, UriKind.Relative));
                            player.Volume = audioVolume;
                            audioFiles.Add(_key, player);
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                DebugLogHandler.logger.Error("没有找到音频文件:", ex);
            }
            catch (System.Exception ex)
            {
                DebugLogHandler.logger.Error("创建MediaPlayer对象失败:", ex);
            }
        }
    }
}
