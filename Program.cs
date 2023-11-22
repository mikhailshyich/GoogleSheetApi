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
            

            
            System.Timers.Timer timer = new System.Timers.Timer(60000);
            timer.AutoReset= true;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            ConsoleKeyInfo response = new ConsoleKeyInfo();

            Console.WriteLine("Приложение запущено");
            Console.WriteLine("Для выхода нажмите Q");
            do
            {
                response = Console.ReadKey();
                
            } while (response.Key != ConsoleKey.Q);


            Console.WriteLine("Приложение остановлено, нажмите любую кнопку");
            timer.Stop();

            Console.ReadKey();
            
        }

        private static void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
           List<PLC_CellData> plcCellMassive = new List<PLC_CellData>()
            {
                new(DataType.DataBlock, 2, 890, VarType.Real),
                new(DataType.DataBlock, 2, 910, VarType.Real),
                new(DataType.DataBlock, 22, 362, VarType.Real)
            };

            List<PLC_CellData> plcCellMassive112 = new List<PLC_CellData>()
            {
                new(DataType.DataBlock, 1, 0, VarType.Real),
                new(DataType.DataBlock, 1, 4, VarType.Real),
                new(DataType.DataBlock, 1, 16, VarType.Real),
                new(DataType.DataBlock, 1, 20, VarType.Real)
            };

            List<object> plcData = new List<object>();

            var plcReader = new PLC_Reader(CpuType.S7300, "192.168.3.20");

            var plcReader112 = new PLC_Reader(CpuType.S71200, "192.168.3.112");

            plcData.Add($"{e.SignalTime.Hour}:{e.SignalTime.Minute}:{e.SignalTime.Second}");

            for (int i = 0; plcCellMassive.Count > i; i++)
            {
                var data = plcReader.ReadValueToPLC(plcCellMassive[i]);
                plcData.Add(data);                            
                Console.WriteLine(data);
            }
            for (int i = 0; plcCellMassive112.Count > i; i++)
            {
                
                var data = plcReader112.ReadValueToPLC(plcCellMassive112[i]);
                plcData.Add(data);
                Console.WriteLine(data);
            }




            //////////////////////////
            var shSender = new GoogleDataSender("1PWxv4H1p-z-LR21uULmE0SJ6EG-WLFmJteXieF6drtg", e.SignalTime.Date.ToShortDateString().ToString());
            


            // List<object> dataMassive = [];
            // for (int i = 0; i < 10; i++)
            // {
            //     dataMassive.Add($"{e.SignalTime}+{i}");
            // }

            shSender.CreateEntry(plcData);
            Console.WriteLine($"{e.SignalTime.Date.ToShortDateString()}Данные записаны");
        }

        


    }
}