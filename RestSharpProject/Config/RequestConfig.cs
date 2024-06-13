using System;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using RestSharpProject.Helpers;

namespace RestSharpProject.Config
{
    public class RequestConfig
    {

        // PROPERTIES
        public string PathToJson = "RestSharpProject/Config/TrelloConfig.json";
        private Dictionary<string, string> _configSelection = new Dictionary<string, string>();


        // METHODS
        public Dictionary<string, string> ConfigBuilder(string configName) 
        {
            try 
            {
                var jsonString = File.ReadAllText(PathHelper.GetFilePath(PathToJson));
                JObject jsonObject = JObject.Parse(jsonString);

                // Handling case where the jsonObject is null
                _configSelection = jsonObject[configName].ToObject<Dictionary<string, string>>() ?? throw new Exception("Config is null.");

                return _configSelection;

                
            } catch (Exception e) 
            {
                Debug.WriteLine($"Error: {e.Message}");
                throw;
            }
        } // ConfigBuilder end     

    }
}
