using System.Collections.Generic;

namespace LibraryService
{
    public interface ILibraryRepositoryService : IRepository<Book, string>
    {
        IList<Book> GetByAuthor(string authorName);
        IList<Book> GetByCategory(string category);
        IList<Book> GetByTitle(string title);
    }
}
