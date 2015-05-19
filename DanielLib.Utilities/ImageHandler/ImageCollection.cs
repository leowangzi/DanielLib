using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;

namespace DanielLib.Utilities.ImageHandler
{
    /// <summary>
    /// 图片集合，更新Location，则自动更新Images列表
    /// </summary>
    public class ImageCollection
    {
        private ObservableCollection<String> images = new ObservableCollection<String>();
        public ObservableCollection<String> Images 
        {
            get { return this.images; } 
        }

        private String location;
        public String Location
        {
            get { return this.location; }
            set
            {
                this.location = value;
                this.Load();
            }
        }

        private void Load()
        {
            Images.Clear();
            if (Directory.Exists(Location))
            {
                DirectoryInfo dir = new DirectoryInfo(Location);
                foreach (FileInfo file in dir.GetFiles("*.*", SearchOption.AllDirectories))
                {
                    if (SaveImageToDisk.IsImageExt(file))
                    {
                        Images.Add(file.FullName);
                    }
                }
            }
        }
    }
}
