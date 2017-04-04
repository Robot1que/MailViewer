using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Robot1que.MailViewer.Converters;

namespace Robot1que.MailViewer.Tests.Converters
{
    [TestClass]
    public class DateTimeToStringConverter_Tests
    {
        private TimeServiceStub _timeService;

        [TestInitialize]
        public void BeforeEach()
        {
            this._timeService = new TimeServiceStub();
            TimeService.Set(this._timeService);
        }

        [TestCleanup]
        public void AfterEach()
        {
            TimeService.Reset();
        }

        public DateTimeToStringConverter DateTimeToStringConverterCreate()
        {
            return new DateTimeToStringConverter();
        }

        [TestMethod]
        public void Convertion_returns_formatted_string()
        {
            var testCases =
                new []
                {
                    new {
                        Now = "2000-01-01 10:00:00",
                        Input = "2000-01-01 09:59:59",
                        ExpectedValue = "a moment ago"
                    },
                    new {
                        Now = "2000-01-01 10:00:00",
                        Input = "2000-01-01 09:59:59",
                        ExpectedValue = "a moment ago"
                    }
                };

            foreach (var testCase in testCases)
            {

                var inputTime = DateTime.Parse(testCase.Input);
                this._timeService.Now = DateTime.Parse(testCase.Now);

                var converter = this.DateTimeToStringConverterCreate();
                var actulaValue = converter.Convert(inputTime, null, null, null);

                Assert.AreEqual(testCase.ExpectedValue, actulaValue);
            }
        }
    }
}
