using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MangaReader.Struct
{
    public class Picture
    {
        public string PictureName { get; set; }
        public Chapter Chapter { get; set; }
        public BitmapImage BitmapImage { get; set; }

        public Picture(string name, Chapter chapter, BitmapImage bitmapImage) 
        {
            PictureName = name;
            Chapter = chapter;
            BitmapImage = bitmapImage;
        }
    }
}
