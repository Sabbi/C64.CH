using C64.Data.Entities;
using System.Collections.Generic;

namespace C64.FrontEnd.Models
{
    public class EditProductionCredits
    {
        public List<EditCredit> EditCredits { get; set; } = new List<EditCredit>();
    }
}