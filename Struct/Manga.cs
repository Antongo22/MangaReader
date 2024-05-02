using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaReader.Struct
{
    public class Manga
    {
        public string Name { get; } 
        public string Path { get; }
        public List<Chapter> Chapters { get; } 

        public Manga(string name, string path)
        {
            Name = name;
            Path = path;
            Chapters = new List<Chapter>(); 
            GetChapters();
        }

        void GetChapters()
        {
            if (!Directory.Exists(Path))
            {
                return;
            }

            string[] chapterDirectories = Directory.GetDirectories(Path);

            var sortedChapterDirectories = chapterDirectories.OrderBy(chapterDirectory => System.IO.Path.GetFileName(chapterDirectory));

            foreach (string chapterDirectory in sortedChapterDirectories)
            {
                string chapterName = System.IO.Path.GetFileName(chapterDirectory);
                Chapters.Add(new Chapter(this, chapterName, chapterDirectory));
            }
        }
    }
}
