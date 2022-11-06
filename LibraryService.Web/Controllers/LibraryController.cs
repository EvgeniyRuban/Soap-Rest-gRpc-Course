using LibraryService.Web.Models;
using LibraryServiceReference;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.Web.Controllers;

public class LibraryController : Controller
{
    private readonly ILogger _logger;


    public LibraryController(ILogger<LibraryController> logger)
    {
        _logger = logger;
    }


    public IActionResult Index(string searchString, SearchType searchType)
    {
        var librarySoapClient = new LibraryWebServiceSoapClient(LibraryWebServiceSoapClient.EndpointConfiguration.LibraryWebServiceSoap);

        try
        {
            if (string.IsNullOrEmpty(searchString))
            {
                throw new ArgumentNullException(searchString);
            }
            else if(searchString.Length < 3)
            {
                throw new ArgumentException("Min search string length, must be greater then 2", searchString);
            }

            return View(new BookViewModel()
            {
                Books = searchType switch
                {
                    SearchType.Title => librarySoapClient.GetBooksByTitle(searchString),
                    SearchType.Category => librarySoapClient.GetBooksByCategory(searchString),
                    SearchType.Author => librarySoapClient.GetBooksByAuthor(searchString),
                }
            });
        }
        catch(Exception ex) 
        {
            _logger.LogError(ex.Message, ex);
        }

        return View(new BookViewModel() { Books = new Book[0] });
    }
}