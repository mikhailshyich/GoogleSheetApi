using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Timers;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using S7.Net;


namespace GoogleSheet
{
    //сслыка на GoogleSheet - https://docs.google.com/spreadsheets/d/1PWxv4H1p-z-LR21uULmE0SJ6EG-WLFmJteXieF6drtg/edit#gid=0
    class Program
    {
        
        static void Main(string[] args)
        {


            /*var shSender = new GoogleDataSender("1PWxv4H1p-z-LR21uULmE0SJ6EG-WLFmJteXieF6drtg", "List2");

            List<object> dataMassive = new List<object>();
            for (int i = 0; i < 10; i++)
            {
                dataMassive.Add($"11+{i}");
            }

            shSender.CreateEntry(dataMassive);*/
            /*
            using (var plc = new Plc(CpuType.S71200, "192.168.3.101", 0, 0))
            {
                plc.Open();
                Console.WriteLine(plc.IsConnected);
                plc.Close();

                

            }*/
            System.Threading.Timer MyTimer = new System.Threading.Timer(OnTimedEvent, null, 0, 3000);

           


            Console.ReadKey();
            
        }
        private static void OnTimedEvent(object source)
        {
            Console.WriteLine("Hello");
            
        }


    }
}