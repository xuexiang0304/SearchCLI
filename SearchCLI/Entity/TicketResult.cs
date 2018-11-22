using System;
namespace SearchCLI.Entity
{
    public class TicketResult
    {
        public Ticket ticket { get; set; }
        public User ticketSubmitter { get; set; }
        public User ticketAssignee { get; set; }
        public Organization organization { get; set; }

        public TicketResult(Ticket ticket, User ticketSubmitter, User ticketAssignee, Organization organization)
        {
            this.ticket = ticket;
            this.ticketSubmitter = ticketSubmitter;
            this.ticketAssignee = ticketAssignee;
            this.organization = organization;
        }
    }
}
