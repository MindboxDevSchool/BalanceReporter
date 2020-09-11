using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace BalanceReporter.Tests
{
    public class BalanceReporterTests
    {
        [Test]
        public void LoadCsvWithHeaders_ValidCsvFile_SuccessLoad()
        {
            // Arrange
            var csvDataOriginal = new List<Dictionary<string, dynamic>>()
            {
                new Dictionary<string, dynamic>()
                {
                    {"date", "01/04/2005"},
                    {"subject", "Logan"},
                    {"amount", "-107.75"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "12/18/2007"},
                    {"subject", "Ethan"},
                    {"amount", "-458.88"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "12/05/2002"},
                    {"subject", "Mia"},
                    {"amount", "-245.08"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "02/21/2019"},
                    {"subject", "James"},
                    {"amount", "36.51"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/05/2014"},
                    {"subject", "Noah"},
                    {"amount", "395.31"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/25/2001"},
                    {"subject", "Logan"},
                    {"amount", "348.12"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "07/21/2014"},
                    {"subject", "Emma"},
                    {"amount", "-202.71"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/31/2004"},
                    {"subject", "Emma"},
                    {"amount", "-48.36"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "02/17/2005"},
                    {"subject", "Mia"},
                    {"amount", "-115.15"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "04/17/2006"},
                    {"subject", "Ethan"},
                    {"amount", "-104.69"}
                }
            };
            // Act
            var csvData = Program.LoadCsvWithHeaders("TestValideCsv.csv");
            // Assert
            Assert.AreEqual(csvDataOriginal, csvData);

            Assert.Pass();
        }

        [Test]
        public void LoadCsvWithHeaders_NoCsvFile_ThrowsNoFileException()
        {
            // Arrange
            // Act
            // Assert
            var exception = Assert.Throws<Exception>(() => Program.LoadCsvWithHeaders("TestNoexistCsv.csv"));
            Assert.That(exception.Message == "File not found!");
        }

        [Test]
        public void LoadCsvWithHeaders_CsvFileWithBadHeaders_ThrowsInvalidHeadersException()
        {
            // Arrange
            // Act
            // Assert
            var exception = Assert.Throws<Exception>(() => Program.LoadCsvWithHeaders("TestInvalideCsv.csv"));
            Assert.That(exception.Message == "The amount of data per line and heading differ!");
        }

        [Test]
        public void CsvStringParser_ValidStringToParse_SuccessParsed()
        {
            // Arrange
            const string stringToParse = "lolix, kekeks, memebs";
            var parsedStringListOriginal = new List<string>() {"lolix", "kekeks", "memebs"};
            // Act
            var parsedStringList = Program.CsvStringParser(stringToParse);
            // Assert
            Assert.AreEqual(parsedStringListOriginal, parsedStringList);
        }

        [Test]
        public void CsvStringParser_EmptyStringToParse_SuccessParsed()
        {
            // Arrange
            const string stringToParse = "";
            var parsedStringListOriginal = new List<string>();
            // Act
            var parsedStringList = Program.CsvStringParser(stringToParse);
            // Assert
            Assert.AreEqual(parsedStringListOriginal, parsedStringList);
        }

        [Test]
        public void CalculateStatisticsOfMovementOfFundsByMonths_ValidBankData_SuccessCalculated()
        {
            // Arrange
            var bankData = new List<Dictionary<string, dynamic>>()
            {
                new Dictionary<string, dynamic>()
                {
                    {"date", "01/04/2005"},
                    {"subject", "Logan"},
                    {"amount", "-107.75"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "12/18/2007"},
                    {"subject", "Ethan"},
                    {"amount", "-458.88"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "12/05/2002"},
                    {"subject", "Mia"},
                    {"amount", "-245.08"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "02/21/2019"},
                    {"subject", "James"},
                    {"amount", "36.51"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/05/2014"},
                    {"subject", "Noah"},
                    {"amount", "395.31"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/25/2001"},
                    {"subject", "Logan"},
                    {"amount", "348.12"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "07/21/2014"},
                    {"subject", "Emma"},
                    {"amount", "-202.71"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/31/2004"},
                    {"subject", "Emma"},
                    {"amount", "-48.36"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "02/17/2005"},
                    {"subject", "Mia"},
                    {"amount", "-115.15"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "04/17/2006"},
                    {"subject", "Ethan"},
                    {"amount", "-104.69"}
                }
            };
            var statisticsOfMovementOfFundsByMonthsOriginal = new Dictionary<int, Dictionary<int, double>>()
            {
                {
                    2001, new Dictionary<int, double>()
                    {
                        {1, 0},
                        {2, 0},
                        {3, 0},
                        {4, 0},
                        {5, 0},
                        {6, 0},
                        {7, 0},
                        {8, 348.12},
                        {9, 0},
                        {10, 0},
                        {11, 0},
                        {12, 0}
                    }
                },
                {
                    2002, new Dictionary<int, double>()
                    {
                        {1, 0},
                        {2, 0},
                        {3, 0},
                        {4, 0},
                        {5, 0},
                        {6, 0},
                        {7, 0},
                        {8, 0},
                        {9, 0},
                        {10, 0},
                        {11, 0},
                        {12, -245.08}
                    }
                },
                {
                    2004, new Dictionary<int, double>()
                    {
                        {1, 0},
                        {2, 0},
                        {3, 0},
                        {4, 0},
                        {5, 0},
                        {6, 0},
                        {7, 0},
                        {8, -48.36},
                        {9, 0},
                        {10, 0},
                        {11, 0},
                        {12, 0}
                    }
                },
                {
                    2005, new Dictionary<int, double>()
                    {
                        {1, -107.75},
                        {2, -115.15},
                        {3, 0},
                        {4, 0},
                        {5, 0},
                        {6, 0},
                        {7, 0},
                        {8, 0},
                        {9, 0},
                        {10, 0},
                        {11, 0},
                        {12, 0}
                    }
                },
                {
                    2006, new Dictionary<int, double>()
                    {
                        {1, 0},
                        {2, 0},
                        {3, 0},
                        {4, -104.69},
                        {5, 0},
                        {6, 0},
                        {7, 0},
                        {8, 0},
                        {9, 0},
                        {10, 0},
                        {11, 0},
                        {12, 0}
                    }
                },
                {
                    2007, new Dictionary<int, double>()
                    {
                        {1, 0},
                        {2, 0},
                        {3, 0},
                        {4, 0},
                        {5, 0},
                        {6, 0},
                        {7, 0},
                        {8, 0},
                        {9, 0},
                        {10, 0},
                        {11, 0},
                        {12, -458.88}
                    }
                },
                {
                    2014, new Dictionary<int, double>()
                    {
                        {1, 0},
                        {2, 0},
                        {3, 0},
                        {4, 0},
                        {5, 0},
                        {6, 0},
                        {7, -202.71},
                        {8, 395.31},
                        {9, 0},
                        {10, 0},
                        {11, 0},
                        {12, 0}
                    }
                },
                {
                    2019, new Dictionary<int, double>()
                    {
                        {1, 0},
                        {2, 36.51},
                        {3, 0},
                        {4, 0},
                        {5, 0},
                        {6, 0},
                        {7, 0},
                        {8, 0},
                        {9, 0},
                        {10, 0},
                        {11, 0},
                        {12, 0}
                    }
                }
            };
            // Act
            var statisticsOfMovementOfFundsByMonths = Program.CalculateStatisticsOfMovementOfFundsByMonths(bankData);
            // Assert
            for (var i = 1; i < 12; i++)
            {
                Assert.AreEqual(statisticsOfMovementOfFundsByMonthsOriginal[2001][i],
                    statisticsOfMovementOfFundsByMonths[2001][i], 0.01);
            }

            for (var i = 1; i < 12; i++)
            {
                Assert.AreEqual(statisticsOfMovementOfFundsByMonthsOriginal[2002][i],
                    statisticsOfMovementOfFundsByMonths[2002][i], 0.01);
            }

            for (var i = 1; i < 12; i++)
            {
                Assert.AreEqual(statisticsOfMovementOfFundsByMonthsOriginal[2004][i],
                    statisticsOfMovementOfFundsByMonths[2004][i], 0.01);
            }

            for (var i = 1; i < 12; i++)
            {
                Assert.AreEqual(statisticsOfMovementOfFundsByMonthsOriginal[2005][i],
                    statisticsOfMovementOfFundsByMonths[2005][i], 0.01);
            }

            for (var i = 1; i < 12; i++)
            {
                Assert.AreEqual(statisticsOfMovementOfFundsByMonthsOriginal[2006][i],
                    statisticsOfMovementOfFundsByMonths[2006][i], 0.01);
            }

            for (var i = 1; i < 12; i++)
            {
                Assert.AreEqual(statisticsOfMovementOfFundsByMonthsOriginal[2007][i],
                    statisticsOfMovementOfFundsByMonths[2007][i], 0.01);
            }

            for (var i = 1; i < 12; i++)
            {
                Assert.AreEqual(statisticsOfMovementOfFundsByMonthsOriginal[2014][i],
                    statisticsOfMovementOfFundsByMonths[2014][i], 0.01);
            }

            for (var i = 1; i < 12; i++)
            {
                Assert.AreEqual(statisticsOfMovementOfFundsByMonthsOriginal[2019][i],
                    statisticsOfMovementOfFundsByMonths[2019][i], 0.01);
            }
        }

        [Test]
        public void CalculateStatisticsOfMovementOfFundsByYears_ValidBankData_SuccessCalculated()
        {
            // Arrange
            var bankData = new List<Dictionary<string, dynamic>>()
            {
                new Dictionary<string, dynamic>()
                {
                    {"date", "01/04/2005"},
                    {"subject", "Logan"},
                    {"amount", "-107.75"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "12/18/2007"},
                    {"subject", "Ethan"},
                    {"amount", "-458.88"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "12/05/2002"},
                    {"subject", "Mia"},
                    {"amount", "-245.08"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "02/21/2019"},
                    {"subject", "James"},
                    {"amount", "36.51"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/05/2014"},
                    {"subject", "Noah"},
                    {"amount", "395.31"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/25/2001"},
                    {"subject", "Logan"},
                    {"amount", "348.12"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "07/21/2014"},
                    {"subject", "Emma"},
                    {"amount", "-202.71"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/31/2004"},
                    {"subject", "Emma"},
                    {"amount", "-48.36"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "02/17/2005"},
                    {"subject", "Mia"},
                    {"amount", "-115.15"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "04/17/2006"},
                    {"subject", "Ethan"},
                    {"amount", "-104.69"}
                }
            };
            var statisticsOfMovementOfFundsByYearsOriginal = new Dictionary<int, double>()
            {
                {2001, 348.12},
                {2002, -245.08},
                {2004, -48.36},
                {2005, -222.9},
                {2006, -104.69},
                {2007, -458.88},
                {2014, 192.6},
                {2019, 36.51},
            };
            // Act
            var statisticsOfMovementOfFundsByYears = Program.CalculateStatisticsOfMovementOfFundsByYears(bankData);
            // Assert
            Assert.AreEqual(statisticsOfMovementOfFundsByYearsOriginal[2001], statisticsOfMovementOfFundsByYears[2001],
                0.01);
            Assert.AreEqual(statisticsOfMovementOfFundsByYearsOriginal[2002], statisticsOfMovementOfFundsByYears[2002],
                0.01);
            Assert.AreEqual(statisticsOfMovementOfFundsByYearsOriginal[2004], statisticsOfMovementOfFundsByYears[2004],
                0.01);
            Assert.AreEqual(statisticsOfMovementOfFundsByYearsOriginal[2005], statisticsOfMovementOfFundsByYears[2005],
                0.01);
            Assert.AreEqual(statisticsOfMovementOfFundsByYearsOriginal[2006], statisticsOfMovementOfFundsByYears[2006],
                0.01);
            Assert.AreEqual(statisticsOfMovementOfFundsByYearsOriginal[2007], statisticsOfMovementOfFundsByYears[2007],
                0.01);
            Assert.AreEqual(statisticsOfMovementOfFundsByYearsOriginal[2014], statisticsOfMovementOfFundsByYears[2014],
                0.01);
            Assert.AreEqual(statisticsOfMovementOfFundsByYearsOriginal[2019], statisticsOfMovementOfFundsByYears[2019],
                0.01);
        }

        [Test]
        public void CalculateAverageIncome_ValidBankData_SuccessCalculated()
        {
            // Arrange
            var bankData = new List<Dictionary<string, dynamic>>()
            {
                new Dictionary<string, dynamic>()
                {
                    {"date", "01/04/2005"},
                    {"subject", "Logan"},
                    {"amount", "-107.75"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "12/18/2007"},
                    {"subject", "Ethan"},
                    {"amount", "-458.88"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "12/05/2002"},
                    {"subject", "Mia"},
                    {"amount", "-245.08"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "02/21/2019"},
                    {"subject", "James"},
                    {"amount", "36.51"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/05/2014"},
                    {"subject", "Noah"},
                    {"amount", "395.31"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/25/2001"},
                    {"subject", "Logan"},
                    {"amount", "348.12"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "07/21/2014"},
                    {"subject", "Emma"},
                    {"amount", "-202.71"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/31/2004"},
                    {"subject", "Emma"},
                    {"amount", "-48.36"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "02/17/2005"},
                    {"subject", "Mia"},
                    {"amount", "-115.15"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "04/17/2006"},
                    {"subject", "Ethan"},
                    {"amount", "-104.69"}
                }
            };
            // Act
            var averageIncome = Program.CalculateAverageIncome(bankData);
            // Assert
            Assert.AreEqual(259.98, averageIncome, 0.01);
        }

        [Test]
        public void CalculateAverageExpense_ValidBankData_SuccessCalculated()
        {
            // Arrange
            var bankData = new List<Dictionary<string, dynamic>>()
            {
                new Dictionary<string, dynamic>()
                {
                    {"date", "01/04/2005"},
                    {"subject", "Logan"},
                    {"amount", "-107.75"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "12/18/2007"},
                    {"subject", "Ethan"},
                    {"amount", "-458.88"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "12/05/2002"},
                    {"subject", "Mia"},
                    {"amount", "-245.08"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "02/21/2019"},
                    {"subject", "James"},
                    {"amount", "36.51"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/05/2014"},
                    {"subject", "Noah"},
                    {"amount", "395.31"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/25/2001"},
                    {"subject", "Logan"},
                    {"amount", "348.12"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "07/21/2014"},
                    {"subject", "Emma"},
                    {"amount", "-202.71"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/31/2004"},
                    {"subject", "Emma"},
                    {"amount", "-48.36"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "02/17/2005"},
                    {"subject", "Mia"},
                    {"amount", "-115.15"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "04/17/2006"},
                    {"subject", "Ethan"},
                    {"amount", "-104.69"}
                }
            };
            // Act
            var averageExpense = Program.CalculateAverageExpense(bankData);
            // Assert
            Assert.AreEqual(-183.23, averageExpense, 0.01);
        }

        [Test]
        public void CalculateMaximumIncome_ValidBankData_SuccessCalculated()
        {
            // Arrange
            var bankData = new List<Dictionary<string, dynamic>>()
            {
                new Dictionary<string, dynamic>()
                {
                    {"date", "01/04/2005"},
                    {"subject", "Logan"},
                    {"amount", "-107.75"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "12/18/2007"},
                    {"subject", "Ethan"},
                    {"amount", "-458.88"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "12/05/2002"},
                    {"subject", "Mia"},
                    {"amount", "-245.08"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "02/21/2019"},
                    {"subject", "James"},
                    {"amount", "36.51"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/05/2014"},
                    {"subject", "Noah"},
                    {"amount", "395.31"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/25/2001"},
                    {"subject", "Logan"},
                    {"amount", "348.12"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "07/21/2014"},
                    {"subject", "Emma"},
                    {"amount", "-202.71"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/31/2004"},
                    {"subject", "Emma"},
                    {"amount", "-48.36"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "02/17/2005"},
                    {"subject", "Mia"},
                    {"amount", "-115.15"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "04/17/2006"},
                    {"subject", "Ethan"},
                    {"amount", "-104.69"}
                }
            };
            // Act
            var maximumExpense = Program.CalculateMaximumExpense(bankData);
            // Assert
            Assert.AreEqual(-458.88, maximumExpense);
        }

        [Test]
        public void CalculateMaximumExpense_ValidBankData_SuccessCalculated()
        {
            // Arrange
            var bankData = new List<Dictionary<string, dynamic>>()
            {
                new Dictionary<string, dynamic>()
                {
                    {"date", "01/04/2005"},
                    {"subject", "Logan"},
                    {"amount", "-107.75"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "12/18/2007"},
                    {"subject", "Ethan"},
                    {"amount", "-458.88"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "12/05/2002"},
                    {"subject", "Mia"},
                    {"amount", "-245.08"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "02/21/2019"},
                    {"subject", "James"},
                    {"amount", "36.51"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/05/2014"},
                    {"subject", "Noah"},
                    {"amount", "395.31"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/25/2001"},
                    {"subject", "Logan"},
                    {"amount", "348.12"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "07/21/2014"},
                    {"subject", "Emma"},
                    {"amount", "-202.71"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/31/2004"},
                    {"subject", "Emma"},
                    {"amount", "-48.36"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "02/17/2005"},
                    {"subject", "Mia"},
                    {"amount", "-115.15"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "04/17/2006"},
                    {"subject", "Ethan"},
                    {"amount", "-104.69"}
                }
            };
            // Act
            var maximumIncome = Program.CalculateMaximumIncome(bankData);
            // Assert
            Assert.AreEqual(395.31, maximumIncome);
        }

        [Test]
        public void CalculateNameWhoMaxSend_ValidBankData_SuccessCalculated()
        {
            // Arrange
            var bankData = new List<Dictionary<string, dynamic>>()
            {
                new Dictionary<string, dynamic>()
                {
                    {"date", "01/04/2005"},
                    {"subject", "Logan"},
                    {"amount", "-107.75"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "12/18/2007"},
                    {"subject", "Ethan"},
                    {"amount", "-458.88"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "12/05/2002"},
                    {"subject", "Mia"},
                    {"amount", "-245.08"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "02/21/2019"},
                    {"subject", "James"},
                    {"amount", "36.51"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/05/2014"},
                    {"subject", "Noah"},
                    {"amount", "395.31"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/25/2001"},
                    {"subject", "Logan"},
                    {"amount", "348.12"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "07/21/2014"},
                    {"subject", "Emma"},
                    {"amount", "-202.71"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/31/2004"},
                    {"subject", "Emma"},
                    {"amount", "-48.36"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "02/17/2005"},
                    {"subject", "Mia"},
                    {"amount", "-115.15"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "04/17/2006"},
                    {"subject", "Ethan"},
                    {"amount", "-104.69"}
                }
            };
            // Act
            var nameWhoMaxSend = Program.CalculateNameWhoMaxSend(bankData);
            // Assert
            Assert.AreEqual("Noah", nameWhoMaxSend);
        }

        [Test]
        public void CalculateNameWhoMaxGet_ValidBankData_SuccessCalculated()
        {
            // Arrange
            var bankData = new List<Dictionary<string, dynamic>>()
            {
                new Dictionary<string, dynamic>()
                {
                    {"date", "01/04/2005"},
                    {"subject", "Logan"},
                    {"amount", "-107.75"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "12/18/2007"},
                    {"subject", "Ethan"},
                    {"amount", "-458.88"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "12/05/2002"},
                    {"subject", "Mia"},
                    {"amount", "-245.08"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "02/21/2019"},
                    {"subject", "James"},
                    {"amount", "36.51"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/05/2014"},
                    {"subject", "Noah"},
                    {"amount", "395.31"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/25/2001"},
                    {"subject", "Logan"},
                    {"amount", "348.12"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "07/21/2014"},
                    {"subject", "Emma"},
                    {"amount", "-202.71"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "08/31/2004"},
                    {"subject", "Emma"},
                    {"amount", "-48.36"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "02/17/2005"},
                    {"subject", "Mia"},
                    {"amount", "-115.15"}
                },
                new Dictionary<string, dynamic>()
                {
                    {"date", "04/17/2006"},
                    {"subject", "Ethan"},
                    {"amount", "-104.69"}
                }
            };
            // Act
            var nameWhoMaxGet = Program.CalculateNameWhoMaxGet(bankData);
            // Assert
            Assert.AreEqual("Ethan", nameWhoMaxGet);
        }
    }
}