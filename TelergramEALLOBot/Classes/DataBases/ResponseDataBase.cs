using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelergramEALLOBot.Classes
{
	public static class ResponseDataBase
	{
		public static Dictionary<RequestType, List<string>> responseTypeText = new Dictionary<RequestType, List<string>>()
		{
			{ RequestType.Test, new List<string>() { "Test-huyest" } },
			{ RequestType.Greetings, new List<string>() { "Привет", "хай", "Привет-как-дела", "буэнос", "аллоха", "дарова" } } ,
			{ RequestType.WarmWater, new List<string>() { "Горячую воду дадут. В 12 по Аляске.", "Горячую воду дадут. В 12-ом миллениуме.", "Горячую воду дадут. А потом догонят, и ещё раз дадут" } } ,
			{ RequestType.WhyTumen, new List<string>() { "Потому что в Тюмени есть нефть." } } ,
			{ RequestType.WhereBestParties, new List<string>() { "У Эвоков на кухне!"} } ,
			{ RequestType.CutTheMoney, new List<string>() { "Мы пилим бюджеты только на Пасху.", "Уже пилили, все остались довольны.", "Потерпи до апреля, ага?"} } ,
			{ RequestType.AlexAndMc, new List<string>() { "Лёша не ходит в мак, там еда вредная.", "Если будете идти в мак, и мне чиз возьмите.", "А какой мак к нам ближе?" } } ,
			{ RequestType.WhatIsTheSense, new List<string>() { "Только интеграл от дельта-функции имеет смысл, и больше ничего!" } } ,
			{ RequestType.CookForMe, new List<string>() { "Эти вот холодные комочки теста в морозилке, да? Что с ними делать? Я не умею (((", "Я сварил пожаренную картошку. Такая прикольна жижа вышла" } } ,
			{ RequestType.Gileva, new List<string>() { "Вот вам смешно, а она однажды ушла..." } } ,
			{ RequestType.PleaseBuy, new List<string>() { "Да за такие деньги я лучше ничего не куплю!", "Если бы я мог продать почку - я бы это сделал. Но я не могу.", "Дай тогда пару миллионов до зарплаты, я всё потом отдам." } } ,
			{ RequestType.WhoWeAre, new List<string>() { "Эвоки!" } } ,
			{ RequestType.WhatDoWeNeed, new List<string>() { "Сахарницу, балалайку и водку!", "Горячую воду!", "Собраться и потусить на кухне!", "Помыть посуду!", "Перегрузить роутер!" } } ,
			{ RequestType.NoSongs, new List<string>() { "Не в настроении я петь. Голос простудил, воды ж горячей нет нифига."} } ,
			{ RequestType.Songs, new List<string>() { "Я вообще так себе пою, я же бот в Телеграме. Но только ради тебя, вот, послушай: " } } ,
			{ RequestType.SomethingStrange, new List<string>() { "Не знаю что и сказать. Ты хочешь странного."} } ,
			{ RequestType.BadJoke, new List<string>() { "Ты напрашиваешься на неприличную шутку.", "Я бы пошутил, да Артур меня отключит.", "Нужно держать себя в руках и не шутить про это."} } ,


		};
	}
}
