namespace C64.Data.Entities
{
    public class ScenerGroupJob
    {
        public int ScenerGroupJobId { get; set; }
        public int ScenerGroupId { get; set; }
        public Job Job { get; set; }
    }
}