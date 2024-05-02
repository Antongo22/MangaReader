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

        private void ChaptersList_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            picturePage = new(mainWindow, ChaptersList.SelectedItem as Chapter);
            MangaPictFrame.Content = picturePage;

        }
    }
}
