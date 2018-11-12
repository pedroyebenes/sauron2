using System;
using System.Json;
namespace Sauron2.Core
{
    public class SimulationParameters
    {
        public ulong TimeLimit { private set; get; }

        public SimulationParameters(string jsonString)
        {
            JsonObject jo = (JsonObject)JsonValue.Parse(jsonString);

            TimeLimit = jo.TryGetValue("time_limit", out JsonValue value) ? (ulong)value : ulong.MaxValue;
        }
    }
}
