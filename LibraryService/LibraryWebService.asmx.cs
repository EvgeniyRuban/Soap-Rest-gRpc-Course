using System.Linq;
using System.Web.Services;

namespace LibraryService
{
    /// <summary>
    /// Summary description for LibraryWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class LibraryWebService : System.Web.Services.WebService
    {
        private readonly ILibraryRepositoryService _libraryRepositoryService;


        public LibraryWebService()
        {
            _libraryRepositoryService = new LibraryRepositoryService(new LibraryDbContextService());
        }


        [WebMethod]
        public Book[] GetBooksByTitle(string title) => _libraryRepositoryService.GetByTitle(title).ToArray();

        [WebMethod]
        public Book[] GetBooksByCategory(string category) => _libraryRepositoryService.GetByCategory(category).ToArray();

        [WebMethod]
        public Book[] GetBooksByAuthor(string author) => _libraryRepositoryService.GetByAuthor(author).ToArray();
    }
}