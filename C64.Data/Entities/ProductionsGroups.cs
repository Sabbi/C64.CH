namespace C64.Data.Entities
{
    public class ProductionsGroups
    {
        public int ProductionId { get; set; }
        public virtual Production Production { get; set; }

        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}