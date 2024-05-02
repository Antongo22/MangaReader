using MangaReader.Pages;
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

namespace MangaReader
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainPage mainPage;
        string findText = "Поиск манги";
        
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainPage = new MainPage();
            MainFrame.Content = mainPage;
        }

        #region Поиск по почтам
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (FindManga.Text == findText)
            {
                FindManga.Text = "";
                FindManga.BorderThickness = new Thickness(1);
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FindManga.Text))
            {
                FindManga.Text = findText;
                FindManga.BorderThickness = new Thickness(1);
            }
        }

        private void FindMail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(mainPage == null)
            {
                return;
            }
            

            if (string.IsNullOrEmpty(FindManga.Text) || FindManga.Text == findText)
            {
                FindManga.BorderThickness = new Thickness(0);
                ButtonTextClear.Visibility = Visibility.Hidden;

                mainPage.UpdateListsManga();
                return;
            }

            ButtonTextClear.Visibility = Visibility.Visible;


            FindManga.BorderThickness = new Thickness(1);

            string searchText = FindManga.Text.ToLower();
            mainPage.LoadFilteredManga(searchText);

        }


        private void ClearText_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(FindManga.Text))
            {
                FindManga.Text = "";
            }
            FindManga.Text = findText;
            FindManga.Foreground = (System.Windows.Media.Brush)new BrushConverter().ConvertFromString("#f5f6f8");
        }

        #endregion

    }
}
