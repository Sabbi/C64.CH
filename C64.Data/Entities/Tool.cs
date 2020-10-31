using System.ComponentModel.DataAnnotations;

namespace C64.Data.Entities
{
    public class Tool
    {
        public int ToolId { get; set; }
        public ToolCategory ToolCategory { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Facebook { get; set; }

        [MaxLength(255)]
        public string Homepage { get; set; }

        [MaxLength(255)]
        public string Repository { get; set; }

        public int Sort { get; set; }

        public bool Show { get; set; }
    }

    public enum ToolCategory
    {
        Emulator,
        Tool
    }
}