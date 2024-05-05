using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MangaReader.Struct
{
    public class Manga
    {
        public string Name { get; } 
        public string Path { get; }
        public List<Chapter> Chapters { get; private set; }

        public readonly bool isPDF;


        public Manga(string name, string path)
        {
            Name = name;
            Path = path;
            isPDF = IsPDFCheck();
        }

        public void GetChapters()
        {
            if (isPDF)
            {
                ChaptersFromPDF();
            }
            else
            {
                ChaptersFromDirectory();
            }
        }

        void ChaptersFromDirectory()
        {
            if (Chapters != null) return;

            Chapters = new List<Chapter>();

            if (!Directory.Exists(Path))
            {
                return;
            }

            string[] chapterDirectories = Directory.GetDirectories(Path);

            var sortedChapterDirectories = NaturalSort(chapterDirectories);
            foreach (string chapterDirectory in sortedChapterDirectories)
            {
                string chapterName = System.IO.Path.GetFileName(chapterDirectory);
                Chapters.Add(new ChapterImg(this, chapterName, chapterDirectory));
            }
        }

        void ChaptersFromPDF()
        {
            // TODO : тут код по получению глав в PDF версии
        }


        public static IEnumerable<string> NaturalSort(IEnumerable<string> enumerable)
        {
            int maxLen = enumerable.Select(s => s.Length).DefaultIfEmpty(0).Max();

            return enumerable.OrderBy(s => Regex.Replace(s, @"\d+", m => m.Value.PadLeft(maxLen, '0')));
        }

        bool IsPDFCheck()
        {
            // TODO проверить на то, есть главы в формате pdf

            return false;
        }

    }
}
