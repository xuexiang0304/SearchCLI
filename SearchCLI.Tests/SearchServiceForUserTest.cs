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
    public class SearchServiceForUserTest
    {
        Mock<ITicketMapper> _mockticketMapper;
        Mock<IOrganizationMapper> _mockorganizationMapper;
        Mock<ISearchUserWithRelatedEntities> _mocksearchUserWithRelatedEntities;
        Mock<IPrintDL> _mockprintDL;

        public SearchServiceForUserTest()
        {
            mock();
        }

        private void mock()
        {
            _mockticketMapper = new Mock<ITicketMapper>();
            _mockticketMapper.Setup(tm => tm.Load(It.IsAny<string>())).Verifiable();
            _mockorganizationMapper = new Mock<IOrganizationMapper>();
            _mockorganizationMapper.Setup(om => om.Load(It.IsAny<string>())).Verifiable();
            _mocksearchUserWithRelatedEntities = new Mock<ISearchUserWithRelatedEntities>();
            _mocksearchUserWithRelatedEntities.Setup(su => su.WildcardSearchUserWithRelatedEntities(It.IsAny<string>(),
                                                                                               It.IsAny<List<Organization>>(),
                                                                                               It.IsAny<List<Ticket>>())).Verifiable();
            _mockprintDL = new Mock<IPrintDL>();
            _mockprintDL.Setup(p => p.PrintUserResult(It.IsAny<List<UserResult>>())).Verifiable();
        }

        [Fact]
        public void ShouldCallLoadTicket()
        {
            ISearchService searchService =
                new SearchServiceForUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                         _mockticketMapper.Object, _mockorganizationMapper.Object,
                                         _mocksearchUserWithRelatedEntities.Object, _mockprintDL.Object);
            searchService.WildcardSearch(It.IsAny<string>());
            _mockticketMapper.Verify(t => t.Load(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void ShouldCallLoadOrganization()
        {
            ISearchService searchService =
                new SearchServiceForUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                         _mockticketMapper.Object, _mockorganizationMapper.Object,
                                         _mocksearchUserWithRelatedEntities.Object, _mockprintDL.Object);
            searchService.WildcardSearch(It.IsAny<string>());
            _mockorganizationMapper.Verify(o => o.Load(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void ShouldCallSearchUser()
        {
            ISearchService searchService =
                new SearchServiceForUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                         _mockticketMapper.Object, _mockorganizationMapper.Object,
                                         _mocksearchUserWithRelatedEntities.Object, _mockprintDL.Object);
            searchService.WildcardSearch(It.IsAny<string>());
            _mocksearchUserWithRelatedEntities.Verify(o => o.WildcardSearchUserWithRelatedEntities(It.IsAny<string>(), It.IsAny<List<Organization>>(), It.IsAny<List<Ticket>>()), Times.Once);
        }

        [Fact]
        public void ShouldCallPrintUserResult()
        {
            ISearchService searchService =
                new SearchServiceForUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                                         _mockticketMapper.Object, _mockorganizationMapper.Object,
                                         _mocksearchUserWithRelatedEntities.Object, _mockprintDL.Object);
            searchService.WildcardSearch(It.IsAny<string>());
            _mockprintDL.Verify(pu => pu.PrintUserResult(It.IsAny<List<UserResult>>()), Times.Once);
        }
    }
}
