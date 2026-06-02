namespace LibraryApp;

public class Library
{
    private List<Book> books = new();
    private List<Reader> readers = new();

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public void AddReader(Reader reader)
    {
        readers.Add(reader);
    }

    public Book? FindBook(int id)
    {
        return books.FirstOrDefault(b => b.Id == id);
    }

    public Reader? FindReader(int id)
    {
        return readers.FirstOrDefault(r => r.Id == id);
    }

    public BookIssue IssueBook(int bookId, int readerId)
    {
        Book book = FindBook(bookId);
        Reader reader = FindReader(readerId);

        if (book == null)
            throw new Exception("Книга не найдена");

        if (reader == null)
            throw new Exception("Читатель не найден");

        if (!book.IsAvailable)
            throw new Exception("Книга уже выдана");

        return new BookIssue(book, reader);
    }

    public BookReturn ReturnBook(int bookId, int readerId)
    {
        Book book = FindBook(bookId);
        Reader reader = FindReader(readerId);

        if (book == null)
            throw new Exception("Книга не найдена");

        if (reader == null)
            throw new Exception("Читатель не найден");

        return new BookReturn(book, reader);
    }
}