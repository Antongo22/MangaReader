using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MangaReader.Struct
{
    public abstract class Chapter
    {
        public Manga ParentManga { get; }
        public string ChapterName { get; }
        public string PathToChapter { get; }
        public List<Picture> Images { get; private set; }

        public Chapter(Manga parentManga, string chapterName, string pathToChapter)
        {
            ParentManga = parentManga;
            ChapterName = chapterName;
            PathToChapter = pathToChapter;
        }
    }
}
