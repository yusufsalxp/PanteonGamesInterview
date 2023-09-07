using System.ComponentModel.DataAnnotations;

public class BuildingsConfigurationCreateDto
{
    [Required]
    public BuildingType Type { get; set; }
    [Required]
    [Range(0.1, double.PositiveInfinity, ErrorMessage = "The field {0} must be greater than {1}.")]
    public decimal BuildingCost { get; set; }
    [Required]
    [Range(30, 1800, ErrorMessage = "The field {0} must be greater than {1} and less than {2}.")]
    public int ConstructionTime { get; set; }
}