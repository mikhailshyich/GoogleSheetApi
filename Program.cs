using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace GoogleSheet
{
    //сслыка на GoogleSheet - https://docs.google.com/spreadsheets/d/1PWxv4H1p-z-LR21uULmE0SJ6EG-WLFmJteXieF6drtg/edit#gid=0
    class Program
    {
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets }; /*Для доступа только к таблицам*/
        static readonly string ApplicationName = "GoogleSheets"; /*Название приложения*/
        static readonly string SpreadsheetId = "1PWxv4H1p-z-LR21uULmE0SJ6EG-WLFmJteXieF6drtg"; /*Идентификатор таблицы*/
        static readonly string sheet = "List"; /*Название листа с которым работаем*/
        static SheetsService service;

        static void Main(string[] args)
        {
            GoogleCredential credential; /*Получаем доступ к учётным данным*/
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }

            service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            MainMenu();
        }

        public static void MainMenu()
        {
            Console.WriteLine("Меню программы (выберите нужную цифру):\n1. Добавить строку в таблицу.\n2. Выход.");
            string user_answer = Console.ReadLine();
            if (user_answer == "1")
            {
                CreateEntry();
            }
            else if (user_answer == "2")
            {
                Process.GetCurrentProcess().Kill();
            }
            else
            {
                Console.WriteLine("Такого пункта меню нет! Выберите действие из предложенных.");
                MainMenu();
            }
        }

        public static void CreateEntry() /*Метод добавления строки в таблицу*/
        {
            var range = $"{sheet}!A:Z";
            var valueRange = new ValueRange();
            var objectList = new List<object>() { }; /*Список значений*/

            Console.WriteLine("Выбран пункт меню 'Добавить строку в таблицу'");
            Console.WriteLine("Ввести значение д / н ? Вернуться в главное меню - 0");
            string answer = "";
            answer = Console.ReadLine();

            if (answer == "д") {
                while (answer == "д")
                {
                    Console.WriteLine("Значение строки:");
                    string text = Console.ReadLine();
                    objectList.Add($"{text}");
                    Console.WriteLine($"Вы добавили - {text}");
                    Console.WriteLine("Продолжаем д / н ?");
                    answer = Console.ReadLine();
                }
            }
            else if (answer == "0")
            {
                MainMenu();
            }
            else
            {
                Console.WriteLine("Введённое значение не распознано. Переходим в главное меню.");
                MainMenu();
            }
                
            
            valueRange.Values = new List<IList<object>> { objectList };

            var appendRequest = service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendResponse = appendRequest.Execute();

            Console.WriteLine("Запись добавлена");
            MainMenu();
        }

    }
}