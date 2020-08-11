namespace C64.Data.Entities
{
    public class EditCredit
    {
        public int Id { get; set; }
        public Credit Credit { get; set; }
        public int ScenerId { get; set; }
        public string ScenerHandle { get; set; }
        public bool Added { get; set; }
        public bool Deleted { get; set; }
    }
}