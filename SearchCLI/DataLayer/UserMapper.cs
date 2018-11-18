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
        public List<User> Load(){
            string filePath = "../Data/users.json";
            List<User> users = new List<User>();
            using(StreamReader r = new StreamReader(filePath)){
                var json = r.ReadToEnd();
                users = JsonConvert.DeserializeObject<List<User>>(json);
            }
            return users;
        }
    }
}
