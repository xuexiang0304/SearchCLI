using System;
using System.Collections.Generic;
using SearchCLI.Entity;
using SearchCLI.IDomainLayer;

namespace SearchCLI.DomainLayer
{
    public class SearchTicketWIthRelatedEntities: ISearchTicketWithRelatedEntities
    {
        readonly ITicketDL _ticketDL;
        public SearchTicketWIthRelatedEntities(ITicketDL ticketDL)
        {
            this._ticketDL = ticketDL;
        }

        /// <summary>
        /// Wildcards the search ticket with related entities.
        /// </summary>
        /// <returns>A list of ticket with related entities.</returns>
        /// <param name="searchStr">Search string.</param>
        /// <param name="users">Users.</param>
        /// <param name="organizations">Organizations.</param>
        public List<TicketResult> WildcardSearchTicketWithRelatedEntities(string searchStr, List<User> users, List<Organization> organizations)
        {
            List<Ticket> tickets = _ticketDL.WildcardSearchTickets(searchStr);
            List<TicketResult> ticketResults = new List<TicketResult>();

            foreach (Ticket ticket in tickets)
            {
                User submitter = users.Find(u => u._id == ticket.submitter_id);
                User assignee = users.Find(u => u._id == ticket.assignee_id);
                Organization organization = organizations.Find(o => o._id == ticket.organization_id);
                TicketResult ticketResult = new TicketResult(ticket, submitter, assignee, organization);
                ticketResults.Add(ticketResult);
            }
            return ticketResults;
        }
    }
}
