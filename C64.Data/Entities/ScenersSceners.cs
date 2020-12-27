namespace C64.Data.Entities
{
    public class ScenersSceners
    {
        public int ScenerId { get; set; }
        public virtual Scener Scener { get; set; }

        public int ScenerToId { get; set; }
        public virtual Scener ScenerTo { get; set; }
    }
}