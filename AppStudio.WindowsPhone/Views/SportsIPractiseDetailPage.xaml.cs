using System;
using System.Net.NetworkInformation;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.DataTransfer;

using AppStudio.Services;
using AppStudio.ViewModels;

namespace AppStudio.Views
{
    public sealed partial class SportsIPractiseDetail : Page
    {
        private NavigationHelper _navigationHelper;

        private DataTransferManager _dataTransferManager;

        public SportsIPractiseDetail()
        {
            this.InitializeComponent();
            _navigationHelper = new NavigationHelper(this);

            SportsIPractiseModel = new SportsIPractiseViewModel();
        }

        public SportsIPractiseViewModel SportsIPractiseModel { get; private set; }

        public NavigationHelper NavigationHelper
        {
            get { return _navigationHelper; }
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += OnDataRequested;

            _navigationHelper.OnNavigatedTo(e);

            await SportsIPractiseModel.LoadItemsAsync();
            SportsIPractiseModel.SelectItem(e.Parameter);

            if (SportsIPractiseModel != null)
            {
                SportsIPractiseModel.ViewType = ViewTypes.Detail;
            }
            DataContext = this;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedFrom(e);
            _dataTransferManager.DataRequested -= OnDataRequested;
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            if (SportsIPractiseModel != null)
            {
                SportsIPractiseModel.GetShareContent(args.Request);
            }
        }
    }
}
