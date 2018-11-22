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
    public class SearchServiceForUser : ISearchService
    {
        public SearchServiceForUser(){

        }
        ///<summary>
        /// Wildcards the search for Users.
        /// Will display all related users and the related entities of each user.
        /// </summary>
        /// <param name="searchStr">Search string.</param>
        public void WildcardSearch(string searchStr)
        {
            string path = Directory.GetCurrentDirectory();
            string userFilePath = path + @"/Data/users.json";
            string ticketFilePath = @"./Data/tickets.json";
            string organizationFilePath = @"./Data/organizations.json";

            IUserMapper userMapper = new UserMapper();
            ITicketMapper ticketMapper = new TicketMapper();
            IOrganizationMapper organizationMapper = new OrganizationMapper();
            IUserDL userDL = new UserDL(userFilePath, userMapper);
            ITicketDL ticketDL = new TicketDL(ticketFilePath, ticketMapper);
            IOrganizationDL organizationDL = new OrganizationDL(organizationFilePath, organizationMapper);
            ISearchUserWithRelatedEntities searchUserWithRealtedEntities = new SearchUserWithRelatedEntities(userDL);
            IPrintDL printDL = new PrintDL(userDL, ticketDL, organizationDL);

            List<Organization> organizations = organizationMapper.Load(organizationFilePath);
            List<Ticket> tickets = ticketMapper.Load(ticketFilePath);

            List<UserResult> userResults = searchUserWithRealtedEntities.WildcardSearchUserWithRelatedEntities(searchStr, organizations, tickets);
           
            printDL.PrintUserResult(userResults);
           
        }
    }
}
