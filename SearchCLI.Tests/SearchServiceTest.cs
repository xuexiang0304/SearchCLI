using System;
using System.Collections.Generic;
using Moq;
using SearchCLI.Entity;
using SearchCLI.IDataLayer;
using SearchCLI.IDomainLayer;
using SearchCLI.IServiceLayer;
using SearchCLI.ServiceLayer;
using Xunit;

namespace SearchCLI.Tests
{
    public class SearchServiceTest
    {
        Mock<IUserMapper> _mockuserMapper;
        Mock<ITicketMapper> _mockticketMapper;
        Mock<IOrganizationMapper> _mockorganizationMapper;
        Mock<ISearchTicketWithRelatedEntities> _mocksearchTicketWithRelatedEntities;
        Mock<ISearchOrganizationWithRelatedEnties> _mocksearchOrganizationWithRelatedEnties;
        Mock<ISearchUserWithRelatedEntities> _mocksearchUserWithRelatedEntities;
        Mock<IPrintDL> _mockprintDL;
        public SearchServiceTest()
        {
            mock();
        }

        private void mock(){
            _mockuserMapper = new Mock<IUserMapper>();
            _mockuserMapper.Setup(um => um.Load(It.IsAny<string>())).Verifiable();
            _mockticketMapper = new Mock<ITicketMapper>();
            _mockticketMapper.Setup(tm => tm.Load(It.IsAny<string>())).Verifiable();
            _mockorganizationMapper = new Mock<IOrganizationMapper>();
            _mockorganizationMapper.Setup(om => om.Load(It.IsAny<string>())).Verifiable();
            _mocksearchUserWithRelatedEntities = new Mock<ISearchUserWithRelatedEntities>();
            _mocksearchUserWithRelatedEntities.Setup(su => su.WildcardSearchUserWithRelatedEntities(It.IsAny<string>(),
                                                                                               It.IsAny<List<Organization>>(),
                                                                                               It.IsAny<List<Ticket>>())).Verifiable();
            _mocksearchTicketWithRelatedEntities = new Mock<ISearchTicketWithRelatedEntities>();
            _mocksearchTicketWithRelatedEntities.Setup(st => st.WildcardSearchTicketWithRelatedEntities(It.IsAny<string>(),
                                                                                                   It.IsAny<List<User>>(),
                                                                                                   It.IsAny<List<Organization>>())).Verifiable();

            _mocksearchOrganizationWithRelatedEnties = new Mock<ISearchOrganizationWithRelatedEnties>();
            _mocksearchOrganizationWithRelatedEnties.Setup(so => so.WildcardSearchOrganizationWithRelatedEntities(It.IsAny<string>(),
                                                                                                             It.IsAny<List<User>>(),
                                                                                                             It.IsAny<List<Ticket>>())).Verifiable();

            _mockprintDL = new Mock<IPrintDL>();
            _mockprintDL.Setup(p => p.PrintUserResult(It.IsAny<List<UserResult>>())).Verifiable();
            _mockprintDL.Setup(p => p.PrintTicketResult(It.IsAny<List<TicketResult>>())).Verifiable();
            _mockprintDL.Setup(p => p.PrintOrganizationResult(It.IsAny<List<OrganizationResult>>())).Verifiable();
        }

        [Fact]
        public void ShouldCallLoadUser(){         
            ISearchService searchService = new SearchService(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                                             _mockuserMapper.Object, _mockticketMapper.Object, _mockorganizationMapper.Object,
                                                             _mocksearchUserWithRelatedEntities.Object, _mocksearchTicketWithRelatedEntities.Object,
                                                             _mocksearchOrganizationWithRelatedEnties.Object, _mockprintDL.Object);
            searchService.WildcardSearch(It.IsAny<string>());
            _mockuserMapper.Verify(u => u.Load(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void ShouldCallLoadTicket()
        {
            ISearchService searchService = new SearchService(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                                             _mockuserMapper.Object, _mockticketMapper.Object, _mockorganizationMapper.Object,
                                                             _mocksearchUserWithRelatedEntities.Object, _mocksearchTicketWithRelatedEntities.Object,
                                                             _mocksearchOrganizationWithRelatedEnties.Object, _mockprintDL.Object);
            searchService.WildcardSearch(It.IsAny<string>());
            _mockticketMapper.Verify(t => t.Load(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void ShouldCallLoadOrganization()
        {
            ISearchService searchService = new SearchService(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                                             _mockuserMapper.Object, _mockticketMapper.Object, _mockorganizationMapper.Object,
                                                             _mocksearchUserWithRelatedEntities.Object, _mocksearchTicketWithRelatedEntities.Object,
                                                             _mocksearchOrganizationWithRelatedEnties.Object, _mockprintDL.Object);
            searchService.WildcardSearch(It.IsAny<string>());
            _mockorganizationMapper.Verify(o => o.Load(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void ShouldCallSearchOrganization()
        {
            ISearchService searchService = new SearchService(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                                             _mockuserMapper.Object, _mockticketMapper.Object, _mockorganizationMapper.Object,
                                                             _mocksearchUserWithRelatedEntities.Object, _mocksearchTicketWithRelatedEntities.Object,
                                                             _mocksearchOrganizationWithRelatedEnties.Object, _mockprintDL.Object);
            searchService.WildcardSearch(It.IsAny<string>());
            _mocksearchOrganizationWithRelatedEnties.Verify(o => o.WildcardSearchOrganizationWithRelatedEntities(It.IsAny<string>(), It.IsAny<List<User>>(), It.IsAny<List<Ticket>>()), Times.Once);
        }

        [Fact]
        public void ShouldCallSearchTicket()
        {
            ISearchService searchService = new SearchService(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                                             _mockuserMapper.Object, _mockticketMapper.Object, _mockorganizationMapper.Object,
                                                             _mocksearchUserWithRelatedEntities.Object, _mocksearchTicketWithRelatedEntities.Object,
                                                             _mocksearchOrganizationWithRelatedEnties.Object, _mockprintDL.Object);
            searchService.WildcardSearch(It.IsAny<string>());
            _mocksearchTicketWithRelatedEntities.Verify(o => o.WildcardSearchTicketWithRelatedEntities(It.IsAny<string>(), It.IsAny<List<User>>(), It.IsAny<List<Organization>>()), Times.Once);
        }

        [Fact]
        public void ShouldCallSearchUser()
        {
            ISearchService searchService = new SearchService(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                                             _mockuserMapper.Object, _mockticketMapper.Object, _mockorganizationMapper.Object,
                                                             _mocksearchUserWithRelatedEntities.Object, _mocksearchTicketWithRelatedEntities.Object,
                                                             _mocksearchOrganizationWithRelatedEnties.Object, _mockprintDL.Object);
            searchService.WildcardSearch(It.IsAny<string>());
            _mocksearchUserWithRelatedEntities.Verify(o => o.WildcardSearchUserWithRelatedEntities(It.IsAny<string>(), It.IsAny<List<Organization>>(), It.IsAny<List<Ticket>>()), Times.Once);
        }

        [Fact]
        public void ShouldCallPrintOrganizationResult()
        {
            ISearchService searchService = new SearchService(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                                             _mockuserMapper.Object, _mockticketMapper.Object, _mockorganizationMapper.Object,
                                                             _mocksearchUserWithRelatedEntities.Object, _mocksearchTicketWithRelatedEntities.Object,
                                                             _mocksearchOrganizationWithRelatedEnties.Object, _mockprintDL.Object);
            searchService.WildcardSearch(It.IsAny<string>());
            _mockprintDL.Verify(po => po.PrintOrganizationResult(It.IsAny<List<OrganizationResult>>()), Times.Once);
        }

        [Fact]
        public void ShouldCallPrintUserResult()
        {
            ISearchService searchService = new SearchService(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                                             _mockuserMapper.Object, _mockticketMapper.Object, _mockorganizationMapper.Object,
                                                             _mocksearchUserWithRelatedEntities.Object, _mocksearchTicketWithRelatedEntities.Object,
                                                             _mocksearchOrganizationWithRelatedEnties.Object, _mockprintDL.Object);
            searchService.WildcardSearch(It.IsAny<string>());
            _mockprintDL.Verify(pu => pu.PrintUserResult(It.IsAny<List<UserResult>>()), Times.Once);
        }

        [Fact]
        public void ShouldCallPrintTicketResult()
        {
            ISearchService searchService = new SearchService(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                                             _mockuserMapper.Object, _mockticketMapper.Object, _mockorganizationMapper.Object,
                                                             _mocksearchUserWithRelatedEntities.Object, _mocksearchTicketWithRelatedEntities.Object,
                                                             _mocksearchOrganizationWithRelatedEnties.Object, _mockprintDL.Object);
            searchService.WildcardSearch(It.IsAny<string>());
            _mockprintDL.Verify(pt => pt.PrintTicketResult(It.IsAny<List<TicketResult>>()), Times.Once);
        }

    }
}
