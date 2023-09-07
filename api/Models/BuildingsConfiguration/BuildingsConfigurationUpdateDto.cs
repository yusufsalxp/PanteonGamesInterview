using System.ComponentModel.DataAnnotations;

public class BuildingsConfigurationUpdateDto
{
    public BuildingType? Type { get; set; }

    [Range(0.1, double.PositiveInfinity, ErrorMessage = "The field {0} must be greater than {1}.")]
    public decimal? BuildingCost { get; set; }

    [Range(30, 1800, ErrorMessage = "The field {0} must be greater than {1} and less than {2}.")]
    public int? ConstructionTime { get; set; }
}