using System;
using System.Collections.Generic;
using System.IO;
using SearchCLI.DataLayer;
using SearchCLI.DomainLayer;
using SearchCLI.Entity;
using SearchCLI.IDataLayer;
using SearchCLI.IDomainLayer;
using SearchCLI.IServiceLayer;

namespace SearchCLI.ServiceLayer
{
    public class SearchService: ISearchService
    {
        /// <summary>
        /// Wildcards the search for User, Ticket and Organization.
        /// There are three groups of results showing.
        /// Each group will contain a list of enities and the related entities of each entities will be included as well.
        /// </summary>
        /// <param name="searchStr">Search string.</param>
        public void WildcardSearch(string searchStr){
            string path = Directory.GetCurrentDirectory();
            string userFilePath = path + @"/Data/users.json";
            string ticketFilePath = path + @"/Data/tickets.json";
            string organizationFilePath = path + @"/Data/organizations.json";

            IUserMapper userMapper = new UserMapper();
            ITicketMapper ticketMapper = new TicketMapper();
            IOrganizationMapper organizationMapper = new OrganizationMapper();
            IUserDL userDL = new UserDL(userFilePath, userMapper);
            ITicketDL ticketDL = new TicketDL(ticketFilePath, ticketMapper);
            IOrganizationDL organizationDL = new OrganizationDL(organizationFilePath, organizationMapper);
            ISearchUserWithRelatedEntities searchUserWithRealtedEntities = new SearchUserWithRelatedEntities(userDL);
            ISearchTicketWithRelatedEntities searchTicketWithRelatedEntities = new SearchTicketWIthRelatedEntities(ticketDL);
            ISearchOrganizationWithRelatedEnties searchOrganizationWithRelatedEnties = new SearchOrganizationWithRelatedEntities(organizationDL);
            IPrintDL printDL = new PrintDL(userDL, ticketDL, organizationDL);

            List<User> users = userMapper.Load(userFilePath);
            List<Organization> organizations = organizationMapper.Load(organizationFilePath);
            List<Ticket> tickets = ticketMapper.Load(ticketFilePath);

            List<UserResult> userResults = searchUserWithRealtedEntities.WildcardSearchUserWithRelatedEntities(searchStr,organizations,tickets);
            List<TicketResult> ticketResults = searchTicketWithRelatedEntities.WildcardSearchTicketWithRelatedEntities(searchStr, users, organizations);
            List<OrganizationResult> organizationResults = searchOrganizationWithRelatedEnties.WildcardSearchOrganizationWithRelatedEntities(searchStr, users, tickets);

            printDL.PrintUserResult(userResults);
            Console.WriteLine("******************************************************************************************");
            printDL.PrintTicketResult(ticketResults);
            Console.WriteLine("******************************************************************************************");
            printDL.PrintOrganizationResult(organizationResults);

        }
    }
}