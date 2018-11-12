using System;
using System.Collections.Generic;
using System.IO;
using System.Json;

namespace Sauron2.Core
{
    public class JSONParser
    {
        const string ModuleKey = "Modules";
        const string ModuleName = "Name";
        const string ModuleGates = "Gates";
        const string ModuleType = "Type";

        const string ConnectionKey = "Connections";
        const string ConnectionModuleA = "ModuleA";
        const string ConnectionModuleB = "ModuleB";
        const string ConnectionPortA = "PortA";
        const string ConnectionPortB = "PortB";
        const string ConnectionIndexA = "IndexA";
        const string ConnectionIndexB = "IndexB";

        readonly JsonObject JSONFile;

        public JSONParser(string jsonString)
        {
            JSONFile = (JsonObject)JsonValue.Parse(jsonString);
        }

        public static string ReadJSONFile(string fileName)
        {
            string s = null;
            try
            {
                using (StreamReader r = new StreamReader(fileName))
                {
                    s = r.ReadToEnd();
                }
            }
            catch (IOException ioex)
            {
                Console.WriteLine(ioex.Message); //FIXME
                System.Environment.Exit(1);
            }
            return s;
        }

        public static JsonValue GetValueFromObjectOrDie(JsonObject jo, string key, string errorMsg = "")
        {
            if (!jo.TryGetValue(key, out JsonValue value))
            {
                if (errorMsg == "")
                {
                    errorMsg = "Unable to find the '" + key + "' key";
                }

                throw new Exceptions.JSONnotValidException(errorMsg);
            }
            return value;
        }

        Module CreateModule(string jsonString, IModuleFactory moduleFactory)
        {
            JsonObject jo = (JsonObject)JsonValue.Parse(jsonString);

            string type = (string)GetValueFromObjectOrDie(jo, ModuleType);
            int gates = (int)GetValueFromObjectOrDie(jo, ModuleGates);

            return moduleFactory.CreateModule(type, jsonString);
        }

        public List<Module> GetModules(IModuleFactory moduleFactory)
        {
            List<Module> ml = new List<Module>();

            if (JSONFile.TryGetValue("topology_file", out JsonValue topologyFile))
            {
                JSONParser jSONParser = new JSONParser(JSONParser.ReadJSONFile((string)topologyFile));
                ml.AddRange(jSONParser.GetModules(moduleFactory));
            }
            if (JSONFile.TryGetValue(ModuleKey, out JsonValue jsonModules))
            {
                foreach (JsonValue jm in jsonModules)
                {
                    ml.Add(CreateModule(jm.ToString(), moduleFactory));
                }
            }

            if (ml.Count == 0)
                throw new Exceptions.JSONnotValidException("JSON file does not contain any modules");

            return ml;
        }

        PreConnection CreatePreConnection(string jsonString)
        {
            JsonObject jo = (JsonObject)JsonValue.Parse(jsonString);
            string moduleA = (string)GetValueFromObjectOrDie(jo, ConnectionModuleA);
            string moduleB = (string)GetValueFromObjectOrDie(jo, ConnectionModuleB);
            int portA = (int)GetValueFromObjectOrDie(jo, ConnectionPortA);
            int portB = (int)GetValueFromObjectOrDie(jo, ConnectionPortB);
            int indexA = (int)GetValueFromObjectOrDie(jo, ConnectionIndexA);
            int indexB = (int)GetValueFromObjectOrDie(jo, ConnectionIndexB);

            return new PreConnection(moduleA, indexA, portA, moduleB, indexB, portB);
        }

        public List<PreConnection> GetConnections()
        {
            List<PreConnection> pl = new List<PreConnection>();

            if (JSONFile.TryGetValue("topology_file", out JsonValue topologyFile))
            {
                JSONParser jSONParser = new JSONParser(JSONParser.ReadJSONFile((string)topologyFile));
                pl.AddRange(jSONParser.GetConnections());
            }
            if (JSONFile.TryGetValue(ConnectionKey, out JsonValue jsonConnections))
            {
                foreach (JsonValue jm in jsonConnections)
                {
                    pl.Add(CreatePreConnection(jm.ToString()));
                }
            }

            if (pl.Count == 0)
                throw new Exceptions.JSONnotValidException("JSON file does not contain any modules");

            return pl;
        }

        public SimulationParameters GetParameters()
        {
            if (JSONFile.TryGetValue("parameters", out JsonValue parameters))
            {
                return new SimulationParameters(parameters.ToString());
            }
            throw new Exceptions.JSONnotValidException("JSON file does not contain parameters");
        }
    }
}
