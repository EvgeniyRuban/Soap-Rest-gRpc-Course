using LibraryService.Properties;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace LibraryService
{
    public class LibraryDbContextService : ILibraryDbContextService
    {
        private IList<Book> _library;


        public LibraryDbContextService() => Initialize();


        public IList<Book> Books => _library;

        private void Initialize() => _library = JsonConvert.DeserializeObject<List<Book>>(Encoding.UTF8.GetString(Resources.books));
    }
}