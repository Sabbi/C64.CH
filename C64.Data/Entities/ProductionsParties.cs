using System;
using System.ComponentModel.DataAnnotations;

namespace C64.Data.Entities
{
    public class ProductionsParties
    {
        public int ProductionId { get; set; }
        public virtual Production Production { get; set; }

        public int PartyId { get; set; }
        public virtual Party Party { get; set; }

        public int Rank { get; set; }

        [MaxLength(255)]
        public string Category { get; set; }

        public string GetAchievementText
        {
            get
            {
                if (!HasRank() && !HasCategory())
                    return null;

                if (HasRank() && !HasCategory())
                    return $"Ranked {RankWithEnding}";

                if (!HasRank() && HasCategory())
                    return $"Released at the {Category}-Competition";

                return ($"Ranked {RankWithEnding} at the {Category}-Competition");
            }
        }

        //public override bool Equals(object obj)
        //{
        //    if ((obj == null) || !GetType().Equals(obj.GetType()))
        //        return false;

        //    var compare = (ProductionsParties)obj;

        //    if (compare.Category != Category || compare.PartyId != PartyId || compare.Rank != Rank || compare.ProductionId != ProductionId)
        //        return false;

        //    return true;
        //}

        private bool HasRank()
        {
            return Rank > 0 && Rank != 99;
        }

        private bool HasCategory()
        {
            return !string.IsNullOrEmpty(Category);
        }

        private string RankWithEnding
        {
            get
            {
                if (!HasRank())
                    throw new ArgumentException("Rank");

                switch (Rank)
                {
                    case 1:
                    case 11:
                    case 21:
                    case 31:
                        return $"{Rank}st";

                    case 2:
                    case 22:
                        return $"{Rank}nd";

                    case 3:
                    case 23:
                        return $"{Rank}rd";
                }
                return $"{Rank}th";
            }
        }
    }
}