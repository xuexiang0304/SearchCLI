using System;
using System.Collections.Generic;
using SearchCLI.Entity;

namespace SearchCLI.IDomainLayer
{
    public interface IUserDL
    {
        List<User> WildcardSearchUsers(string searchStr);
        void PrintUser(User user);
    }
}
