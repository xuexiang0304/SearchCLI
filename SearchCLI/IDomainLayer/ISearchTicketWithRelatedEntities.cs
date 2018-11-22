using System;
using System.Collections.Generic;
using SearchCLI.Entity;

namespace SearchCLI.IDomainLayer
{
    public interface ISearchTicketWithRelatedEntities
    {
        List<TicketResult> WildcardSearchTicketWithRelatedEntities(string searchStr, List<User> users, List<Organization> organizations);
    }
}
