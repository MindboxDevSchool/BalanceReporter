using System;
using System.Collections.Generic;
using System.IO;

namespace Week1Task2 {
    internal static class Utility {
        //returns hardcoded path to CSV or [args[0]]
        public static string GetCsvPath(IReadOnlyList<string> args) {
            string resultCsvName;
            if (args.Count == 0) { //default hardcode file
                const string csvExamplePath = @"csvExamples\bankStatementsTask2_v3.csv"; // replace [csvExampleName] for test another csv file 
                var environment = Environment.CurrentDirectory;
                var directoryInfo = Directory.GetParent(environment).Parent;
                if (directoryInfo != null) {
                    var projectDirectory = directoryInfo.FullName;
                    var projectDirectoryWithoutBin = projectDirectory.Replace("\\bin", "");
                    resultCsvName = Path.Combine(projectDirectoryWithoutBin, csvExamplePath);
                }
                else {
                    throw new Exception("Directory.GetParent(System.Environment.CurrentDirectory).Parent returns null");
                }
            }
            else { //only first args[0] is used
                resultCsvName = args[0]; 
            }
            return resultCsvName;
        }
    }
}