using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MangaReader.Struct
{
    public class ChapterImg : Chapter
    {
        public List<Picture> Images { get; private set; }

        public ChapterImg(Manga parentManga, string chapterName, string pathToChapter) : base(parentManga, chapterName, pathToChapter)
        {
           
        }


        public void GetImages()
        {
            if (Images != null) return;

            Images = new List<Picture>();
            if (!Directory.Exists(PathToChapter))
            {
                return;
            }

            string[] imageFiles = Directory.GetFiles(PathToChapter);

            foreach (string file in imageFiles)
            {
                if (IsImageFile(file))
                {
                    BitmapImage image = new BitmapImage(new Uri(file));
                    Images.Add(new(Path.GetFileName(file), this, image));
                }
            }
        }

        bool IsImageFile(string filePath)
        {
            string extension = Path.GetExtension(filePath);

            return extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) ||
                   extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                   extension.Equals(".png", StringComparison.OrdinalIgnoreCase) ||
                   extension.Equals(".gif", StringComparison.OrdinalIgnoreCase) ||
                   extension.Equals(".bmp", StringComparison.OrdinalIgnoreCase);
        }
    }
}
