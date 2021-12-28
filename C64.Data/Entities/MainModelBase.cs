namespace C64.Data.Entities
{
    // Base Model for all Main Models (yes, that is why it is named that way.. :) )
    // Main Models -> Production, Group, Scener, Party
    public class MainModelBase
    {
        // Pingback-IDs
        public int CsdbId { get; set; }

        public int DemoZooId { get; set; }

        public int LemonId { get; set; }

        public int PouetId { get; set; }

        public bool Deleted { get; set; }
    }
}