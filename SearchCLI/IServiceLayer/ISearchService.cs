using System;
namespace SearchCLI.IServiceLayer
{
    public interface ISearchService
    {
        void WildcardSearch(string searchStr);
    }
}
