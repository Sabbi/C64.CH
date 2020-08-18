using System;

namespace C64.Data.Entities
{
    public class ProductionsParties
    {
        public int ProductionId { get; set; }
        public virtual Production Production { get; set; }

        public int PartyId { get; set; }
        public virtual Party Party { get; set; }

        public int Rank { get; set; }

        public int? PartyCategoryId { get; set; }
        public virtual PartyCategory PartyCategory { get; set; }

        public string GetAchievementText
        {
            get
            {
                if (!HasRank() && !HasCategory())
                    return null;

                if (HasRank() && !HasCategory())
                    return $"Ranked {RankWithEnding}";

                if (!HasRank() && HasCategory())
                    return $"Released at the {PartyCategory.Name}-Competition";

                return ($"Ranked {RankWithEnding} at the {PartyCategory.Name}-Competition");
            }
        }

        private bool HasRank()
        {
            return Rank > 0 && Rank != 99;
        }

        private bool HasCategory()
        {
            return PartyCategoryId > 0;
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