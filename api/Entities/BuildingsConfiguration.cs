using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
public class BuildingsConfiguration
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    public BuildingType Type { get; set; }
    public decimal BuildingCost { get; set; }
    public int ConstructionTime { get; set; }

}

public enum BuildingType
{
    Farm,
    Academy,
    Headquarters,
    LumberMill,
    Barracks
}