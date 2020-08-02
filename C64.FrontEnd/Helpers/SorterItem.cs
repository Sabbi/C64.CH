namespace C64.FrontEnd.Helpers
{
    public class SorterItem
    {
        public string Key { get; set; }
        public string Display { get; set; }
        public bool DefaultSortAscending { get; set; }

        public SorterItem(string key, string display, bool defaultSortAscending)
        {
            Key = key;
            Display = display;
            DefaultSortAscending = defaultSortAscending;
        }
    }
}