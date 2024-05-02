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
using System.IO;
using System.Windows.Forms;
using MangaReader.Struct;

namespace MangaReader
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainPage mainPage;
        string findText = "Поиск манги";
        public List<Manga> mangas;

        string pathToSave = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Manga");
        
        public MainWindow()
        {
            InitializeComponent();

            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }
            GetManga();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainPage = new MainPage(this);
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

        private void AddManga_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.Description = "Выберите исходную папку для копирования";
                dialog.ShowNewFolderButton = true;

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    newName:
                    string newFolderName = Microsoft.VisualBasic.Interaction.InputBox("Введите название новой папки:", "Создание новой папки", "Новая папка");

                    if (!string.IsNullOrWhiteSpace(newFolderName))
                    {
                        string newFolderPath = Path.Combine(pathToSave, newFolderName);

                        if (Directory.Exists(newFolderPath))
                        {
                            System.Windows.MessageBox.Show("Папка с таким именем уже существует. Введите другое название.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                            goto newName;
                        }

                        Directory.CreateDirectory(newFolderPath);
                        CopyFolder(dialog.SelectedPath, newFolderPath);

                        Manga m = new(Path.GetFileName(dialog.SelectedPath), Path.Combine(pathToSave, Path.GetFileName(dialog.SelectedPath)));

                        mangas.Add(m);
                        mainPage.ListBoxManga.Items.Add(m);


                        System.Windows.MessageBox.Show("Папка успешно скопирована!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Вы не ввели название новой папки!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }


        /// <summary>
        /// Метод для копирования содержимого папки
        /// </summary>
        /// <param name="sourceFolder"></param>
        /// <param name="destFolder"></param>
        private void CopyFolder(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
            }

            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest);
            }

            // Рекурсивное копирование подпапок
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyFolder(folder, dest);
            }
        }

       void GetManga()
       {
            mangas = new List<Manga>();

            try
            {
                if (Directory.Exists(pathToSave))
                {
                    // Получаем список подпапок в указанной директории
                    string[] subdirectories = Directory.GetDirectories(pathToSave);

                    // Добавляем названия подпапок в список
                    foreach (string subdirectory in subdirectories)
                    {
                        mangas.Add(new(Path.GetFileName(subdirectory), subdirectory));
                    }

                }
            }
            catch (Exception ex) { }
        }
    }
}
