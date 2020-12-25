namespace C64.Data.Entities
{
    public class ProductionsSceners
    {
        public int ProductionId { get; set; }

        public virtual Production Production { get; set; }

        public int ScenerId { get; set; }

        public virtual Scener Scener { get; set; }
    }
}