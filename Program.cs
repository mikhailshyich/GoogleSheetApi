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
        
        static void Main(string[] args)
        {
            

            var shSender = new GoogleDataSender("1PWxv4H1p-z-LR21uULmE0SJ6EG-WLFmJteXieF6drtg", "List2");

            List<object> dataMassive = new List<object>();
            for (int i = 0; i < 10; i++)
            {
                dataMassive.Add($"11+{i}");
            }

            shSender.CreateEntry(dataMassive);

        }


    }
}