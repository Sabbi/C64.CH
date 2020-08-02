namespace C64.Services
{
    public class MailAddress
    {
        public string Address { get; set; }
        public string Name { get; set; }

        public MailAddress(string address, string name)
        {
            Address = address;
            Name = name;
        }

        public MailAddress(string address)
        {
            Address = address;
        }
    }
}