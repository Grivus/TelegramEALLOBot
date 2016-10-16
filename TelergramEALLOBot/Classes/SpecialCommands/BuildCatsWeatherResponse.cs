using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelergramEALLOBot.Classes.SpecialCommands
{
	public class BuildCatsWeatherResponse : ISpecialCommandBuilder
	{
		public enum WeatherType
		{
			Good,
			Bad,
			Normal
		}

		public static BuildCatsWeatherResponse GetBuilder( ParsedMessage message )
		{
			return new BuildCatsWeatherResponse( message );
		}

		public BuildCatsWeatherResponse( ParsedMessage aMessage )
		{
			message = aMessage;
		}

		public string GetMessage()
		{
			string result = "";

			var weather = GetWeather();

			var kitten = GetKitten();

			result = weather + "\n" + kittensTextByWeatherType[weatherType] + "\n" +  kitten;

			return result;
		}

		private string GetKitten()
		{
			string Url = "";

			switch ( weatherType )
			{
				case WeatherType.Good:
					Url = "https://www.pinterest.com/search/pins/?q=happy%20kitty";
				break;
				case WeatherType.Normal:
					return normalCatsUrls[ Utils.GetRandomIndex( normalCatsUrls.Count ) ];
				case WeatherType.Bad:
					Url = "https://www.pinterest.com/search/pins/?q=sad%20kitty";
				break;
			}
			

			HtmlWeb web = new HtmlWeb();
			HtmlDocument doc = web.Load( Url );

			HtmlNode contentNode = Utils.FindRecursiveByClass( "Module SearchPageContent", doc.DocumentNode );
			HtmlNode tempNode1 = contentNode.ChildNodes[ 1 ];

			var maxChildren = tempNode1.ChildNodes.Max( x => x.ChildNodes.Count );
			HtmlNode containerNode = tempNode1.ChildNodes[0];
			foreach ( var child in tempNode1.ChildNodes )
			{
				if ( child.ChildNodes.Count == maxChildren )
					containerNode = child;
			}

			List<string> linksToKittens = new List<string>();

			foreach ( var element in containerNode.ChildNodes )
			{
				var finded = Utils.FindRecursiveByClass( "pinImg fullBleed", element );
				if ( finded != null )
				{
					string link = finded.Attributes[ "src" ].Value;
					linksToKittens.Add( link );
				}
			}

			return linksToKittens[ Utils.GetRandomIndex( linksToKittens.Count ) ];
		}

		private string GetWeather()
		{
			var cityCode = GetCityCode();//"4944";

			string Url = "https://www.gismeteo.ru/city/daily/" + cityCode + "/";
			HtmlWeb web = new HtmlWeb();
			HtmlDocument doc = web.Load( Url );

			HtmlNode temperatureNode = Utils.FindRecursiveByClass( "value m_temp c", doc.DocumentNode );

			string text = temperatureNode.InnerText;

			int temperature = Int32.Parse( text.Split('&')[0] );

			weatherType = WeatherType.Normal;

			switch ( DateTime.Now.Month )
			{
				case 6:
				case 7:
				case 8:
					if ( temperature > averageTemperatureByMonth[ DateTime.Now.Month ] + normalDiffTemperatureByMonth[ DateTime.Now.Month ].Key ||
						 temperature < averageTemperatureByMonth[ DateTime.Now.Month ] + normalDiffTemperatureByMonth[ DateTime.Now.Month ].Value )
					{
						weatherType = WeatherType.Bad;
					}
				break;

				default:
					if ( temperature > averageTemperatureByMonth[ DateTime.Now.Month ] + normalDiffTemperatureByMonth[ DateTime.Now.Month ].Key )
					{
						weatherType = WeatherType.Good;
					}
					else if ( temperature < averageTemperatureByMonth[ DateTime.Now.Month ] + normalDiffTemperatureByMonth[ DateTime.Now.Month ].Value )
					{
						weatherType = WeatherType.Bad;
					}
				break;
			}
			
			var tempNode = Utils.FindRecursiveByAttributeName( "class", "cloudness", doc.DocumentNode );

			string textDescription = Utils.FindRecursiveByClass( "png", tempNode ).Attributes[ "title" ].Value;

			if ( textDescription.ToLower() == "гроза" || textDescription == "дождь" || textDescription == "ливень" )
				weatherType = WeatherType.Bad;

			return "Там " + textDescription.ToLower() + ", " + temperature.ToString() + "°C";
		}

		private string GetCityCode()
		{
			//FIXME: костыль :( 
			if ( message.wordsTokens.Contains( "киев" ) || message.wordsTokens.Contains( "киеве" ) || message.wordsTokens.Contains( "киеву" ) )
				return "4944";

			if ( message.wordsTokens.Contains( "днепр" ) || message.wordsTokens.Contains( "днепре" ) || message.wordsTokens.Contains( "днепру" ) ||
				message.wordsTokens.Contains( "днепропетровск" ) || message.wordsTokens.Contains( "днепропетровску" ) || message.wordsTokens.Contains( "днепропетровске" ) )
				return "5077";


			//Валидно, пока Женя не переедет в Киев.
			if ( message.rawMessage.From.Username == "i_am_a_stupid_fox" )
				return "5077";

			return "4944";
		}

		private ParsedMessage message;

		private WeatherType weatherType;

		private Dictionary<int, int> averageTemperatureByMonth = new Dictionary<int, int>()
		{
			{ 1, -3 },
			{ 2, -3 },
			{ 3, 5 },
			{ 4, 13 },
			{ 5, 20 },
			{ 6, 23 },
			{ 7, 26 },
			{ 8, 24 },
			{ 9, 19 },
			{ 10, 11 },
			{ 11, 5 },
			{ 12, -2 }
		};

		private Dictionary<int, KeyValuePair<int,int>> normalDiffTemperatureByMonth = new Dictionary<int, KeyValuePair<int, int>>()
		{
			{ 1, new KeyValuePair<int, int>(3, -7) },
			{ 2, new KeyValuePair<int, int>(3, -5) },
			{ 3, new KeyValuePair<int, int>(3, -3) },
			{ 4, new KeyValuePair<int, int>(5, -4) },
			{ 5, new KeyValuePair<int, int>(3, -7) },
			{ 6, new KeyValuePair<int, int>(6, -4) },
			{ 7, new KeyValuePair<int, int>(5, -7) },
			{ 8, new KeyValuePair<int, int>(7, -7) },
			{ 9, new KeyValuePair<int, int>(3, -6) },
			{ 10, new KeyValuePair<int, int>(6, -5) },
			{ 11, new KeyValuePair<int, int>(10, -5) },
			{ 12, new KeyValuePair<int, int>(3, -7) }
		};

		private List<string> normalCatsUrls = new List<string>()
		{
			"https://pbs.twimg.com/profile_images/706844157093027840/2Aan_aSU.jpg",
			"https://yt3.ggpht.com/-8l25EFsz0-g/AAAAAAAAAAI/AAAAAAAAAAA/Dd_7MUaGT4A/s900-c-k-no-mo-rj-c0xffffff/photo.jpg",
			"http://teecraze.com/wp-content/uploads/2016/08/business-cat.jpg",
			"http://i.imgur.com/5XrEmdb.jpg",
			"http://i.imgur.com/F9LuhFN.jpg",
			"https://thumbs.dreamstime.com/z/business-cat-isolated-white-55804584.jpg",

		};

		private Dictionary<WeatherType, string> kittensTextByWeatherType = new Dictionary<WeatherType, string>()
		{
			{WeatherType.Bad, "Котик смотрит на погоду и морщится. Котик умный, он не пойдёт на улицу." },
			{WeatherType.Normal, "Котик одевается по-деловому и выходит на улицу. Там обычная погода." },
			{WeatherType.Good, "Котик с радостью и блеском в глазах выбегает из дому. На улице клёво!" },
			
		};
	}
}
