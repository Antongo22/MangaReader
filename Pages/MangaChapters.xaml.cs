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

        public MangaChapters(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
        }

        public void LoadManga(Manga manga)
        {
            if(manga == null) { return; }

            manga.GetChapters();

            foreach (var ch in manga.Chapters)
            {
                ChaptersList.Items.Add(ch);
            }
        }


        public void OpenLastPage()
        {
            isLastp = true;
            picturePage = new(mainWindow, ChaptersList.SelectedItem as Chapter, this);
            MangaPictFrame.Content = picturePage;

            var index = picturePage.ListBoxPictures.Items.Count - 1;
            picturePage.ListBoxPictures.SelectedItem = index;
        }

        public void OpenFirstPage()
        {
            var firstImage = (picturePage.ListBoxPictures.Items.Count > 0) ? picturePage.ListBoxPictures.Items[0] : null;

            picturePage.ListBoxPictures.SelectedItem = firstImage;
        }

        private void ChaptersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isLastp)
            {
                System.Windows.Forms.MessageBox.Show("Test");
                isLastp = false;
                return;
            }

            if (ChaptersList.SelectedItem != null)
            {
                picturePage = new(mainWindow, ChaptersList.SelectedItem as Chapter, this);
                MangaPictFrame.Content = picturePage;

                OpenFirstPage();
            }
        }
    }
    
}
