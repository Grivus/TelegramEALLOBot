# TelegramEALLOBot
MSVS 2015 solution.

Бот для внутреннего пользования в телеграме. 

Есть два способа разнообразить его поведение:

1) Файлы RequestsTypeDataBase и ResponseDataBase содержат типы сообщений с тегами, 
по которым они опознаются и заготовки для ответов на эти типы. Добавляя свои типы 
с тегами и свои ответы можно учить бота новым веткам диалога.
2) Можно создавать специфическое программное поведение по своим придуманным 
признакам. Идём в папку SpecialCommands, создаём свой класс, наследуем его от 
ISpecialCommandBuilder, реализуем там интерфейс, логику, делаем фабричный метод 
GetBuilder возвращающий ISpecialCommandBuilderи принимающий ParsedMessage, делаем 
новый тип специальной команды в enum-ке MessageRequestType в файле ParsedMessage, 
дальше идём в файл SpecialCommandsProcessorsMap, и там заносим соответствие между 
новым типом и фабричным методом в словарь. После этого, (осталось немного, правда) 
идём в MessageParser и дописываем там определение типа специальной команды 
(когда-нибудь там будет отдельный список парсеров).После этого можно запустить 
бота и он должен выполнять новую команду!
