using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Converters;

public class BuildingsConfiguration
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    [BsonRepresentation(BsonType.String)]
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