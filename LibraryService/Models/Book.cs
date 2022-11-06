namespace LibraryService
{
    public class Book
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Lang { get; set; }
        public int Pages { get; set; }
        public int AgeLimit { get; set; }
        public int PublicationDate { get; set; }
        public Author[] Authors { get; set; }

        public override string ToString() => $"Id: {Id}\n" +
                                             $"Title: {Title}\n" +
                                             $"Category: {Category}\n" +
                                             $"Lang: {Lang}\n" +
                                             $"Pages: {Pages}\n" +
                                             $"AgeLimit: {AgeLimit}\n" +
                                             $"PublicationDate: {PublicationDate}\n" +
                                             $"{string.Join<Author>(string.Empty, Authors)}\n";
    }
}