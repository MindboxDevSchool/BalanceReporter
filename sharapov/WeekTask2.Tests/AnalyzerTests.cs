using System;
using System.Collections.Generic;
using System.Data;
using NUnit.Framework;
using Week1Task2;

//??? probably [double] bad idea for representation business logic ???
//probably each test should have own micro [transactions] arrange with obvious logic for compare expected and actual not a general List<Transaction> 
namespace WeekTask2.Tests {
    public class AnalyzerTests {
        private Analyzer _analyzer;

        // delta for wo sign after point precision precision  
        private const double Delta = 0.001;

        [SetUp]
        public void GlobalSetup() {
            //create list of transactions
              // @formatter:off
            var transactions = new List<Transaction> {
                new Transaction {Date = new DateTime(2019, 11, 10), VendorName = "ALL WE NEED",  Value = 802.77d,    TransactionType = Transaction.Type.Outcome},
                new Transaction {Date = new DateTime(2019, 01, 04), VendorName = "АСНА",         Value = 2587.18d,   TransactionType = Transaction.Type.Outcome},
                new Transaction {Date = new DateTime(2019, 02, 21), VendorName = "ГИПЕР АВТО",   Value = 4592.19d,   TransactionType = Transaction.Type.Outcome},
                new Transaction {Date = new DateTime(2019, 02, 23), VendorName = "ОЛАНТ",        Value = 3986.9d,    TransactionType = Transaction.Type.Outcome},
                new Transaction {Date = new DateTime(2019, 02, 25), VendorName = "VTB",          Value = +24096.85d, TransactionType = Transaction.Type.Income},
                new Transaction {Date = new DateTime(2020, 01, 03), VendorName = "MODIS",        Value = 1236.03d,   TransactionType = Transaction.Type.Outcome},
                new Transaction {Date = new DateTime(2020, 01, 18), VendorName = "Будь здоров!", Value = 3482.93d,   TransactionType = Transaction.Type.Outcome},
                new Transaction {Date = new DateTime(2020, 01, 21), VendorName = "Pizza Hat",    Value = 1913.84d,   TransactionType = Transaction.Type.Outcome},
                new Transaction {Date = new DateTime(2020, 01, 24), VendorName = "Будь здоров!", Value = 2140.34d,   TransactionType = Transaction.Type.Outcome},
                new Transaction {Date = new DateTime(2020, 01, 29), VendorName = "Связной ",     Value = 4575.69d,   TransactionType = Transaction.Type.Outcome},
                new Transaction {Date = new DateTime(2020, 01, 31), VendorName = "Alpha  Bank",  Value = +10000d,    TransactionType = Transaction.Type.Income},
                new Transaction {Date = new DateTime(2020, 01, 31), VendorName = "VTB",          Value = 78555.66d,  TransactionType = Transaction.Type.Outcome},
                new Transaction {Date = new DateTime(2020, 02, 15), VendorName = "Связной ",     Value = 4575.69d,   TransactionType = Transaction.Type.Outcome},
                new Transaction {Date = new DateTime(2020, 01, 31), VendorName = "Alpha Bank ",  Value = +10000.00d, TransactionType = Transaction.Type.Income},
                new Transaction {Date = new DateTime(2020, 01, 31), VendorName = "VTB",          Value = +78555.66d, TransactionType = Transaction.Type.Income},
            };
            // @formatter:on 
            //crete analyzes
            _analyzer = new Analyzer(transactions);
        }

        [Test]
        public void FirstAndLastDayCheckBankInfo() {
            //arrange
            var expectedFirstDate = new DateTime(2019, 1, 4);
            var expectedLastDate = new DateTime(2020, 02, 15);

            //act
            var firstDate = _analyzer.FirstDate();
            var lastDate = _analyzer.LastDate();

            //assert
            Assert.AreEqual(expectedFirstDate, firstDate);
            Assert.AreEqual(expectedLastDate, lastDate);
        }

        [Test]
        //if (firstDate == lastDate) => no throw exception 
        //which better for testing exceptions 
        //1) TestDelegate on act. - TestDelegate  IncomeCashFlowAction()
        //2)local function on act - void IncomeCashFlowAction() => _analyzer.IncomeCashFlow(firstDate, lastDate);
        //3) combine act & assert -  Assert.Throws<EvaluateException>(_analyzer.IncomeCashFlow(firstDate, lastDate));
        public void FirstDateParamLaterThenSecondDateParamBankInfo() {
            //arrange
            var firstDate = new DateTime(2000, 1, 1);
            var lastDate = new DateTime(1999, 1, 1);

            //act  
            void IncomeCashFlowAction() => _analyzer.IncomeCashFlow(firstDate, lastDate);
            void OutcomeCashFlowAction() => _analyzer.OutcomeCashFlow(firstDate, lastDate);
            void AverageIncomeAction() => _analyzer.AverageIncome(firstDate, lastDate);
            void AverageOutcomeAction() => _analyzer.AverageOutcome(firstDate, lastDate);
            void MaxIncomeAction() => _analyzer.MaxIncome(firstDate, lastDate);
            void MaxOutcomeAction() => _analyzer.MaxOutcome(firstDate, lastDate);
            void MaxIncomeByVendorAction() => _analyzer.MaxIncomeByVendor(firstDate, lastDate);
            void MaxOutcomeByVendorAction() => _analyzer.MaxOutcomeByVendor(firstDate, lastDate);

            //assert
            Assert.Throws<EvaluateException>(IncomeCashFlowAction);
            Assert.Throws<EvaluateException>(OutcomeCashFlowAction);
            Assert.Throws<EvaluateException>(AverageIncomeAction);
            Assert.Throws<EvaluateException>(AverageOutcomeAction);
            Assert.Throws<EvaluateException>(MaxIncomeAction);
            Assert.Throws<EvaluateException>(MaxOutcomeAction);
            Assert.Throws<EvaluateException>(MaxIncomeByVendorAction);
            Assert.Throws<EvaluateException>(MaxOutcomeByVendorAction);
        }

        [Test]
        //if (firstDate == lastDate) => no throw exception 
        public void FirstDateParamAndSecondDateAreSameDateBankInfo() {
            //arrange
            var firstDate = new DateTime(2000, 1, 1);
            var lastDate = new DateTime(2000, 1, 1);

            //act

            //assert
            Assert.DoesNotThrow(code: () => _analyzer.IncomeCashFlow(firstDate, lastDate));
            Assert.DoesNotThrow(() => _analyzer.OutcomeCashFlow(firstDate, lastDate));
            Assert.DoesNotThrow(() => _analyzer.AverageIncome(firstDate, lastDate));
            Assert.DoesNotThrow(() => _analyzer.AverageOutcome(firstDate, lastDate));
            Assert.DoesNotThrow(() => _analyzer.MaxIncome(firstDate, lastDate));
            Assert.DoesNotThrow(() => _analyzer.MaxOutcome(firstDate, lastDate));
            Assert.DoesNotThrow(() => _analyzer.MaxIncomeByVendor(firstDate, lastDate));
            Assert.DoesNotThrow(() => _analyzer.MaxOutcomeByVendor(firstDate, lastDate));
        }

        [Test]
        public void CashFlowAllTimeBankInfo() {
            //arrange
            const double expectedIncomeCashFlow = 122652.51d;
            const double expectedOutcomeCashFlow = 108449.22d;
            var firstDate = _analyzer.FirstDate();
            var lastDate = _analyzer.LastDate();

            //act
            var (incomeCashFlow, outcomeCashFlow) = CashFlow(firstDate, lastDate);

            //arrange
            Assert.AreEqual(expectedIncomeCashFlow, incomeCashFlow, Delta);
            Assert.AreEqual(expectedOutcomeCashFlow, outcomeCashFlow, Delta);
        }

        [Test]
        public void CashFlowByYearBankInfo() {
            //arrange
            const double expectedIncomeCashFlow2019 = 24096.85d;
            const double expectedOutcomeCashFlow2019 = 11969.04d;
            const double expectedIncomeCashFlow2020 = 98555.66d;
            const double expectedOutcomeCashFlow2020 = 96480.18d;

            //act
            var (incomeCashFlow2019, outcomeCashFlow2019) = CashFlow(2019);
            var (incomeCashFlow2020, outcomeCashFlow2020) = CashFlow(2020);

            //assert
            Assert.AreEqual(expectedIncomeCashFlow2019, incomeCashFlow2019, Delta);
            Assert.AreEqual(expectedOutcomeCashFlow2019, outcomeCashFlow2019, Delta);
            Assert.AreEqual(expectedIncomeCashFlow2020, incomeCashFlow2020, Delta);
            Assert.AreEqual(expectedOutcomeCashFlow2020, outcomeCashFlow2020, Delta);
        }

        [Test]
        public void CashFlowByOneMonthBankInfo() {
            //arrange
            const double expectedIncomeCashFlow = 98555.66d;
            const double expectedOutcomeCashFlow = 91904.49d;
            var firstDateOfMonth = new DateTime(2020, 1, 1);
            var lastDateOfMonth = new DateTime(2020, 1, 31);

            //act
            var (incomeCashFlow, outcomeCashFlow) = CashFlow(firstDateOfMonth, lastDateOfMonth);

            //assert
            Assert.AreEqual(expectedIncomeCashFlow, incomeCashFlow, Delta);
            Assert.AreEqual(expectedOutcomeCashFlow, outcomeCashFlow, Delta);
        }

        [Test]
        public void AverageBankInfo() {
            //arrange
            const double expectedAverageIncome = double.NaN;
            const double expectedOAverageOutcome = 802.77d;

            //act 
            var averageIncome = _analyzer.AverageIncome(new DateTime(2019, 6, 1), new DateTime(2019, 12, 31));
            var averageOutcome = _analyzer.AverageOutcome(new DateTime(2019, 6, 1), new DateTime(2019, 12, 31));

            //assert
            Assert.AreEqual(Double.IsNaN(expectedAverageIncome), Double.IsNaN(averageIncome));
            Assert.AreEqual(expectedOAverageOutcome, averageOutcome, Delta);
        }

        [Test]
        public void MaxBankInfo() {
            //arrange
            var expectedMaxIncomeTransactions = new List<Transaction> {
                new Transaction {
                    Date = new DateTime(2020, 1, 31),
                    VendorName = "VTB",
                    Value = 78555.660000000003d,
                    TransactionType = Transaction.Type.Income
                }
            };

            var expectedMaxOutcomeTransactions = new List<Transaction> {
                new Transaction {
                    Date = new DateTime(2020, 1, 31),
                    VendorName = "VTB",
                    Value = 78555.660000000003d, //TODO how indicate Delta for Assert.That ???
                    TransactionType = Transaction.Type.Outcome
                },
                new Transaction {
                    Date = new DateTime(2020, 1, 29),
                    VendorName = "Связной ",
                    Value = 4575.6899999999996d,
                    TransactionType = Transaction.Type.Outcome
                }
            };

            //act 
            var maxIncomeTransactions =
                _analyzer.MaxIncome(
                    new DateTime(2019, 6, 1),
                    new DateTime(2020, 12, 31));
            var maxOutcomeTransactions =
                _analyzer.MaxOutcome(
                    new DateTime(2019, 6, 1),
                    new DateTime(2020, 12, 31),
                    2);

            //assert
            Assert.That(expectedMaxIncomeTransactions, Is.EquivalentTo(maxIncomeTransactions));
            Assert.That(expectedMaxOutcomeTransactions, Is.EquivalentTo(maxOutcomeTransactions));
        }

        [Test]
        public void MaxByVendorBankInfo() {
            //arrange
            var expectedMaxByVendorIncomeTransactions = new List<VendorInfoResult> {
                new VendorInfoResult {
                    VendorName = "VTB",
                    TotalSum = 102652.51000000001d
                },
                new VendorInfoResult {
                    VendorName = "Alpha  Bank",
                    TotalSum = 10000d
                }
            };

            var expectedMaxByVendorOutcomeTransactions = new List<VendorInfoResult> {
                new VendorInfoResult {
                    VendorName = "VTB",
                    TotalSum = 78555.660000000003d
                },
                new VendorInfoResult {
                    VendorName = "Связной ",
                    TotalSum = 9151.3799999999992d
                },
                new VendorInfoResult {
                    VendorName = "Будь здоров!",
                    TotalSum = 5623.2700000000004d
                },
                new VendorInfoResult {
                    VendorName = "ГИПЕР АВТО",
                    TotalSum = 4592.1899999999996d
                },
                new VendorInfoResult {
                    VendorName = "ОЛАНТ",
                    TotalSum = 3986.9000000000001d
                }
            };

            //act 
            var maxIncomeByVendorTransactions =
                _analyzer.MaxIncomeByVendor(
                    new DateTime(2018, 1, 1),
                    new DateTime(2020, 12, 31),
                    2);
            var maxOutcomeByVendorTransactions =
                _analyzer.MaxOutcomeByVendor(
                    new DateTime(2018, 1, 1),
                    new DateTime(2020, 12, 31),
                    5);

            //assert
            Assert.That(expectedMaxByVendorIncomeTransactions, Is.EquivalentTo(maxIncomeByVendorTransactions));
            Assert.That(expectedMaxByVendorOutcomeTransactions, Is.EquivalentTo(maxOutcomeByVendorTransactions));
        }


        [Test]
        public void EmptyResults() {
            //arrange
            var firstDate = _analyzer.FirstDate().AddYears(-1000);
            var lastDate = _analyzer.LastDate().AddYears(-1000);

            const double expectedCashFlowIncome = 0;
            const double expectedOCashFlowOutcome = 0;
            const double expectedAverageIncome = double.NaN;
            const double expectedOAverageOutcome = double.NaN;
            var expectedMaxIncome = new List<Transaction>();
            var expectedMaxOutcome = new List<Transaction>();
            var expectedMaxIncomeByVendor = new List<VendorInfoResult>();
            var expectedMaxOutcomeByVendor = new List<VendorInfoResult>();

            //act 
            var cashFlowIncome = _analyzer.IncomeCashFlow(firstDate, lastDate);
            var cashFlowOut = _analyzer.OutcomeCashFlow(firstDate, lastDate);
            var averageIncome = _analyzer.AverageIncome(firstDate, lastDate);
            var averageOutcome = _analyzer.AverageOutcome(firstDate, lastDate);
            var maxIncome = _analyzer.MaxIncome(firstDate, lastDate);
            var maxOutcome = _analyzer.MaxIncome(firstDate, lastDate);
            var maxIncomeByVendor = _analyzer.MaxIncomeByVendor(firstDate, lastDate);
            var maxOutcomeByVendor = _analyzer.MaxOutcomeByVendor(firstDate, lastDate);

            //assert
            Assert.AreEqual(expectedCashFlowIncome, cashFlowIncome);
            Assert.AreEqual(expectedOCashFlowOutcome, cashFlowOut);
            Assert.AreEqual(double.IsNaN(expectedAverageIncome), double.IsNaN(averageIncome));
            Assert.AreEqual(double.IsNaN(expectedOAverageOutcome), double.IsNaN(averageOutcome));
            Assert.That(expectedMaxIncome, Is.EquivalentTo(maxIncome));
            Assert.That(expectedMaxOutcome, Is.EquivalentTo(maxOutcome));
            Assert.That(expectedMaxIncomeByVendor, Is.EquivalentTo(maxIncomeByVendor));
            Assert.That(expectedMaxOutcomeByVendor, Is.EquivalentTo(maxOutcomeByVendor));
        }


        private (double, double) CashFlow(int year) {
            var cashFlow = CashFlow(new DateTime(year, 1, 1), new DateTime(year, 12, 31));
            return cashFlow;
        }

        private (double, double) CashFlow(DateTime firstDate, DateTime lastDate) {
            var incomeCashFlow = _analyzer.IncomeCashFlow(firstDate, lastDate);
            var outcomeCashFlow = _analyzer.OutcomeCashFlow(firstDate, lastDate);
            return (incomeCashFlow, outcomeCashFlow);
        }
    }
}