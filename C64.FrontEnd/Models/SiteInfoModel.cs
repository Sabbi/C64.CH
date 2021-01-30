using System.ComponentModel.DataAnnotations;

public class SiteInfoModel
{
    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    public string Text { get; set; }

    public bool Show { get; set; }
    public int CategoryId { get; set; }

    public string Writer { get; set; }

    public string User { get; set; }
}