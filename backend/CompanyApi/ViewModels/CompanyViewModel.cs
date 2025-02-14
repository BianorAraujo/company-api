using System.ComponentModel.DataAnnotations;

namespace CompanyApi.ViewModels;

public class CompanyViewModel
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "The {0} field is required")]
    [StringLength(50, ErrorMessage = "The {0} field must be between {2} and {1} characters long.", MinimumLength = 1)]
    public string Name { get; set; }

    [Required(ErrorMessage = "The {0} field is required")]
    [StringLength(50, ErrorMessage = "The {0} field must be between {2} and {1} characters long.", MinimumLength = 1)]
    public string Exchange { get; set; }

    [Required(ErrorMessage = "The {0} field is required")]
    [StringLength(5, ErrorMessage = "The {0} field must be between {2} and {1} characters long.", MinimumLength = 4)]
    public string Ticker { get; set; }

    [Required(ErrorMessage = "The {0} field is required")]
    [StringLength(12, ErrorMessage = "The {0} field must be {1} characters long.", MinimumLength = 12)]
    [RegularExpression("^[a-zA-Z]{2}.*", ErrorMessage = "The first two characters of the ISIN must be letters.")]
    public string Isin { get; set; }
    
    public string? Website { get; set; }
}