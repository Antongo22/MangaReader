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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        MangaChapters mangaChapters;
        MainWindow mainWindow;

        public MainPage(MainWindow mainWindow)
        {
            InitializeComponent();

            mangaChapters = new(mainWindow);

            MangaChaptersFrame.Content = mangaChapters;
            this.mainWindow = mainWindow;

            LoadManga();
        }

        public void LoadManga()
        {
            foreach(var manga in mainWindow.mangas)
            {
                ListBoxManga.Items.Add(manga);
            }
        }

        public void LoadFilteredManga(string searchText)
        {
            ListBoxManga.Items.Clear();

            foreach (var manga in mainWindow.mangas)
            {
                if (manga.Name.ToLower().Contains(searchText.ToLower()))
                {
                    ListBoxManga.Items.Add(manga);
                }
            }
        }

        public void UpdateListsManga()
        {
            ListBoxManga.Items.Clear();
            foreach (var manga in mainWindow.mangas)
            {
                ListBoxManga.Items.Add(manga);
            }
        }

        private void ListBoxManga_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mangaChapters.ChaptersList.Items.Clear();
            mangaChapters.LoadManga(ListBoxManga.SelectedItem as Manga);
        }

        public void ListBoxPictures_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(mangaChapters != null)
            {
                mangaChapters.ListBoxPictures_PreviewKeyDown(sender, e);
            }
        }
    }
}
