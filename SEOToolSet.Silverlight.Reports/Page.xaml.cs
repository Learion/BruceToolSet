#region Using Directives

using System;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

#endregion

namespace SEOToolSet.Silverlight.Reports
{
    public partial class Page
    {
        private BitmapImage _bitmapImage;

        public Page(String idControlHost)
        {
            IdControlHost = idControlHost;
            InitializeComponent();
        }

        public String IdControlHost { get; set; }

        private void BtnRandomHeight_Click(object sender, RoutedEventArgs e)
        {
            if (_bitmapImage == null)
            {
                _bitmapImage = new BitmapImage();
            }

            _bitmapImage.DownloadProgress += OnProgress;
            var newImage = new Image {Source = _bitmapImage, Name = "dinamycImage"};
            //BasicLayout.Children.Add(newImage);
            ImageHolder.Children.Clear();
            ImageHolder.Children.Add(newImage);


            _bitmapImage.UriSource = new Uri(ImageUrl.Text, UriKind.Absolute);
        }

        private void OnProgress(object s, DownloadProgressEventArgs args)
        {
            if (args.Progress != 100) return;

            Dispatcher.BeginInvoke(() =>
                                       {
                                           var img = ImageHolder.FindName("dinamycImage") as Image;
                                           if (img != null)
                                               HtmlPage.Window.CreateInstance("xHeight",
                                                                              String.Format("{0}px",
                                                                                            img.ActualHeight + 40));
                                       });

            _bitmapImage.DownloadProgress -= OnProgress;
        }
    }
}