using System;
using System.Windows;
using System.Windows.Input;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class SportsIPractiseViewModel : ViewModelBase<SportsIPractiseSchema>
    {
        private RelayCommandEx<SportsIPractiseSchema> itemClickCommand;
        public RelayCommandEx<SportsIPractiseSchema> ItemClickCommand
        {
            get
            {
                if (itemClickCommand == null)
                {
                    itemClickCommand = new RelayCommandEx<SportsIPractiseSchema>(
                        (item) =>
                        {

                            NavigationServices.NavigateToPage("SportsIPractiseDetail", item);
                        });
                }

                return itemClickCommand;
            }
        }

        override protected DataSourceBase<SportsIPractiseSchema> CreateDataSource()
        {
            return new SportsIPractiseDataSource(); // CollectionDataSource
        }


        override public Visibility PinToStartVisibility
        {
            get { return ViewType == ViewTypes.Detail ? Visibility.Visible : Visibility.Collapsed; }
        }

        override protected void PinToStart()
        {
            base.PinToStart("SportsIPractiseDetail", "{DefaultTitle}", "{DefaultSummary}", "{DefaultImageUrl}");
        }

        override public Visibility RefreshVisibility
        {
            get { return ViewType == ViewTypes.List ? Visibility.Visible : Visibility.Collapsed; }
        }

        override public void NavigateToSectionList()
        {
            NavigationServices.NavigateToPage("SportsIPractiseList");
        }

        override protected void NavigateToSelectedItem()
        {
            NavigationServices.NavigateToPage("SportsIPractiseDetail");
        }
    }
}
