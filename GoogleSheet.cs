using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace GoogleSheet
{
    public class GoogleSheet
    {
        public string SpreadsheetId; /*Идентификатор таблицы*/
        public string sheet; /*Название листа с которым работаем*/
        public string range;
        public List<object> objectList = new List<object>() { };
    }
}
