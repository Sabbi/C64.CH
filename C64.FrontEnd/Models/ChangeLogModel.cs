using System;
using System.Collections.Generic;

namespace C64.FrontEnd.Models
{
    public class ChangeLogModel
    {
        public DateTime Date { get; set; }
        public IEnumerable<string> Changes { get; set; }

        public ChangeLogModel()
        {
        }

        public ChangeLogModel(DateTime date, params string[] changes)
        {
            Date = date;
            Changes = changes;
        }
    }
}