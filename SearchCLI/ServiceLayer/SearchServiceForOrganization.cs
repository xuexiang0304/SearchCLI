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
    public class SearchServiceForOrganization : ISearchService
    {
        /// <summary>
        /// Wildcards the search for Organizations.
        /// Will display all related organizations and the related entities of each organizaition.
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
            ISearchOrganizationWithRelatedEnties searchOrganizationWithRelatedEnties = new SearchOrganizationWithRelatedEntities(organizationDL);
            IPrintDL printDL = new PrintDL(userDL, ticketDL, organizationDL);

            List<User> users = userMapper.Load(userFilePath);
            List<Ticket> tickets = ticketMapper.Load(ticketFilePath);

            List<OrganizationResult> organizationResults = searchOrganizationWithRelatedEnties.WildcardSearchOrganizationWithRelatedEntities(searchStr, users, tickets);

            printDL.PrintOrganizationResult(organizationResults);

        }
    }
}
