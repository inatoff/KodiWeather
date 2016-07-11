using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace KodiWeatherCore
{
	public static class WeatherProvider
	{
		public async static Task<WeatherModel> InnerGetWeatherAsync(string requestString)
		{
			var http = new HttpClient();
			var response = await http.GetAsync(requestString);
			var result = await response.Content.ReadAsStringAsync();
			//"{\"coord\":{\"lon\":30.52,\"lat\":50.43},\"weather\":[{\"id\":800,\"main\":\"Clear\",\"description\":\"clear sky\",\"icon\":\"01d\"}],\"base\":\"cmc stations\",\"main\":{\"temp\":297.49,\"pressure\":1017,\"humidity\":50,\"temp_min\":297.15,\"temp_max\":298.15},\"wind\":{\"speed\":4,\"deg\":270},\"clouds\":{\"all\":0},\"dt\":1468227600,\"sys\":{\"type\":1,\"id\":7358,\"message\":0.0045,\"country\":\"UA\",\"sunrise\":1468202393,\"sunset\":1468260394},\"id\":703448,\"name\":\"Kiev\",\"cod\":200}";
			var data = JsonConvert.DeserializeObject<WeatherModel>(result);
			return data;
		}

		public async static Task<WeatherModel> GetWeatherAsync(ForecastMode mode, int cityId)
			=> await InnerGetWeatherAsync(RequestBuilder.GetWeatherFinalRequest(mode, cityId));

		public async static Task<WeatherModel> GetWeatherAsync(ForecastMode mode, string cityName)
			=> await InnerGetWeatherAsync(RequestBuilder.GetWeatherFinalRequest(mode, cityName));

		public async static Task<WeatherModel> GetWeatherAsync(ForecastMode mode, double lat, double lon)
			=> await InnerGetWeatherAsync(RequestBuilder.GetWeatherFinalRequest(mode, lat, lon));

		public async static Task<WeatherModel> GetWeatherAsync(ForecastMode mode, string cityName, string countryCode)
			=> await InnerGetWeatherAsync(RequestBuilder.GetWeatherFinalRequest(mode, cityName, countryCode));

		public static BitmapImage GetWeatherIcon(string IconName)
			=> new BitmapImage(new Uri($"ms-appx:///Assets/WeatherIco/{IconName}.png", UriKind.Absolute));

		public static BitmapImage GetWeatherBackground(string weatherType)
		{
			string backgroundName = "";
			if (weatherType.Equals("01d"))
				backgroundName = "sunnyD";
			else if (weatherType.Equals("01n"))
				backgroundName = "moonN";
			else if (weatherType.Equals("02d") || weatherType.Equals("03d") || weatherType.Equals("04d"))
				backgroundName = "cloudyD";
			else if (weatherType.Equals("02n") || weatherType.Equals("03n") || weatherType.Equals("04n"))
				backgroundName = "cloudyN";
			else if (weatherType.Equals("09d") || weatherType.Equals("10d") || weatherType.Equals("11d"))
				backgroundName = "rainyD";
			else if (weatherType.Equals("09n") || weatherType.Equals("10n") || weatherType.Equals("11n"))
				backgroundName = "rainyN";
			else
				backgroundName = "sunnyD";
			//no images for Foggy or Snowy
			return new BitmapImage(new Uri($"ms-appx:///Assets/WeatherBackgrounds/{backgroundName}.png", UriKind.Absolute));
		}
	}
}
