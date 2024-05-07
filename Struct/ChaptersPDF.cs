using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaReader.Struct
{
    public class ChaptersPDF : Chapter
    {
        public ChaptersPDF(Manga parentManga, string chapterName, string pathToChapter) : base(parentManga, chapterName, pathToChapter)
        {

        }
    }
}
