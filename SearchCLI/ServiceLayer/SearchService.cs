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
        readonly string _userFilePath;
        readonly string _ticketFilePath;
        readonly string _organizationFilePath;
        readonly IUserMapper _userMapper;
        readonly ITicketMapper _ticketMapper;
        readonly IOrganizationMapper _organizationMapper;
        readonly IUserDL _userDL;
        readonly ITicketDL _ticketDL;
        readonly IOrganizationDL _organizationDL;
        readonly ISearchTicketWithRelatedEntities _searchTicketWithRelatedEntities;
        readonly ISearchOrganizationWithRelatedEnties _searchOrganizationWithRelatedEnties;
        readonly ISearchUserWithRelatedEntities _searchUserWithRelatedEntities;
        readonly IPrintDL _printDL;

        public SearchService(string userFilePath, string ticketFilePath, string organizationFilePath)
        {
            _userFilePath = userFilePath;
            _ticketFilePath = ticketFilePath;
            _organizationFilePath = organizationFilePath;
            _userMapper = new UserMapper();
            _ticketMapper = new TicketMapper();
            _organizationMapper = new OrganizationMapper();
            _userDL = new UserDL(_userFilePath, _userMapper);
            _ticketDL = new TicketDL(_ticketFilePath, _ticketMapper);
            _organizationDL = new OrganizationDL(_organizationFilePath, _organizationMapper);
            _searchUserWithRelatedEntities = new SearchUserWithRelatedEntities(_userDL);
            _searchTicketWithRelatedEntities = new SearchTicketWIthRelatedEntities(_ticketDL);
            _searchOrganizationWithRelatedEnties = new SearchOrganizationWithRelatedEntities(_organizationDL);
            _printDL = new PrintDL(_userDL, _ticketDL, _organizationDL);
        }

        public SearchService(string userFilePath, string ticketFilePath, string organizationFilePath,
                             IUserMapper userMapper, ITicketMapper ticketMapper, IOrganizationMapper organizationMapper,
                             IUserDL userDL, ITicketDL ticketDL, IOrganizationDL organizationDL,
                             ISearchUserWithRelatedEntities searchUserWithRelatedEntities,
                             ISearchTicketWithRelatedEntities searchTicketWithRelatedEntities,
                             ISearchOrganizationWithRelatedEnties searchOrganizationWithRelatedEnties,
                             IPrintDL printDL)
        {
            _userFilePath = userFilePath;
            _ticketFilePath = ticketFilePath;
            _organizationFilePath = organizationFilePath;
            _userMapper = userMapper;
            _ticketMapper = ticketMapper;
            _organizationMapper = organizationMapper;
            _userDL = userDL;
            _ticketDL = ticketDL;
            _organizationDL = organizationDL;
            _searchUserWithRelatedEntities = searchUserWithRelatedEntities;
            _searchTicketWithRelatedEntities = searchTicketWithRelatedEntities;
            _searchOrganizationWithRelatedEnties = searchOrganizationWithRelatedEnties;
            _printDL = printDL;
        }

        /// <summary>
        /// Wildcards the search for User, Ticket and Organization.
        /// There are three groups of results showing.
        /// Each group will contain a list of enities and the related entities of each entities will be included as well.
        /// </summary>
        /// <param name="searchStr">Search string.</param>
        public void WildcardSearch(string searchStr){
            //string path = Directory.GetCurrentDirectory();
            //string userFilePath = path + @"/Data/users.json";
            //string ticketFilePath = path + @"/Data/tickets.json";
            //string organizationFilePath = path + @"/Data/organizations.json";

            //IUserMapper userMapper = new UserMapper();
            //ITicketMapper ticketMapper = new TicketMapper();
            //IOrganizationMapper organizationMapper = new OrganizationMapper();
            //IUserDL userDL = new UserDL(userFilePath, userMapper);
            //ITicketDL ticketDL = new TicketDL(ticketFilePath, ticketMapper);
            //IOrganizationDL organizationDL = new OrganizationDL(organizationFilePath, organizationMapper);
            //ISearchUserWithRelatedEntities searchUserWithRealtedEntities = new SearchUserWithRelatedEntities(userDL);
            //ISearchTicketWithRelatedEntities searchTicketWithRelatedEntities = new SearchTicketWIthRelatedEntities(ticketDL);
            //ISearchOrganizationWithRelatedEnties searchOrganizationWithRelatedEnties = new SearchOrganizationWithRelatedEntities(organizationDL);
            //IPrintDL printDL = new PrintDL(userDL, ticketDL, organizationDL);

            List<User> users = _userMapper.Load(_userFilePath);
            List<Organization> organizations = _organizationMapper.Load(_organizationFilePath);
            List<Ticket> tickets = _ticketMapper.Load(_ticketFilePath);

            List<UserResult> userResults = _searchUserWithRelatedEntities.WildcardSearchUserWithRelatedEntities(searchStr,organizations,tickets);
            List<TicketResult> ticketResults = _searchTicketWithRelatedEntities.WildcardSearchTicketWithRelatedEntities(searchStr, users, organizations);
            List<OrganizationResult> organizationResults = _searchOrganizationWithRelatedEnties.WildcardSearchOrganizationWithRelatedEntities(searchStr, users, tickets);

            _printDL.PrintUserResult(userResults);
            Console.WriteLine("******************************************************************************************");
            _printDL.PrintTicketResult(ticketResults);
            Console.WriteLine("******************************************************************************************");
            _printDL.PrintOrganizationResult(organizationResults);

        }
    }
}