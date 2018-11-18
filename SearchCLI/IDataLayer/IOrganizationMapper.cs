using System;
using System.Collections.Generic;
using SearchCLI.Entity;

namespace SearchCLI.IDataLayer
{
    public interface IOrganizationMapper
    {
        List<Organization> Load();
    }
}
