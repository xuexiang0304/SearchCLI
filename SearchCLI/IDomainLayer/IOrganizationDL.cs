using System;
using System.Collections.Generic;
using SearchCLI.Entity;

namespace SearchCLI.IDomainLayer
{
    public interface IOrganizationDL
    {
        List<Organization> WildcardSearchOrganizations(string searchStr);
        void PrintOrganization(Organization organization);
    }
}
