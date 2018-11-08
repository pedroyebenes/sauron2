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

        JsonValue GetValueFromObject(JsonObject jo, string key)
        {
            if (!jo.TryGetValue(key, out JsonValue value))
            {
                throw new Exceptions.JSONnotValidException("Unable to find the '" + ModuleKey + "' key in " + value);
            }
            return value;
        }

        Module CreateModule(string jsonString)
        {
            JsonObject jo = (JsonObject)JsonValue.Parse(jsonString);

            string type = (string)GetValueFromObject(jo, ModuleType);
            int gates = (int)GetValueFromObject(jo, ModuleGates);

            return Factory.GetModule(type, jsonString);
        }

        public List<Module> GetModules()
        {
            List<Module> ml = new List<Module>();
            if (JSONFile.TryGetValue(ModuleKey, out JsonValue jsonModules))
            {
                foreach (JsonValue jm in jsonModules)
                {
                    ml.Add(CreateModule(jm.ToString()));
                }
            }
            else
            {
                throw new Exceptions.JSONnotValidException("JSON file does not contain the '" + ModuleKey + "' key");
            }

            return ml;
        }

        PreConnection CreatePreConnection(string jsonString)
        {
            JsonObject jo = (JsonObject)JsonValue.Parse(jsonString);
            string moduleA = (string)GetValueFromObject(jo, ConnectionModuleA);
            string moduleB = (string)GetValueFromObject(jo, ConnectionModuleB);
            int portA = (int)GetValueFromObject(jo, ConnectionPortA);
            int portB = (int)GetValueFromObject(jo, ConnectionPortB);
            int indexA = (int)GetValueFromObject(jo, ConnectionIndexA);
            int indexB = (int)GetValueFromObject(jo, ConnectionIndexB);

            return new PreConnection(moduleA, indexA, portA, moduleB, indexB, portB);
        }

        public List<PreConnection> GetConnections()
        {
            List<PreConnection> pl = new List<PreConnection>();

            if (JSONFile.TryGetValue(ConnectionKey, out JsonValue jsonConnections))
            {
                foreach (JsonValue jm in jsonConnections)
                {
                    pl.Add(CreatePreConnection(jm.ToString()));
                }
            }
            else
            {
                throw new Exceptions.JSONnotValidException("JSON file does not contain the '" + ConnectionKey + "' key");
            }
            return pl;
        }
    }
}
