using System;
using System.Collections.Generic;
using SearchCLI.Entity;
using SearchCLI.IDataLayer;

namespace SearchCLI.DataLayer
{
    public class UserMapper: IUserMapper
    {
        public List<User> Load(){
            List<User> users = new List<User>();

            return users;
        }
    }
}
