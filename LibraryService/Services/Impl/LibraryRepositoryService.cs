using System.Collections.Generic;
using System.Linq;

namespace LibraryService
{
    public class LibraryRepositoryService : ILibraryRepositoryService
    {
        private readonly ILibraryDbContextService _dbContext;


        public LibraryRepositoryService(ILibraryDbContextService dbContext) => _dbContext = dbContext;


        public IList<Book> GetByAuthor(string authorName)
        {
            try
            {
                return _dbContext.Books.Where(b =>
                                              b.Authors.Where(a =>
                                                              a.Name.ToLower().Contains(authorName.ToLower())).Count() > 0).ToList();
            }
            catch
            {
                return new List<Book>();
            }
        }

        public IList<Book> GetByCategory(string category)
        {
            try
            {
                return _dbContext.Books.Where(b => b.Category.ToLower().Contains(category.ToLower())).ToList();
            }
            catch
            {
                return new List<Book>();
            }
        }

        public IList<Book> GetByTitle(string title)
        {
            try
            {
                return _dbContext.Books.Where(b => b.Title.ToLower().Contains(title.ToLower())).ToList();
            }
            catch
            {
                return new List<Book>();
            }
        }

        public string Add(Book model)
        {
            throw new System.NotImplementedException();
        }

        public string Delete(string id)
        {
            throw new System.NotImplementedException();
        }

        public Book Get(string id)
        {
            throw new System.NotImplementedException();
        }

        public IList<Book> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public string Update(Book model)
        {
            throw new System.NotImplementedException();
        }
    }
}