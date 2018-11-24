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
    public class SearchServiceForOrganizationTest
    {
        Mock<IUserMapper> _mockuserMapper;
        Mock<ITicketMapper> _mockticketMapper;
        Mock<ISearchOrganizationWithRelatedEnties> _mocksearchOrganizationWithRelatedEnties;
        Mock<IPrintDL> _mockprintDL;
        public SearchServiceForOrganizationTest()
        {
            mock();
        }

        private void mock()
        {
            _mockuserMapper = new Mock<IUserMapper>();
            _mockuserMapper.Setup(um => um.Load(It.IsAny<string>())).Verifiable();
            _mockticketMapper = new Mock<ITicketMapper>();
            _mockticketMapper.Setup(tm => tm.Load(It.IsAny<string>())).Verifiable();
            _mocksearchOrganizationWithRelatedEnties = new Mock<ISearchOrganizationWithRelatedEnties>();
            _mocksearchOrganizationWithRelatedEnties.Setup(so => so.WildcardSearchOrganizationWithRelatedEntities(It.IsAny<string>(),
                                                                                                             It.IsAny<List<User>>(),
                                                                                                             It.IsAny<List<Ticket>>())).Verifiable();

            _mockprintDL = new Mock<IPrintDL>();
            _mockprintDL.Setup(p => p.PrintOrganizationResult(It.IsAny<List<OrganizationResult>>())).Verifiable();
        }

        [Fact]
        public void ShouldCallLoadUser()
        {
            ISearchService searchService = 
                new SearchServiceForOrganization(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                                 _mockuserMapper.Object, _mockticketMapper.Object,                                            
                                                 _mocksearchOrganizationWithRelatedEnties.Object, _mockprintDL.Object);
            searchService.WildcardSearch(It.IsAny<string>());
            _mockuserMapper.Verify(u => u.Load(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void ShouldCallLoadTicket()
        {
            ISearchService searchService =
                new SearchServiceForOrganization(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                                 _mockuserMapper.Object, _mockticketMapper.Object,
                                                 _mocksearchOrganizationWithRelatedEnties.Object, _mockprintDL.Object);
            searchService.WildcardSearch(It.IsAny<string>());
            _mockticketMapper.Verify(t => t.Load(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void ShouldCallSearchOrganization()
        {
            ISearchService searchService =
                new SearchServiceForOrganization(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                                 _mockuserMapper.Object, _mockticketMapper.Object,
                                                 _mocksearchOrganizationWithRelatedEnties.Object, _mockprintDL.Object);
            searchService.WildcardSearch(It.IsAny<string>());
            _mocksearchOrganizationWithRelatedEnties.Verify(o => o.WildcardSearchOrganizationWithRelatedEntities(It.IsAny<string>(), It.IsAny<List<User>>(), It.IsAny<List<Ticket>>()), Times.Once);
        }

        [Fact]
        public void ShouldCallPrintOrganizationResult()
        {
            ISearchService searchService =
                  new SearchServiceForOrganization(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                                   _mockuserMapper.Object, _mockticketMapper.Object,
                                                   _mocksearchOrganizationWithRelatedEnties.Object, _mockprintDL.Object);
            searchService.WildcardSearch(It.IsAny<string>());
            _mockprintDL.Verify(po => po.PrintOrganizationResult(It.IsAny<List<OrganizationResult>>()), Times.Once);
        }

    }
}
