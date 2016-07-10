using KodiWeatherCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace KodiWeather
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class CurrentWeatherPage : Page
	{
		public CurrentWeatherPage()
		{
			this.InitializeComponent();
		}
		

		private async void Page_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				WeatherModel model = new WeatherModel();
				model = await WeatherProvider.GetWeatherAsync(ForecastMode.Current,"Kiev","ua");
				tbTemperature.Text = model.main.temp.ToString();
				tbWindSpeed.Text = model.wind.speed.ToString();
				CWImage.Source = WeatherProvider.GetWeatherIcon(model.weather[0].icon);
			}
			catch
			{
				tbTemperature.Text = "shit happens";
			}
		}
	}
}
