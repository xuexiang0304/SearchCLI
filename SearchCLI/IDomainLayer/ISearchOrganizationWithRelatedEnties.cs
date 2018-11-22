using System;
using System.Collections.Generic;
using SearchCLI.Entity;

namespace SearchCLI.IDomainLayer
{
    public interface ISearchOrganizationWithRelatedEnties
    {
        List<OrganizationResult> WildcardSearchOrganizationWithRelatedEntities(string searchStr, List<User> users, List<Ticket> tickets);
    }
}
