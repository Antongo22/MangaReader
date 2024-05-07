using MangaReader.Struct;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MangaReader.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChapterPdfPage.xaml
    /// </summary>
    public partial class ChapterPdfPage : Page
    {
        MainWindow mainWindow;
        ChaptersPDF chapter;
        MangaChapters mangaChapters;

        string pathToTmpHtml;

        public ChapterPdfPage(MainWindow mainWindow, ChaptersPDF chapter, MangaChapters mangaChapters)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
            this.chapter = chapter;
            this.mangaChapters = mangaChapters;

            LoadChapter();
        }

        void LoadChapter()
        {
            GC.Collect();

            try
            {
                if (!string.IsNullOrEmpty(chapter.PathToChapter))
                {
                    string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(chapter.PathToChapter);
                    string tmpHtmlFileName = $"{fileNameWithoutExtension}.html";

                    string directoryPath = System.IO.Path.GetDirectoryName(chapter.PathToChapter);

                    pathToTmpHtml = System.IO.Path.Combine(directoryPath, tmpHtmlFileName);

                    string htmlTemplate = @"<!DOCTYPE html>
                                    <html lang=""en"">
                                    <head>
                                    <meta charset=""UTF-8"">
                                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                    <style>
                                        .pdf {
                                            width: 100%;
                                            aspect-ratio: 4 / 3;
                                        }

                                        .pdf,
                                        html,
                                        body {
                                            height: 100%;
                                            margin: 0;
                                            padding: 0;
                                        }

                                        h1,
                                        h3 {
                                            text-align: center;
                                        }

                                        h1 {
                                            color: green;
                                        }
                                    </style>
                                    </head>
                                    <body>
                                    <embed class=""pdf"" 
                                               src=""{0}"" type=""application/pdf"" width=""100%"" height=""100%"" />
                                    </body>
                                    </html>";

                    htmlTemplate = htmlTemplate.Replace("{0}", chapter.PathToChapter);

                    using (StreamWriter writer = new StreamWriter(pathToTmpHtml))
                    {
                        writer.Write(htmlTemplate);
                    }

                    chromiumWebBrowser.Address = new Uri(pathToTmpHtml).AbsoluteUri;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Путь к PDF файлу пустой.");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Ошибка при загрузке PDF файла: {ex.Message}");
            }
        }




        ~ChapterPdfPage()
        {
            File.Delete(pathToTmpHtml);
        }

    }
}
