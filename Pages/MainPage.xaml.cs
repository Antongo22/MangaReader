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

namespace MangaReader.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        MangaChapters mangaChapters;
        public MainPage()
        {
            InitializeComponent();

            mangaChapters = new();

            MangaChaptersFrame.Content = mangaChapters;
        }

        public void LoadFilteredManga(string searchText)
        {
            
        }

        public void UpdateListsManga()
        {
            
        }
    }
}
