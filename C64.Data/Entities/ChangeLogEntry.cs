using System;
using System.ComponentModel.DataAnnotations;

namespace C64.Data.Entities
{
    public class ChangeLogEntry
    {
        public int ChangeLogEntryId { get; set; }

        public DateTime Date { get; set; }

        [MaxLength(65535)]
        public string Change { get; set; }
    }
}