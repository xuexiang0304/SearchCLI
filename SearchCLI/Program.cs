using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using SearchCLI.IServiceLayer;
using SearchCLI.ServiceLayer;

namespace SearchCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            string basePath = ".";
            if (!Debugger.IsAttached)
                basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string userFilePath = basePath + @"/Data/users.json";
            string ticketFilePath = basePath + @"/Data/tickets.json";
            string organizationFilePath = basePath + @"/Data/organizations.json";

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
                        ISearchService searchService = new SearchService(userFilePath, ticketFilePath, organizationFilePath);
                        searchService.WildcardSearch(searchStr);
                    }
                    else if (line == "2"){
                        Console.WriteLine("Please enter your search value");
                        string searchStr = Console.ReadLine();
                        ISearchService searchService = new SearchServiceForUser(userFilePath, ticketFilePath, organizationFilePath);
                        searchService.WildcardSearch(searchStr);
                    }else if(line == "3"){
                        Console.WriteLine("Please enter your search value");
                        string searchStr = Console.ReadLine();
                        ISearchService searchService = new SearchServiceForOrganization(userFilePath, ticketFilePath, organizationFilePath);
                        searchService.WildcardSearch(searchStr);
                    }else if(line == "4"){
                        Console.WriteLine("Please enter your search value");
                        string searchStr = Console.ReadLine();
                        ISearchService searchService = new SearchServiceForTicket(userFilePath, ticketFilePath, organizationFilePath);
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
