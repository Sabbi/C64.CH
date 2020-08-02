namespace C64.Data.Entities
{
    public class PartiesGroups
    {
        public int PartyId { get; set; }
        public virtual Party Party { get; set; }

        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}