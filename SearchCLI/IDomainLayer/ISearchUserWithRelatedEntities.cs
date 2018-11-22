using System;
using System.Collections.Generic;
using SearchCLI.Entity;

namespace SearchCLI.IDomainLayer
{
    public interface ISearchUserWithRelatedEntities
    {
        List<UserResult> WildcardSearchUserWithRelatedEntities(string searchStr, List<Organization> organizations, List<Ticket> tickets);
    }
}
