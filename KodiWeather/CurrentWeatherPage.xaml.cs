using KodiWeatherCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
		static WeatherModel _model;
		public CurrentWeatherPage()
		{
			this.InitializeComponent();
		}
		

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			_model = WeatherCache.CurrentWeatherModelCache;
			PageInitialize();
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{

		}

		private async void PageInitialize()
		{
			if (_model == null)
			{
				_model = await WeatherProvider.GetWeatherAsync(ForecastMode.Current, "Kiev", "ua");
			}
			tbTemperature.Text = _model.main.temp.ToString();
			tbWindSpeed.Text = _model.wind.speed.ToString();
			CWImage.Source = WeatherProvider.GetWeatherIcon(_model.weather[0].icon);
			BackgroundInit(_model.weather[0].icon);
		}

		private void BackgroundInit(string weatherType)
		{
			BackgroundImage.Source = WeatherProvider.GetWeatherBackground(weatherType);
		}
	}
}
