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
        readonly string _userFilePath;
        readonly string _ticketFilePath;
        readonly string _organizationFilePath;
        readonly IUserMapper _userMapper;
        readonly ITicketMapper _ticketMapper;
        readonly IOrganizationMapper _organizationMapper;
        readonly IUserDL _userDL;
        readonly ITicketDL _ticketDL;
        readonly IOrganizationDL _organizationDL;
        readonly ISearchUserWithRelatedEntities _searchUserWithRelatedEntities;
        readonly IPrintDL _printDL;

        public SearchServiceForUser(string userFilePath, string ticketFilePath, string organizationFilePath)
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
            _printDL = new PrintDL(_userDL, _ticketDL, _organizationDL);
        }

        public SearchServiceForUser(string userFilePath, string ticketFilePath, string organizationFilePath,
                                    IUserMapper userMapper, ITicketMapper ticketMapper, IOrganizationMapper organizationMapper,
                                    IUserDL userDL, ITicketDL ticketDL, IOrganizationDL organizationDL,
                                    ISearchUserWithRelatedEntities searchUserWithRelatedEntities,
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
            _printDL = printDL;
        }

        ///<summary>
        /// Wildcards the search for Users.
        /// Will display all related users and the related entities of each user.
        /// </summary>
        /// <param name="searchStr">Search string.</param>
        public void WildcardSearch(string searchStr)
        {
            List<Organization> organizations = _organizationMapper.Load(_organizationFilePath);
            List<Ticket> tickets = _ticketMapper.Load(_ticketFilePath);

            List<UserResult> userResults = _searchUserWithRelatedEntities.WildcardSearchUserWithRelatedEntities(searchStr, organizations, tickets);
           
            _printDL.PrintUserResult(userResults);
           
        }
    }
}
