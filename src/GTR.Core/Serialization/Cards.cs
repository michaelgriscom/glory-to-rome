#region

using GTR.Core.Game;
using Newtonsoft.Json;

#endregion

namespace GTR.Core.Serialization
{
    public class CardSerialization
    {
        [JsonProperty("id")] public int Id;
    }

    public class MaterialCardSerialization : CardSerialization
    {
        [JsonProperty("material")] public MaterialType Material;
    }

    public class OrderCardSerialization : MaterialCardSerialization
    {
        [JsonProperty("building_name")] public string BuildingName;
    }

    public class BuildingFoundationSerialization : MaterialCardSerialization
    {
        [JsonProperty("is_within_rome")] public bool IsWithinRome;
    }

    public class JackCardSerialization : CardSerialization
    {
    }
}