using System;
using System.Collections.Generic;
using System.IO;

namespace BalanceReporterLocal
{
    class BalanceReporter
    {
        private readonly BalanceParser _parser = new BalanceParser();
        private BalanceAnalyser _analyser;
        private readonly BalancePrinter _printer = new BalancePrinter();
        
        public void MainMethod()
        {
            _printer.AskForPathInput();
            string path = DefinePath();

            var fileStream = File.OpenRead(path);
            _parser.ParseFromFile(fileStream);
            HashSet<Transaction> catalogue = _parser.GetTransactionCatalogue();
            
            _analyser = new BalanceAnalyser(catalogue);
            do
            {
                int command = InputCommand();
                if (!DoCommand(command))
                {
                    break;
                }
            } while (true);
        }

        string DefinePath()
        {
            string input;
            do
            {
                input = Console.ReadLine();
                if (!File.Exists(input))
                {
                    _printer.WarnInvalidPath();
                }
            } while (!File.Exists(input));
            return input;
        }

        int InputCommand()
        {
            _printer.PrintCommandMenu();
            int command;
            do
            {
                if (int.TryParse(Console.ReadLine(), out command))
                {
                    break;
                }

                _printer.WarnUnknownCommand();
            } while (true);
            return command;
        }

        bool DoCommand(int command)
        {
            DateTime startDate;
            DateTime endDate;
            Dictionary<string, double[]> traffic;
            switch (command)
            {
                case 0:
                    return false;
                case 1:
                    startDate = DefineDate("start");
                    endDate = DefineDate("end");
                    traffic = _analyser.CalculateTrafficForYears(startDate, endDate);
                    _printer.PrintTrafficData(traffic);
                    break; 
                case 2:
                    startDate = DefineDate("start");
                    endDate = DefineDate("end");
                    traffic = _analyser.CalculateTrafficForMonths(startDate, endDate);
                    _printer.PrintTrafficData(traffic);
                    break;
                case 3:
                    startDate = DefineDate("start");
                    endDate = DefineDate("end");
                    var averageIncomeAndSpending = 
                        _analyser.CalculateAverageIncomeAndSpending(startDate, endDate);
                    _printer.PrintAverageResults(averageIncomeAndSpending);
                    break;
                case 4:
                    startDate = DefineDate("start");
                    endDate = DefineDate("end");
                    var maxIncomeAndLowestSpending = 
                        _analyser.CalculateMaxIncomeAndLowestSpending(startDate, endDate);
                    _printer.PrintMaxResults(maxIncomeAndLowestSpending);
                    break;
                case 5:
                    startDate = DefineDate("start");
                    endDate = DefineDate("end");
                    var sources = 
                        _analyser.CalculateMaxIncomeAndLowestSpendingSources(startDate, endDate);
                    _printer.PrintSources(sources);
                    break;
                default:
                    break;
                    
            }

            return true;
        }
        
        
        DateTime DefineDate(string date)
        {
            _printer.AskToInputDate(date);
            string input;
            do
            {
                input = Console.ReadLine();
                if (input == "any")
                {
                    return date == "start" ? DateTime.MinValue : DateTime.MaxValue;
                }
            } while (!DateTime.TryParse(input, out _));
            return _parser.ConvertStringToDateTime(input);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var reporter = new BalanceReporter();
            reporter.MainMethod();
        }
    }
}
// /home/deckard/RiderProjects/BalanceReporterLocal/myFile0.csv