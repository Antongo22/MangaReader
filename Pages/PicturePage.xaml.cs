﻿using MangaReader.Struct;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MangaReader.Pages
{
    /// <summary>
    /// Логика взаимодействия для PicturePage.xaml
    /// </summary>
    public partial class PicturePage : Page
    {
        MainWindow mainWindow;
        Chapter chapter;

        public PicturePage(MainWindow mainWindow, Chapter chapter)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.chapter = chapter;

            LoadPictures();
        }

        void LoadPictures()
        {
            if(chapter == null)
            {
                return;
            }

            // Сортируем изображения по числовому порядку их имен
            var sortedImages = chapter.Images.OrderBy(image => GetImageNumberFromFileName(image));

            foreach (var pict in sortedImages)
            {
                ListBoxPictures.Items.Add(pict);
            }
        }

        int GetImageNumberFromFileName(Picture image)
        {
            string fileName = System.IO.Path.GetFileNameWithoutExtension(image.PictureName);

            int imageNumber;
            if (int.TryParse(fileName, out imageNumber))
            {
                return imageNumber;
            }
            else
            {
                return int.MaxValue;
            }
        }



        private void LeftPart_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void RightPart_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ListBoxPictures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxPictures.SelectedItem != null)
            {
                Picture selectedImage = (Picture)ListBoxPictures.SelectedItem;
                BitmapImage bitmapImage = selectedImage.BitmapImage;

                // Конвертируем изображение в Base64 строку
                string imageBase64 = ConvertBitmapImageToBase64(bitmapImage);

                // Генерируем HTML-код для отображения картинки
                string html = $"<html><head><style>img {{ max-width: 100%; height: auto; }}</style></head><body><img src='data:image/png;base64,{imageBase64}'></body></html>";

                // Загружаем HTML-код в WebBrowser
                WebBrowserImage.NavigateToString(html);
            }
        }

        private string ConvertBitmapImageToBase64(BitmapImage bitmapImage)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(ms);
                byte[] imageBytes = ms.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }


    }
}
