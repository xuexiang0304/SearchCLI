using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SearchCLI.Entity;
using SearchCLI.IDataLayer;

namespace SearchCLI.DataLayer
{
    public class UserMapper: IUserMapper
    {
        /// <summary>
        /// Load the json string from the json file and convert to a list of Users.
        /// </summary>
        /// <returns>A list of Users.</returns>
        /// <param name="filePath">File path.</param>
        public List<User> Load(string filePath){
            List<User> users = new List<User>();
            using(StreamReader r = new StreamReader(filePath)){
                var json = r.ReadToEnd();
                users = JsonConvert.DeserializeObject<List<User>>(json);
            }
            return users;
        }
    }
}
