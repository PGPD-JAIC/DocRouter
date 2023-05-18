using DocRouter.Common;
using System;

namespace DocRouter.Application.UnitTests.Common
{
    public class DateTimeTestProvider : IDateTime
    {
        public DateTimeTestProvider()
        {
            _date = new DateTime(2023, 6, 1);
        }
        public DateTimeTestProvider(DateTime date)
        {
            _date = date;
        }
        private readonly DateTime _date;
        public DateTime Now { get { return _date; } }
    }
}
