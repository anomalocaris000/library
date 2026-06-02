namespace LibraryApp;

public class Library
{
    private List<Book> books = new();
    private List<Reader> readers = new();
    private List<(int BookId, int ReaderId, DateTime Date)> issues = new();
    private List<(int BookId, int ReaderId, DateTime Date)> returns = new();

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

        var issue = new BookIssue(book, reader);
        issues.Add((bookId, readerId, issue.IssueDate));
        return issue;
    }

    public BookReturn ReturnBook(int bookId, int readerId)
    {
        Book book = FindBook(bookId);
        Reader reader = FindReader(readerId);

        if (book == null)
            throw new Exception("Книга не найдена");

        if (reader == null)
            throw new Exception("Читатель не найден");

        var ret = new BookReturn(book, reader);
        returns.Add((bookId, readerId, ret.ReturnDate));
        return ret;
    }

    public List<Book> GetBooks() => books;
    public List<Reader> GetReaders() => readers;
    public List<(int BookId, int ReaderId, DateTime Date)> GetIssues() => issues;
    public List<(int BookId, int ReaderId, DateTime Date)> GetReturns() => returns;

    public void ClearData()
    {
        books.Clear();
        readers.Clear();
        issues.Clear();
        returns.Clear();
    }
}
