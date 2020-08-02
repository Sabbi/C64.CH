using System;

namespace C64.Data.Entities
{
    public class UserFavorite
    {
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public int ProductionId { get; set; }
        public virtual Production Production { get; set; }

        public DateTime Added { get; set; } = DateTime.MinValue;
    }
}