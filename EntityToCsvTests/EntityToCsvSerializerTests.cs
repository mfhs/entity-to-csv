using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntityToCsv;

namespace EntityToCsvTests
{
    [TestClass]
    public class EntityToCsvSerializerTests
    {
        [TestMethod]
        public void EntityToCsvTest()
        {
            var actualCsvRecords  = EntityToCsvSerializer.SerializeToDelimitedText(GetTestEntityCollection(), ',');
            var expectedCsvRecords = GetExpectedCsvRecordsForNonEmptyEntity();
            Assert.AreEqual(actualCsvRecords, expectedCsvRecords);
        }

        [TestMethod]
        public void EmptyEntityToCsvTest()
        {
            var actualCsvRecords = EntityToCsvSerializer.SerializeToDelimitedText(new List<TestPersonEntity>(), ',');
            var expectedEmptyCsvRecord = GetExpectedCsvRecordForEmptyEntity();
            Assert.AreEqual(actualCsvRecords, expectedEmptyCsvRecord);
        }

        private static IEnumerable<TestPersonEntity> GetTestEntityCollection()
        {
            return
                new List<TestPersonEntity>
                {
                    new TestPersonEntity
                    {
                        FirstName = "Md Faroque",
                        LastName = "Hossain",
                        Designation = "Senior Software Engineer",
                        DescriptionInEnglish = "I love 'programming', 'logic', solving 'problems'!",
                        DescriptionInSwedish = "Jag älskar \"programmering\", \"logik\", lösa \"problem\"!",
                        DecimalTestField = 2456.13,
                        IntegerTestField = 9988
                    },
                    new TestPersonEntity
                    {
                        FirstName = "Shuvashish",
                        LastName = "Das",
                        Designation = "Lead Software Engineer",
                        DescriptionInEnglish = "I love logic, solving programming challenges!",
                        DescriptionInSwedish = "Jag älskar logik, lösning av programutmaningar!",
                        DecimalTestField = 5687.32,
                        IntegerTestField = 1122
                    }
                };
        }

        private static string GetExpectedCsvRecordsForNonEmptyEntity(char delimiter = ',')
        {
            return
                $"FirstName{delimiter}LastName{delimiter}Designation{delimiter}DescriptionInEnglish{delimiter}DescriptionInSwedish{delimiter}DecimalTestField{delimiter}IntegerTestField" +
                $"{Environment.NewLine}" +
                $"Md Faroque{delimiter}" +
                $"Hossain{delimiter}" +
                $"Senior Software Engineer{delimiter}" +
                $"\"I love 'programming', 'logic', solving 'problems'!\"{delimiter}" +
                $"\"Jag älskar \"\"programmering\"\", \"\"logik\"\", lösa \"\"problem\"\"!\"{delimiter}" +
                $"2456.13{delimiter}" +
                $"9988{Environment.NewLine}" +
                $"Shuvashish{delimiter}" +
                $"Das{delimiter}" +
                $"Lead Software Engineer{delimiter}" +
                $"\"I love logic, solving programming challenges!\"{delimiter}" +
                $"\"Jag älskar logik, lösning av programutmaningar!\"{delimiter}" +
                $"5687.32{delimiter}" +
                $"1122{Environment.NewLine}";
        }

        private static string GetExpectedCsvRecordForEmptyEntity(char delimiter = ',')
        {
            return
                $"FirstName{delimiter}LastName{delimiter}Designation{delimiter}DescriptionInEnglish{delimiter}DescriptionInSwedish{delimiter}DecimalTestField{delimiter}IntegerTestField" +
                $"{Environment.NewLine}";
        }
    }

    internal class TestPersonEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public string DescriptionInEnglish { get; set; }
        public string DescriptionInSwedish { get; set; }
        public double DecimalTestField { get; set; }
        public int IntegerTestField { get; set; }
    }
}