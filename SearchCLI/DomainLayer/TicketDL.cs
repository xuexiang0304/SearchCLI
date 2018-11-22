using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SearchCLI.Entity;
using SearchCLI.IDataLayer;
using SearchCLI.IDomainLayer;

namespace SearchCLI.DomainLayer
{
    public class TicketDL: ITicketDL
    {
        readonly string ticketFilePath;
        readonly ITicketMapper ticketMapper;

        public TicketDL(string ticketFilePath, ITicketMapper ticketMapper)
        {
            this.ticketFilePath = ticketFilePath;
            this.ticketMapper = ticketMapper;
        }

        /// <summary>
        /// Wildcards the search tickets.
        /// only return a list of tickets without related entities
        /// </summary>
        /// <returns>The search tickets.</returns>
        /// <param name="searchStr">Search string.</param>
        public List<Ticket> WildcardSearchTickets(string searchStr)
        {
            List<Ticket> tickets = new List<Ticket>();
            List<Ticket> ticketsource = ticketMapper.Load(ticketFilePath);
            Regex rx = new Regex(@"(^|\s+)" + searchStr + @"(\s+|$)");
            tickets = ticketsource.FindAll(ticket => ticket._id == searchStr
                                           || (ticket.url != null && rx.IsMatch(ticket.url))
                                           || (ticket.external_id != null && rx.IsMatch(ticket.external_id))
                                           || (ticket.created_at != null && rx.IsMatch(ticket.created_at))
                                           || (ticket.type != null && rx.IsMatch(ticket.type))
                                           || (ticket.subject != null && rx.IsMatch(ticket.subject))
                                           || (ticket.description != null && rx.IsMatch(ticket.description))
                                           || (ticket.priority != null && rx.IsMatch(ticket.priority))
                                           || (ticket.status != null && rx.IsMatch(ticket.status))
                                           || (ticket.submitter_id != null && ticket.submitter_id.ToString() == searchStr)
                                           || (ticket.assignee_id != null && ticket.assignee_id.ToString() == searchStr)
                                           || (ticket.organization_id != null && ticket.organization_id.ToString() == searchStr)
                                           || (ticket.tags != null && rx.IsMatch(string.Join(" ", ticket.tags.ToArray())))
                                           || (ticket.has_incidents != null && ticket.has_incidents.ToString().ToLower() == searchStr.ToLower())
                                           || (ticket.due_at != null && rx.IsMatch(ticket.due_at))
                                           || (ticket.via != null && rx.IsMatch(ticket.via)));
            return tickets;
        }

        /// <summary>
        /// Prints the ticket object with a certain format
        /// </summary>
        /// <param name="ticket">Ticket.</param>
        public void PrintTicket(Ticket ticket){
            Console.WriteLine();
            Console.WriteLine("The information of ticket ({0}) is shown below:", ticket._id);
            Console.WriteLine(@"id:{0},
external_id:{1},
created_at:{3}.
type:{4},
subject:{5},
description:{6},
priority:{7},
status:{8},
submitter_id:{9},
assignee_id:{10},
organization_id:{11},
tags:{12},
has_incidents:{13},
due_at:{14},
via:{15}",
                              ticket._id, ticket.url, ticket.external_id, ticket.created_at,
                              ticket.type, ticket.subject, ticket.description, ticket.priority,
                              ticket.status, ticket.submitter_id, ticket.assignee_id, ticket.organization_id,
                              ticket.tags == null ? "" : string.Join(",", ticket.tags.ToArray()), ticket.has_incidents, ticket.due_at, ticket.via);
            Console.WriteLine();
        }
    }
}
