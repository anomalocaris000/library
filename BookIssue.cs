namespace LibraryApp;

public class BookIssue
{
    public Book Book { get; set; }
    public Reader Reader { get; set; }
    public DateTime IssueDate { get; set; }

    public BookIssue(Book book, Reader reader)
    {
        Book = book;
        Reader = reader;
        IssueDate = DateTime.Now;

        Book.IsAvailable = false;
    }

    public string GetInfo()
    {
        return $"Книга '{Book.Title}' выдана читателю {Reader.FullName} {IssueDate:d}";
    }
}