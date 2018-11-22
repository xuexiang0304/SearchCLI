using System;
using System.Collections.Generic;
using System.Linq;
using SearchCLI.Entity;
using SearchCLI.IDomainLayer;

namespace SearchCLI.DomainLayer
{
    public class PrintDL: IPrintDL
    {
        readonly IUserDL _userDL;
        readonly ITicketDL _ticketDL;
        readonly IOrganizationDL _organizationDL;

        public PrintDL(IUserDL userDL, ITicketDL ticketDL, IOrganizationDL organizationDL)
        {
            this._userDL = userDL;
            this._ticketDL = ticketDL;
            this._organizationDL = organizationDL;
        }

        /// <summary>
        /// Prints the user search result.
        /// Display a list uses with realted entities
        /// </summary>
        /// <param name="userResults">User results.</param>
        public void PrintUserResult(List<UserResult> userResults){
            Console.WriteLine("There are {0} users show in the result", userResults.Count());
            foreach (var result in userResults)
            {
                Console.WriteLine("The details of user {0} is shown below:", result.user.name);
                _userDL.PrintUser(result.user);
                if (result.organization != null)
                {
                    Console.WriteLine("User {0} belong to organization {1}.", result.user.name, result.organization.name);
                    _organizationDL.PrintOrganization(result.organization);
                }
                else
                    Console.WriteLine("User {0} does not belong to any organization.", result.user.name);
                Console.WriteLine();
                Console.WriteLine("There are {0} tickets submitted by user {1}({2})", result.submitttedTickets.Count(), result.user.name, result.user._id);
                foreach (var ticket in result.submitttedTickets)
                {
                    _ticketDL.PrintTicket(ticket);
                }
                Console.WriteLine();
                Console.WriteLine("There are {0} tickets assgined by user {1}({2})", result.assingedTickets.Count(), result.user.name, result.user._id);
                foreach (var ticket in result.assingedTickets)
                {
                    _ticketDL.PrintTicket(ticket);
                }
                Console.WriteLine("----------------------------------------------------------------------------------");
            }
        }

        /// <summary>
        /// Prints the ticket search result.
        /// Display a list of tickets with related entities
        /// </summary>
        /// <param name="ticketResults">Ticket results.</param>
        public void PrintTicketResult(List<TicketResult> ticketResults){
            Console.WriteLine("There are {0} tickets show in the result", ticketResults.Count());
            foreach (var result in ticketResults)
            {
                //Console.WriteLine("The details of ticket \"{0}\" are shown below:", result.ticket.description);
                _ticketDL.PrintTicket(result.ticket);
                if (result.ticketSubmitter != null)
                {
                    Console.WriteLine("The ticket was submitted by user {0}({1})", result.ticketSubmitter.name, result.ticketSubmitter._id);
                    _userDL.PrintUser(result.ticketSubmitter);
                }
                else
                    Console.WriteLine("There is no information about the submitter");
                if (result.ticketAssignee != null)
                {
                    Console.WriteLine("The ticket was assgined by user {0}({1})", result.ticketAssignee.name, result.ticketAssignee._id);
                    _userDL.PrintUser(result.ticketAssignee);
                }
                else
                    Console.WriteLine("These is no information about the assignee");
                if (result.organization != null)
                {
                    Console.WriteLine("The ticket is from organisation: {0}", result.organization._id);
                    _organizationDL.PrintOrganization(result.organization);
                }
                else
                    Console.WriteLine("There is no information about the organization of this ticket");
                Console.WriteLine("----------------------------------------------------------------------------------");
            }
        }

        /// <summary>
        /// Prints the organization search result.
        /// Display a list of organizations with related entities.
        /// </summary>
        /// <param name="organizationResults">Organization results.</param>
        public void PrintOrganizationResult(List<OrganizationResult> organizationResults){
            Console.WriteLine("There are {0} organizations show in the result", organizationResults.Count());
            foreach (var result in organizationResults)
            {
                _organizationDL.PrintOrganization(result.organization);
                Console.WriteLine();
                Console.WriteLine("There are {0} users belongs to organization {1}({2}). The information of these users are shown below:", result.organizationUsers.Count(), result.organization.name, result.organization._id);
                foreach (var user in result.organizationUsers)
                {
                    _userDL.PrintUser(user);
                }
                Console.WriteLine();
                Console.WriteLine("There are {0} tickets belongs to organization {1}({2}). The information of these tickets are shown below:", result.organizationTickets.Count(), result.organization.name, result.organization._id);
                foreach (var ticket in result.organizationTickets)
                {
                    _ticketDL.PrintTicket(ticket);
                }
                Console.WriteLine("----------------------------------------------------------------------------------");
            }
        }
    }
}
