using Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace ImdbProject
{
    class StoringValues
    {
        public StoringValues()
        {
        }

        public void SerializeJsonToFile(Actor actor, string filePath)
        {
            List<KeyValuePair<int, string>> actors = new List<KeyValuePair<int, string>>();

            if (File.Exists(filePath))
            {
                var jsonData = File.ReadAllText(filePath);
                var employeeList = JsonConvert.DeserializeObject<List<Actor>>(jsonData)
                                      ?? new List<Actor>();
                employeeList.Add(actor);
                jsonData = JsonConvert.SerializeObject(employeeList);
                System.IO.File.WriteAllText(filePath, jsonData);
            }
            else
            {
                var theFile = File.Create(filePath);
                theFile.Close();
                var jsonData = File.ReadAllText(filePath);
                var employeeList = JsonConvert.DeserializeObject<List<Actor>>(jsonData)
                                      ?? new List<Actor>();
                employeeList.Add(actor);
                jsonData = JsonConvert.SerializeObject(employeeList);
                File.WriteAllText(filePath, jsonData);
            }
        }

        public List<Actor> DeserialiseJsonFromFile(string filePath)
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                List<Actor> actors = JsonConvert.DeserializeObject<List<Actor>>(json);
                return actors;
            }

        }


    }
}
