using System;
using System.Collections.Generic;

namespace SearchCLI.Entity
{
    public class OrganizationResult
    {
        public Organization organization { get; set; }
        public List<User> organizationUsers { get; set; }
        public List<Ticket> organizationTickets { get; set; }

        public OrganizationResult(Organization organization, List<User> organizationUsers, List<Ticket> organizationTickets)
        {
            this.organization = organization;
            this.organizationUsers = organizationUsers;
            this.organizationTickets = organizationTickets;
        }
    }
}
