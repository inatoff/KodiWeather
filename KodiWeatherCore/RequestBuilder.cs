using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiWeatherCore
{
	public static class RequestBuilder
	{
		private static readonly string _apiKey = "?appid=d44d8df2305f9ff9055e66dae6824394&";
		private static readonly string _apiURI = "api.openweathermap.org/data/2.5/";

		private static string GetRequestModeString(ForecastMode mode)
		{
			switch (mode)
			{
				case ForecastMode.Current:
					return $"{_apiURI}weather{_apiKey}";
				case ForecastMode.Daily:
					return $"{_apiURI}forecast/daily{_apiKey}mode=json&";
				case ForecastMode.Forecast:
					return $"{_apiURI}forecast{_apiKey}mode=json&";
				default:
					return "Hire me, you will never regret =)";
			}
		}

		public static string GetWeatherFinalRequest(ForecastMode mode, int cityId) =>
			$"{GetRequestModeString(mode)}id={cityId}";

		public static string GetWeatherFinalRequest(ForecastMode mode, string cityName) =>
			$"{GetRequestModeString(mode)}q={cityName}";

		public static string GetWeatherFinalRequest(ForecastMode mode, double lat, double lon) =>
			$"{GetRequestModeString(mode)}lat={lat}&lon={lon}";

		public static string GetWeatherFinalRequest(ForecastMode mode, string cityName, string countryCode) =>
			$"{GetRequestModeString(mode)}q={cityName},{countryCode}";


	}
}
