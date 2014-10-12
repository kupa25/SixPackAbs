using System;

using Windows.UI.Xaml.Controls;

using AppStudio.ViewModels;

namespace AppStudio.Views
{
    public sealed partial class AboutThisAppPage : Page
    {
        public AboutThisAppPage()
        {
            this.InitializeComponent();
            AboutModel = new AboutThisAppViewModel();
            this.DataContext = this;
        }

        public AboutThisAppViewModel AboutModel { get; private set; }

        private async void TextBlock_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var mailUri = new Uri("mailto:kupa25@live.com");
            // Launch the URI
            var success = await Windows.System.Launcher.LaunchUriAsync(mailUri);

            if (!success)
            {
                //MessageBox box = new MessageBox()
            }
        }
    }
}
