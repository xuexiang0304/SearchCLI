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
        readonly IPrintDL _printDL;

        public SearchServiceForTicket(string userFilePath, string ticketFilePath, string organizationFilePath)
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
            _searchTicketWithRelatedEntities = new SearchTicketWIthRelatedEntities(_ticketDL);
            _printDL = new PrintDL(_userDL, _ticketDL, _organizationDL);
        }

        public SearchServiceForTicket(string userFilePath, string ticketFilePath, string organizationFilePath,
                                      IUserMapper userMapper, IOrganizationMapper organizationMapper,
                                      ISearchTicketWithRelatedEntities searchTicketWithRelatedEntities,
                                      IPrintDL printDL)
        {
            _userFilePath = userFilePath;
            _ticketFilePath = ticketFilePath;
            _organizationFilePath = organizationFilePath;
            _userMapper = userMapper;
            _organizationMapper = organizationMapper;
            _searchTicketWithRelatedEntities = searchTicketWithRelatedEntities;
            _printDL = printDL;
        }

        ///<summary>
        /// Wildcards the search for Tickets.
        /// Will display all related tickets and the related entities of each ticket.
        /// </summary>
        /// <param name="searchStr">Search string.</param>
        public void WildcardSearch(string searchStr)
        {
            List<User> users = _userMapper.Load(_userFilePath);
            List<Organization> organizations = _organizationMapper.Load(_organizationFilePath);

            List<TicketResult> ticketResults = _searchTicketWithRelatedEntities.WildcardSearchTicketWithRelatedEntities(searchStr, users, organizations);

            _printDL.PrintTicketResult(ticketResults);

        }
    }
}
