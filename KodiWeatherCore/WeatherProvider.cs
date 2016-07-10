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
			var serializer = new DataContractJsonSerializer(typeof(WeatherModel));
			var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
			var data = (WeatherModel)serializer.ReadObject(ms);
			return data;
		}

		public async static Task<WeatherModel> GetWeatherAsync(ForecastMode mode, int cityId) =>
			await InnerGetWeatherAsync(RequestBuilder.GetWeatherFinalRequest(mode, cityId));

		public async static Task<WeatherModel> GetWeatherAsync(ForecastMode mode, string cityName) =>
			await InnerGetWeatherAsync(RequestBuilder.GetWeatherFinalRequest(mode, cityName));

		public async static Task<WeatherModel> GetWeatherAsync(ForecastMode mode, double lat, double lon) =>
			await InnerGetWeatherAsync(RequestBuilder.GetWeatherFinalRequest(mode, lat, lon));

		public async static Task<WeatherModel> GetWeatherAsync(ForecastMode mode, string cityName, string countryCode) =>
			await InnerGetWeatherAsync(RequestBuilder.GetWeatherFinalRequest(mode, cityName, countryCode));

		public static BitmapImage GetWeatherIcon(string IconName)
			=> new BitmapImage(new Uri($"ms-appx:///Assets/WeatherIco/{IconName}.png", UriKind.Absolute));

	}
}
