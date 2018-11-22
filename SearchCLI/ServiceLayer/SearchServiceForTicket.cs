using System;
using System.Collections.Generic;
using SearchCLI.DataLayer;
using SearchCLI.DomainLayer;
using SearchCLI.Entity;
using SearchCLI.IDataLayer;
using SearchCLI.IDomainLayer;
using SearchCLI.IServiceLayer;

namespace SearchCLI.ServiceLayer
{
    public class SearchServiceForTicket : ISearchService
    {
        ///<summary>
        /// Wildcards the search for Tickets.
        /// Will display all related tickets and the related entities of each ticket.
        /// </summary>
        /// <param name="searchStr">Search string.</param>
        public void WildcardSearch(string searchStr)
        {
            string userFilePath = @"./Data/users.json";
            string ticketFilePath = @"./Data/tickets.json";
            string organizationFilePath = @"./Data/organizations.json";

            IUserMapper userMapper = new UserMapper();
            ITicketMapper ticketMapper = new TicketMapper();
            IOrganizationMapper organizationMapper = new OrganizationMapper();
            IUserDL userDL = new UserDL(userFilePath, userMapper);
            ITicketDL ticketDL = new TicketDL(ticketFilePath, ticketMapper);
            IOrganizationDL organizationDL = new OrganizationDL(organizationFilePath, organizationMapper);
            ISearchTicketWithRelatedEntities searchTicketWithRelatedEntities = new SearchTicketWIthRelatedEntities(ticketDL);
            IPrintDL printDL = new PrintDL(userDL, ticketDL, organizationDL);

            List<User> users = userMapper.Load(userFilePath);
            List<Organization> organizations = organizationMapper.Load(organizationFilePath);

            List<TicketResult> ticketResults = searchTicketWithRelatedEntities.WildcardSearchTicketWithRelatedEntities(searchStr, users, organizations);

            printDL.PrintTicketResult(ticketResults);

        }
    }
}
