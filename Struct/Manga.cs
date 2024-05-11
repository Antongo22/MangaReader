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
            // Удаление всех HTML файлов перед началом сбора глав
            DeleteAllHtmlFiles();

            if (Chapters != null) return;

            Chapters = new List<Chapter>();

            if (!Directory.Exists(Path))
            {
                return;
            }

            string[] chapterDirectories = Directory.GetFiles(Path);

            var sortedChapterDirectories = NaturalSort(chapterDirectories);
            foreach (string chapterDirectory in sortedChapterDirectories)
            {
                string chapterName = System.IO.Path.GetFileName(chapterDirectory);
                Chapters.Add(new ChaptersPDF(this, chapterName, chapterDirectory));
            }
        }

        void DeleteAllHtmlFiles()
        {
            try
            {
                // Получение всех HTML файлов в директории
                string[] htmlFiles = Directory.GetFiles(Path, "*.html");

                // Удаление каждого HTML файла
                foreach (string htmlFile in htmlFiles)
                {
                    File.Delete(htmlFile);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Ошибка при удалении HTML файлов: {ex.Message}");
            }
        }


        public static IEnumerable<string> NaturalSort(IEnumerable<string> enumerable)
        {
            int maxLen = enumerable.Select(s => s.Length).DefaultIfEmpty(0).Max();

            return enumerable.OrderBy(s => Regex.Replace(s, @"\d+", m => m.Value.PadLeft(maxLen, '0')));
        }

        bool IsPDFCheck()
        {
            try
            {
                if (Directory.Exists(Path))
                {
                    string[] files = Directory.GetFiles(Path);

                    foreach (string file in files)
                    {
                        if (System.IO.Path.GetExtension(file).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Указанная директория не существует.");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Ошибка при проверке файлов: {ex.Message}");
            }

            return false;
        }

        public void AddChapter(string chapterPath)
        {
            string chapterName = System.IO.Path.GetFileName(chapterPath);

            Chapter newChapter;
            if (isPDF)
            {
                CopyPdfChaptert(chapterPath);
                newChapter = new ChaptersPDF(this, chapterName, chapterPath);
            }
            else
            {
                CopyDirecttoryChapter(chapterPath);
                newChapter = new ChapterImg(this, chapterName, chapterPath);
            }

            Chapters ??= new List<Chapter>();
            Chapters.Add(newChapter);

            Chapters = NaturalSort(Chapters.Select(chapter => chapter.ChapterName)).Select(chapterName =>
            {
                return Chapters.First(chapter => chapter.ChapterName == chapterName);
            }).ToList();
        }

        void CopyDirecttoryChapter(string chapterPath)
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }

            string chapterName = System.IO.Path.GetFileName(chapterPath);

            string destinationPath = System.IO.Path.Combine(Path, chapterName);

            DirectoryCopy(chapterPath, destinationPath, true);
        }

        void CopyPdfChaptert(string chapterPath)
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path); 
            }

            string chapterName = System.IO.Path.GetFileName(chapterPath);

            string destinationPath = System.IO.Path.Combine(Path, chapterName);

            File.Copy(chapterPath, destinationPath);
        }

        // Метод для рекурсивного копирования директории
        void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = System.IO.Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = System.IO.Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }


    }
}
