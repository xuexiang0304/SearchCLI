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
        readonly string _userFilePath;
        readonly string _ticketFilePath;
        readonly string _organizationFilePath;
        readonly IUserMapper _userMapper;
        readonly ITicketMapper _ticketMapper;
        readonly IOrganizationMapper _organizationMapper;
        readonly IUserDL _userDL;
        readonly ITicketDL _ticketDL;
        readonly IOrganizationDL _organizationDL;
        readonly ISearchOrganizationWithRelatedEnties _searchOrganizationWithRelatedEnties;
        readonly IPrintDL _printDL;

        public SearchServiceForOrganization(string userFilePath, string ticketFilePath, string organizationFilePath)
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
            _searchOrganizationWithRelatedEnties = new SearchOrganizationWithRelatedEntities(_organizationDL);
            _printDL = new PrintDL(_userDL, _ticketDL, _organizationDL);
        }

        public SearchServiceForOrganization(string userFilePath, string ticketFilePath, string organizationFilePath,
                                            IUserMapper userMapper, ITicketMapper ticketMapper, IOrganizationMapper organizationMapper,
                                            IUserDL userDL, ITicketDL ticketDL, IOrganizationDL organizationDL,
                                            ISearchOrganizationWithRelatedEnties searchOrganizationWithRelatedEnties,
                                            IPrintDL printDL){
            _userFilePath = userFilePath;
            _ticketFilePath = ticketFilePath;
            _organizationFilePath = organizationFilePath;
            _userMapper = userMapper;
            _ticketMapper = ticketMapper;
            _organizationMapper = organizationMapper;
            _userDL = userDL;
            _ticketDL = ticketDL;
            _organizationDL = organizationDL;
            _searchOrganizationWithRelatedEnties = searchOrganizationWithRelatedEnties;
            _printDL = printDL;
        }

        /// <summary>
        /// Wildcards the search for Organizations.
        /// Will display all related organizations and the related entities of each organizaition.
        /// </summary>
        /// <param name="searchStr">Search string.</param>
        public void WildcardSearch(string searchStr)
        {
           // ISearchOrganizationWithRelatedEnties searchOrganizationWithRelatedEnties = new SearchOrganizationWithRelatedEntities(_organizationDL);
           // IPrintDL printDL = new PrintDL(_userDL, _ticketDL, _organizationDL);

            List<User> users = _userMapper.Load(_userFilePath);
            List<Ticket> tickets = _ticketMapper.Load(_ticketFilePath);

            List<OrganizationResult> organizationResults = _searchOrganizationWithRelatedEnties.WildcardSearchOrganizationWithRelatedEntities(searchStr, users, tickets);

            _printDL.PrintOrganizationResult(organizationResults);

        }
    }
}
