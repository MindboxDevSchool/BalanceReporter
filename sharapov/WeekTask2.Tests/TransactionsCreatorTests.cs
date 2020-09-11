using System;
using System.Collections.Generic;
using NUnit.Framework;
using Week1Task2;

namespace WeekTask2.Tests {
    public class TransactionsCreatorTests {
        [Test]
        public void CreateTransactionsFromCsv() {
            //arrange 
                // @formatter:off
            var expectedTransactions = new List<Transaction> {
                new Transaction{Date = new DateTime(2019,  11, 10), VendorName = "ALL WE NEED",  Value = 802.77,    TransactionType = Transaction.Type.Outcome},
                new Transaction{Date = new DateTime( 2019,  1, 04), VendorName = "АСНА",         Value = 2587.18,   TransactionType = Transaction.Type.Outcome},
                new Transaction{Date = new DateTime( 2019,  2, 21), VendorName = "ГИПЕР АВТО",   Value = 4592.19,   TransactionType = Transaction.Type.Outcome},
                new Transaction{Date = new DateTime( 2019,  2, 23), VendorName = "ОЛАНТ",        Value = 3986.9,    TransactionType = Transaction.Type.Outcome},
                new Transaction{Date = new DateTime( 2019,  2, 25), VendorName = "VTB",          Value = +24096.85, TransactionType = Transaction.Type.Income},
                new Transaction{Date = new DateTime( 2020,  1, 03), VendorName = "MODIS",        Value = 1236.03,   TransactionType = Transaction.Type.Outcome},
                new Transaction{Date = new DateTime( 2020,  1, 18), VendorName = "Будь здоров!", Value = 3482.93,   TransactionType = Transaction.Type.Outcome},
                new Transaction{Date = new DateTime( 2020,  1, 21), VendorName = "Pizza Hat",    Value = 1913.84,   TransactionType = Transaction.Type.Outcome},
                new Transaction{Date = new DateTime( 2020,  1, 24), VendorName = "Будь здоров!", Value = 2140.34,   TransactionType = Transaction.Type.Outcome},
                new Transaction{Date = new DateTime( 2020,  1, 29), VendorName = "Связной ",     Value = 4575.69,   TransactionType = Transaction.Type.Outcome},
                new Transaction{Date = new DateTime( 2020,  1, 31), VendorName = "Alpha Bank",   Value = +10000.00, TransactionType = Transaction.Type.Income},
                new Transaction{Date = new DateTime( 2020,  1, 31), VendorName = "VTB",          Value = +78555.66, TransactionType = Transaction.Type.Income},
                new Transaction{Date = new DateTime( 2020,  2, 15), VendorName = "Связной ",     Value = 4575.69,   TransactionType = Transaction.Type.Outcome}
            };
            // @formatter:on

            //act
            var transactionsFromCsv = TransactionsCreator.CreateFromCsv(@"csv\bankStatementsTask2_v3.csv");

            //assert 
            Assert.That(expectedTransactions, Is.EquivalentTo(transactionsFromCsv));
        }

        [Test]
        public void CreateTransactionsFromString() {
            //arrange 
            var inputString = @"Дата;С кем была транзакция;Сумма транзакции
                                        11/10/2019;ALL WE NEED;802.77
                                        1/4/2019;АСНА;2587.18
                                        2/21/2019;ГИПЕР АВТО;4592.19
                                        2/23/2019;ОЛАНТ;3986.9
                                        2/25/2019;VTB;+24096.85
                                        1/3/2020;MODIS;1236.03
                                        1/18/2020;Будь здоров!;3482.93
                                        1/21/2020;Pizza Hat;1913.84
                                        1/24/2020;Будь здоров!;2140.34
                                        1/29/2020;Связной ;4575.69
                                        1/31/2020;Alpha Bank;+10000.00
                                        1/31/2020;VTB;+78555.66
                                        2/15/2020;Связной ;4575.69";
            var expectedTransactions = new List<Transaction> {
                new Transaction {
                    Date = new DateTime(2019, 11, 10), VendorName = "ALL WE NEED", Value = 802.77,
                    TransactionType = Transaction.Type.Outcome
                },
                new Transaction {
                    Date = new DateTime(2019, 1, 04), VendorName = "АСНА", Value = 2587.18,
                    TransactionType = Transaction.Type.Outcome
                },
                new Transaction {
                    Date = new DateTime(2019, 2, 21), VendorName = "ГИПЕР АВТО", Value = 4592.19,
                    TransactionType = Transaction.Type.Outcome
                },
                new Transaction {
                    Date = new DateTime(2019, 2, 23), VendorName = "ОЛАНТ", Value = 3986.9,
                    TransactionType = Transaction.Type.Outcome
                },
                new Transaction {
                    Date = new DateTime(2019, 2, 25), VendorName = "VTB", Value = +24096.85,
                    TransactionType = Transaction.Type.Income
                },
                new Transaction {
                    Date = new DateTime(2020, 1, 03), VendorName = "MODIS", Value = 1236.03,
                    TransactionType = Transaction.Type.Outcome
                },
                new Transaction {
                    Date = new DateTime(2020, 1, 18), VendorName = "Будь здоров!", Value = 3482.93,
                    TransactionType = Transaction.Type.Outcome
                },
                new Transaction {
                    Date = new DateTime(2020, 1, 21), VendorName = "Pizza Hat", Value = 1913.84,
                    TransactionType = Transaction.Type.Outcome
                },
                new Transaction {
                    Date = new DateTime(2020, 1, 24), VendorName = "Будь здоров!", Value = 2140.34,
                    TransactionType = Transaction.Type.Outcome
                },
                new Transaction {
                    Date = new DateTime(2020, 1, 29), VendorName = "Связной ", Value = 4575.69,
                    TransactionType = Transaction.Type.Outcome
                },
                new Transaction {
                    Date = new DateTime(2020, 1, 31), VendorName = "Alpha Bank", Value = +10000.00,
                    TransactionType = Transaction.Type.Income
                },
                new Transaction {
                    Date = new DateTime(2020, 1, 31), VendorName = "VTB", Value = +78555.66,
                    TransactionType = Transaction.Type.Income
                },
                new Transaction {
                    Date = new DateTime(2020, 2, 15), VendorName = "Связной ", Value = 4575.69,
                    TransactionType = Transaction.Type.Outcome
                }
            };
            //act
            var transactionsFromCsv = TransactionsCreator.CreateFromString(inputString);

            //assert 
            Assert.That(expectedTransactions, Is.EquivalentTo(transactionsFromCsv));
        }

        [Test]
        public void CreateTransactionsFromEmptyString() {
            //arrange 
            const string inputString = @"";
            var expectedTransactions = new List<Transaction>();
            //act
            var transactionsFromCsv = TransactionsCreator.CreateFromString(inputString);

            //assert 
            Assert.That(expectedTransactions, Is.EquivalentTo(transactionsFromCsv));
        }
    }
}