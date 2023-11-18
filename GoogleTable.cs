using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;


namespace GoogleSheet
{
    public class GoogleTable
    {
        public string SpreadsheetId = "1PWxv4H1p-z-LR21uULmE0SJ6EG-WLFmJteXieF6drtg"; /*Идентификатор таблицы*/
        public string sheet; /*Название листа с которым работаем*/
        public string range; /*Диапазон для записи данных в таблицу*/
        public SheetsService service; /*Свойство service*/
        const string APPLICATION_NAME = "GoogleSheetApi";
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        public List<object> objectList; /*Список объектов для строки в таблице*/

        public GoogleTable()
        {
            InitializeSrevice();
        }

        public void InitializeSrevice() /*Инициализируем экземпляр SheetService*/
        {
            var credential = GetCredentialsFromFile();
            service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = APPLICATION_NAME
            });
        }

        public GoogleCredential GetCredentialsFromFile()/*Получаем доступ к учётным данным из файла*/
        {
            GoogleCredential credential;
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }
            return credential;
        }
    }
}
