using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelergramEALLOBot.Classes
{
	public enum RequestType
	{
		Test,
		Greetings,
		WarmWater,
		WhyTumen,
		WhereBestParties,
		CutTheMoney,
		AlexAndMc,
		WhatIsTheSense,
		CookForMe,	
		Gileva,
		PleaseBuy,
		WhoWeAre,
		WhatDoWeNeed,
		NoSongs,
		Songs,
		SomethingStrange,
		BadJoke,
		GetKittensWeather,
		NoSongsFound,
		DoorsLocked,
	}

	public static class RequestsTypeDataBase
	{
		public static Dictionary<RequestType, List<string>> requestTypeTags = new Dictionary<RequestType, List<string>>()
		{
			{ RequestType.Test, new List<string>() { "test", "тест" } } ,
			{ RequestType.Greetings, new List<string>() { "привет", "хай", "приветкакдела", "как сам", "аллоха", "дарова" } } ,
			{ RequestType.WarmWater, new List<string>() { "когда", "горячую", "воду", "горячая", "вода", "тёплая", "киевэнерго", "водоканал", "жэк" } } ,
			{ RequestType.WhyTumen, new List<string>() { "почему", "тюмени", "всё", "есть" } } ,
			{ RequestType.WhereBestParties, new List<string>() { "где", "лучшие", "тусовки"} } ,
			{ RequestType.CutTheMoney, new List<string>() { "давайте", "нужно", "распилим", "распилить", "бюджет"} } ,
			{ RequestType.AlexAndMc, new List<string>() { "лёша", "алексей", "лёха", "леша", "леха", "мак", "мак-дак", "пойдёшь", "когда", "сегодня", "сходишь", "собирался", "чиз", "картошку" } } ,
			{ RequestType.WhatIsTheSense, new List<string>() { "смысл", "жизни" } } ,
			{ RequestType.CookForMe, new List<string>() { "приготовь", "еды", "пожарь", "еда", "еду", "покушать" } } ,
			{ RequestType.Gileva, new List<string>() { "гилёва", "гильова" } } ,
			{ RequestType.PleaseBuy, new List<string>() { "купи" } } ,
			{ RequestType.WhoWeAre, new List<string>() { "кто", "мы" } } ,
			{ RequestType.WhatDoWeNeed, new List<string>() { "что","чего", "мы","хотим", "нужно", "нам"} } ,
			//{ RequestType.NoSongs, new List<string>() { "спой","споём", "споем","петь"} } ,
			{ RequestType.Songs, new List<string>() { "спой","споём", "споем","петь"} } ,
			{ RequestType.SomethingStrange, new List<string>() { "странное","странно"} } ,
			{ RequestType.BadJoke, new List<string>() { "шутка","пошути", "пошутил"} } ,
			{ RequestType.GetKittensWeather, new List<string>() { "расскажи","котятах", "погоду"} } ,
			{ RequestType.DoorsLocked, new List<string>() { "дверь","закрыл", "закрыла", "закрыта"} } ,

		};

	}
}
