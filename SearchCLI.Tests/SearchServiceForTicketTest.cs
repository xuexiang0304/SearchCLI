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
    public class SearchServiceForTicketTest
    {
        Mock<IUserMapper> _mockuserMapper;
        Mock<IOrganizationMapper> _mockorganizationMapper;
        Mock<ISearchTicketWithRelatedEntities> _mocksearchTicketWithRelatedEntities;
        Mock<IPrintDL> _mockprintDL;

        public SearchServiceForTicketTest()
        {
            mock();
        }

        private void mock()
        {
            _mockuserMapper = new Mock<IUserMapper>();
            _mockuserMapper.Setup(um => um.Load(It.IsAny<string>())).Verifiable();
            _mockorganizationMapper = new Mock<IOrganizationMapper>();
            _mockorganizationMapper.Setup(om => om.Load(It.IsAny<string>())).Verifiable();
            _mocksearchTicketWithRelatedEntities = new Mock<ISearchTicketWithRelatedEntities>();
            _mocksearchTicketWithRelatedEntities.Setup(st => st.WildcardSearchTicketWithRelatedEntities(It.IsAny<string>(),
                                                                                                   It.IsAny<List<User>>(),
                                                                                                   It.IsAny<List<Organization>>())).Verifiable();
            _mockprintDL = new Mock<IPrintDL>();
            _mockprintDL.Setup(p => p.PrintTicketResult(It.IsAny<List<TicketResult>>())).Verifiable();
        }

        [Fact]
        public void ShouldCallLoadUser()
        {
            ISearchService searchService = 
                new SearchServiceForTicket(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                             _mockuserMapper.Object, _mockorganizationMapper.Object,
                                             _mocksearchTicketWithRelatedEntities.Object,
                                             _mockprintDL.Object);
            searchService.WildcardSearch(It.IsAny<string>());
            _mockuserMapper.Verify(u => u.Load(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void ShouldCallLoadOrganization()
        {
            ISearchService searchService =
                new SearchServiceForTicket(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                             _mockuserMapper.Object, _mockorganizationMapper.Object,
                                             _mocksearchTicketWithRelatedEntities.Object,
                                             _mockprintDL.Object); 
            searchService.WildcardSearch(It.IsAny<string>());
            _mockorganizationMapper.Verify(o => o.Load(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void ShouldCallSearchTicket()
        {
            ISearchService searchService =
              new SearchServiceForTicket(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                           _mockuserMapper.Object, _mockorganizationMapper.Object,
                                           _mocksearchTicketWithRelatedEntities.Object,
                                           _mockprintDL.Object); 
            searchService.WildcardSearch(It.IsAny<string>());
            _mocksearchTicketWithRelatedEntities.Verify(o => o.WildcardSearchTicketWithRelatedEntities(It.IsAny<string>(), It.IsAny<List<User>>(), It.IsAny<List<Organization>>()), Times.Once);
        }

        [Fact]
        public void ShouldCallPrintTicketResult()
        {
            ISearchService searchService =
                new SearchServiceForTicket(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                             _mockuserMapper.Object, _mockorganizationMapper.Object,
                                             _mocksearchTicketWithRelatedEntities.Object,
                                             _mockprintDL.Object); 
            searchService.WildcardSearch(It.IsAny<string>());
            _mockprintDL.Verify(pt => pt.PrintTicketResult(It.IsAny<List<TicketResult>>()), Times.Once);
        }

    }
}
