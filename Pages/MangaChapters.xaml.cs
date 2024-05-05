using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MangaReader.Struct;

namespace MangaReader.Pages
{
    /// <summary>
    /// Логика взаимодействия для MangaChapters.xaml
    /// </summary>
    public partial class MangaChapters : Page
    {
        PicturePage picturePage;
        MainWindow mainWindow;
        public bool isLastp = false;
        Manga manga;

        public MangaChapters(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
        }

        public void LoadManga(Manga manga)
        {
            if(manga == null) { return; }

            this.manga = manga;

            manga.GetChapters();

            foreach (var ch in manga.Chapters)
            {
                ChaptersList.Items.Add(ch);
            }
        }


        public void OpenLastPage()
        {
            isLastp = true;
            if (ChaptersList.SelectedItem != null && manga != null)
            {
                if(manga.isPDF)
                {
                   // TODO : доделать страницу открытия с PDF форматои
                }
                else
                {
                    picturePage = new PicturePage(mainWindow, ChaptersList.SelectedItem as ChapterImg, this);
                    MangaPictFrame.Content = picturePage;

                    // Показываем последнюю страницу выбранной главы
                    picturePage.OpenLastPage();
                }
            }
        }

        public void OpenFirstPage()
        {
            if (picturePage != null)
            {
                picturePage.OpenFirstPage();
            }
        }

        private void ChaptersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isLastp)
            {
                isLastp = false;
                return;
            }
            else if (ChaptersList.SelectedItem != null)
            {
                if (manga.isPDF)
                {

                }
                else
                {
                    picturePage = new PicturePage(mainWindow, ChaptersList.SelectedItem as ChapterImg, this);
                    MangaPictFrame.Content = picturePage;

                    // Показываем первую страницу выбранной главы
                    picturePage.OpenFirstPage();
                }
            }
        }

        public void ListBoxPictures_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(picturePage != null)
            {
                picturePage.ListBoxPictures_PreviewKeyDown(sender, e);
            }
        }
    }

}
