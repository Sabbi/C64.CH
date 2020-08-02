namespace C64.Data.Entities
{
    public class PartiesSceners
    {
        public int PartyId { get; set; }
        public virtual Party Party { get; set; }

        public int ScenerId { get; set; }
        public virtual Scener Scener { get; set; }
    }
}