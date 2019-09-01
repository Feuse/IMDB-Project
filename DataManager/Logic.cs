

using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using Contracts;

namespace DataManager
{
    public class Logic : ILogic
    {
        static string filePath;
        static string backup;
        private static ConcurrentDictionary<string, Actor> originalList;
  
        

        static Logic()
        {
            originalList = new ConcurrentDictionary<string, Actor>();
            backup = ConfigurationManager.AppSettings["backup"];
            filePath = ConfigurationManager.AppSettings["tempList"];
            File.Copy(filePath, BACKUP, true);

            SaveOriginal();
          
        }



        public async static Task<List<Actor>> GetCelebritiesInner()
        {
            return originalList.Values.ToList();
        }

        public async Task<List<Actor>> GetAllActorsAsync()
        {
            
            return await GetCelebritiesInner();
        }




        // Try to read the data from the Json and initialize it. if failed , initialize with whatever it got. return 

        private static List<Actor> ReadActorsFromJson(string json)
        {
            List<Actor> celebListReadFromFile;
            try
            {

                var celebJson = File.ReadAllText(json);
                celebListReadFromFile = JsonConvert.DeserializeObject<List<Actor>>(celebJson);

            }
            catch (Exception ex)
            {
                celebListReadFromFile = new List<Actor>();
                // Empty list/whatever it got in it
            }
            return celebListReadFromFile;
        }

        public async Task RemoveActorAsync(string name)
        {

            if (originalList.TryRemove(name, out Actor removedActor))
            {
                var jsonToWrite = JsonConvert.SerializeObject(await GetCelebritiesInner());

                try
                {
                    File.WriteAllText(filePath, jsonToWrite);
                }
                catch (Exception ex)
                {
                    //Unable to remove due to an error.

                }
            }
        }

        public async Task ResetAsync()
        {
            originalList.Clear();
            await UpdateFile();
            await SaveOriginal();
        }

        //Saving the actor, adding the name as key & object as value.
        public static async Task SaveOriginal()
        {
            foreach (var currceleb in ReadActorsFromJson(filePath))
            {
                var curr = currceleb;
                originalList.TryAdd(currceleb.name, currceleb);
            }
        }

        public static async Task UpdateFile()
        {
            File.Copy(BACKUP, filePath, true);
        }
    }
}

