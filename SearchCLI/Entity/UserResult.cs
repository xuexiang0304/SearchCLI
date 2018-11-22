using System;
using System.Collections.Generic;

namespace SearchCLI.Entity
{
    public class UserResult
    {
        public User user { get; set; }
        public Organization organization { get; set; }
        public List<Ticket> submitttedTickets { get; set; }
        public List<Ticket> assingedTickets { get; set; }

        public UserResult(User user, Organization organization, List<Ticket> submittedTickets, List<Ticket> assignedTickets)
        {
            this.user = user;
            this.organization = organization;
            this.submitttedTickets = submittedTickets;
            this.assingedTickets = assignedTickets;
        }
    }
}
