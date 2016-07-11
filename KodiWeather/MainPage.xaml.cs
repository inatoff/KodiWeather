using KodiWeatherCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace KodiWeather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
		static Type[] appPages = new Type[] {
			typeof(MainPage),
			typeof(CurrentWeatherPage),
			typeof(HourlyForecastPage),
			typeof(DailyForecastPage),
			typeof(WeatherMapPage),
			typeof(ContactMePage) };

		public MainPage()
        {
            this.InitializeComponent();
			GetCurrentWeatherModel();
        }

		private void SplitMenuButton_Click(object sender, RoutedEventArgs e)
		{
			MenuSplitView.IsPaneOpen = !MenuSplitView.IsPaneOpen;
		}
		
		private void IconsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{			
			switch (IconsListBox.Items.IndexOf(IconsListBox.SelectedItem))
			{
				case 0:
					MainWindowFrame.Navigate(appPages[1]);
					break;
				case 1:
					MainWindowFrame.Navigate(appPages[2]);
					break;
				case 2:
					MainWindowFrame.Navigate(appPages[3]);
					break;
				case 3:
					MainWindowFrame.Navigate(appPages[4]);
					break;
				case 4:
					MainWindowFrame.Navigate(appPages[5]);
					break;
				default:
					break;
			}
		}

		private async void GetCurrentWeatherModel()
		{
			WeatherCache.CurrentWeatherModelCache = await WeatherProvider.GetWeatherAsync(ForecastMode.Current,"Kiev","ua");
		}
	}
}
