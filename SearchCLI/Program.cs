using System;
using SearchCLI.IServiceLayer;
using SearchCLI.ServiceLayer;

namespace SearchCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //ISearchService service = new SearchService();

            //service.WildcardSearch("Artisan");
            try
            {
                while (true)
                {
                    Console.WriteLine("Please select from the following options by type the number:");
                    Console.WriteLine("1. Search through Users, Tickets and Orgnizations and display related entities.");
                    Console.WriteLine("2. Search through Users and display related entities.");
                    Console.WriteLine("3. Search through Organizations and display related entities");
                    Console.WriteLine("4. Search through Tickets and display related entities");
                    Console.WriteLine("5. Exit");
                    Console.WriteLine();

                    string line = Console.ReadLine();

                    if (line == "1")
                    {
                        Console.WriteLine("Please enter your search value");
                        string searchStr = Console.ReadLine();
                        ISearchService searchService = new SearchService();
                        searchService.WildcardSearch(searchStr);
                    }
                    else if (line == "2"){
                        Console.WriteLine("Please enter your search value");
                        string searchStr = Console.ReadLine();
                        ISearchService searchService = new SearchServiceForUser();
                        searchService.WildcardSearch(searchStr);
                    }else if(line == "3"){
                        Console.WriteLine("Please enter your search value");
                        string searchStr = Console.ReadLine();
                        ISearchService searchService = new SearchServiceForOrganization();
                        searchService.WildcardSearch(searchStr);
                    }else if(line == "4"){
                        Console.WriteLine("Please enter your search value");
                        string searchStr = Console.ReadLine();
                        ISearchService searchService = new SearchServiceForTicket();
                        searchService.WildcardSearch(searchStr);
                    }else if(line == "5"){
                        break;
                    }
                    else{
                        Console.WriteLine("Please enter a valid option number");
                    }
                }
                   
            }catch(Exception ex){
                Console.WriteLine("Something went wrong: {0}", ex.Message);
            }
        }
    }
}
