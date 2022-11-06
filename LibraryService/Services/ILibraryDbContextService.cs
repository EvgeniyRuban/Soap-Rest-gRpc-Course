using System.Collections.Generic;

namespace LibraryService
{
    public interface ILibraryDbContextService
    {
        IList<Book> Books { get; }
    }
}
