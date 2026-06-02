namespace LibraryApp;

public class BookReturn
{
    public Book Book { get; set; }
    public Reader Reader { get; set; }
    public DateTime ReturnDate { get; set; }

    public BookReturn(Book book, Reader reader)
    {
        Book = book;
        Reader = reader;
        ReturnDate = DateTime.Now;

        Book.IsAvailable = true;
    }

    public string GetInfo()
    {
        return $"Книга '{Book.Title}' возвращена читателем {Reader.FullName} {ReturnDate:d}";
    }
}