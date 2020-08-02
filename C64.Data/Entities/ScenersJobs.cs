namespace C64.Data.Entities
{
    public class ScenersJobs
    {
        public int ScenersJobsId { get; set; }
        public int ScenerId { get; set; }
        public virtual Scener Scener { get; set; }
        public Job Job { get; set; }
    }
}