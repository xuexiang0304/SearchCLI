using System;
using System.Collections.Generic;
using SearchCLI.Entity;

namespace SearchCLI.IDataLayer
{
    public interface IUserMapper
    {
        List<User> Load(string filePath);
    }
}
