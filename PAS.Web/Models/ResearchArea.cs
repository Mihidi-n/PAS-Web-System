using System.ComponentModel.DataAnnotations;

namespace PAS.Web.Models;

public class ResearchArea
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
}