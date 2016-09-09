#region

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

#endregion

namespace GTR.Core.Util
{
    public class JsonNetConfiguration
    {
        public static void ConfigureJsonNet()
        {
            JsonConvert.DefaultSettings = (() =>
            {
                var settings = new JsonSerializerSettings();
                // convert all enums to strings and vice-versa
                settings.Converters.Add(new StringEnumConverter {CamelCaseText = true});
                // convert PascalCase properties to camelCase
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                return settings;
            });
        }
    }
}