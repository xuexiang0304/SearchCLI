using System;
using System.Collections.Generic;
using SearchCLI.Entity;

namespace SearchCLI.IDomainLayer
{
    public interface IPrintDL
    {
        void PrintUserResult(List<UserResult> userResults);
        void PrintTicketResult(List<TicketResult> ticketResults);
        void PrintOrganizationResult(List<OrganizationResult> organizationResults);
    }
}
