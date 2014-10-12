using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using AppStudio.ViewModels;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace AppStudio.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CameraPage : Page
    {
        private CameraPageViewModel _cameraPageViewModel = null;

        private NavigationHelper _navigationHelper;

        private DataTransferManager _dataTransferManager;
        private CameraCapture cameraCapture;

        public CameraPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            _navigationHelper = new NavigationHelper(this);
            
            _cameraPageViewModel = _cameraPageViewModel ?? new CameraPageViewModel();

            DataContext = this;
            
            ApplicationView.GetForCurrentView().
                SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
        }

        public CameraPageViewModel CameraPageViewModel
        {
            get { return _cameraPageViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return _navigationHelper; }
        }

        #region NavigationHelper registration
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _navigationHelper.OnNavigatedTo(e);

            cameraCapture = new CameraCapture();
            CameraPreview.Source = await cameraCapture.Initialize();
            await cameraCapture.StartPreview();            
        }

        protected override async void OnNavigatedFrom(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedFrom(e);

            // Release resources
            if (cameraCapture != null)
            {
                await cameraCapture.StopPreview();
                CameraPreview.Source = null;
                cameraCapture.Dispose();
                cameraCapture = null;
            }
        }
        #endregion

        private async void btnClick_Click(object sender, RoutedEventArgs e)
        {
            // Take snapshot and add to ListView
            // Disable button to prevent exception due to parallel capture usage
            btnClick.IsEnabled = false;
            var photoStorageFile = await cameraCapture.CapturePhoto();

            var bitmap = new BitmapImage();
            await bitmap.SetSourceAsync(await photoStorageFile.OpenReadAsync());

            btnClick.IsEnabled = true;
        }

    }
}
