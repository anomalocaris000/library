namespace LibraryApp;

using System.Text;
using System.Text.Json;

public class FileStorage
{
    private string dataDir = "data";
    private string booksFile = "books.txt";
    private string readersFile = "readers.txt";
    private string issuesFile = "issues.txt";
    private string returnsFile = "returns.txt";

    public FileStorage()
    {
        if (!Directory.Exists(dataDir))
            Directory.CreateDirectory(dataDir);
    }

    // === ТЕКСТОВЫЕ ФАЙЛЫ ===

    public void SaveBooksToText(List<Book> books)
    {
        var path = Path.Combine(dataDir, booksFile);
        var lines = new List<string>();

        foreach (var book in books)
        {
            lines.Add($"{book.Id}|{book.Title}|{book.Author}|{book.Year}|{book.IsAvailable}");
        }

        File.WriteAllLines(path, lines, Encoding.UTF8);
    }

    public List<Book> LoadBooksFromText()
    {
        var path = Path.Combine(dataDir, booksFile);
        var books = new List<Book>();

        if (!File.Exists(path))
            return books;

        var lines = File.ReadAllLines(path, Encoding.UTF8);

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = line.Split('|');
            if (parts.Length != 5)
                continue;

            var book = new Book(
                int.Parse(parts[0]),
                parts[1],
                parts[2],
                int.Parse(parts[3])
            )
            {
                IsAvailable = bool.Parse(parts[4])
            };

            books.Add(book);
        }

        return books;
    }

    public void SaveReadersToText(List<Reader> readers)
    {
        var path = Path.Combine(dataDir, readersFile);
        var lines = new List<string>();

        foreach (var reader in readers)
        {
            lines.Add($"{reader.Id}|{reader.FullName}");
        }

        File.WriteAllLines(path, lines, Encoding.UTF8);
    }

    public List<Reader> LoadReadersFromText()
    {
        var path = Path.Combine(dataDir, readersFile);
        var readers = new List<Reader>();

        if (!File.Exists(path))
            return readers;

        var lines = File.ReadAllLines(path, Encoding.UTF8);

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = line.Split('|');
            if (parts.Length != 2)
                continue;

            var reader = new Reader(int.Parse(parts[0]), parts[1]);
            readers.Add(reader);
        }

        return readers;
    }

    public void SaveIssuesToText(List<(int BookId, int ReaderId, DateTime Date)> issues)
    {
        var path = Path.Combine(dataDir, issuesFile);
        var lines = new List<string>();

        foreach (var issue in issues)
        {
            lines.Add($"{issue.BookId}|{issue.ReaderId}|{issue.Date:yyyy-MM-dd HH:mm:ss}");
        }

        File.WriteAllLines(path, lines, Encoding.UTF8);
    }

    public List<(int BookId, int ReaderId, DateTime Date)> LoadIssuesFromText()
    {
        var path = Path.Combine(dataDir, issuesFile);
        var issues = new List<(int, int, DateTime)>();

        if (!File.Exists(path))
            return issues;

        var lines = File.ReadAllLines(path, Encoding.UTF8);

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = line.Split('|');
            if (parts.Length != 3)
                continue;

            var bookId = int.Parse(parts[0]);
            var readerId = int.Parse(parts[1]);
            var date = DateTime.ParseExact(parts[2], "yyyy-MM-dd HH:mm:ss", null);

            issues.Add((bookId, readerId, date));
        }

        return issues;
    }

    public void SaveReturnsToText(List<(int BookId, int ReaderId, DateTime Date)> returns)
    {
        var path = Path.Combine(dataDir, returnsFile);
        var lines = new List<string>();

        foreach (var ret in returns)
        {
            lines.Add($"{ret.BookId}|{ret.ReaderId}|{ret.Date:yyyy-MM-dd HH:mm:ss}");
        }

        File.WriteAllLines(path, lines, Encoding.UTF8);
    }

    public List<(int BookId, int ReaderId, DateTime Date)> LoadReturnsFromText()
    {
        var path = Path.Combine(dataDir, returnsFile);
        var returns = new List<(int, int, DateTime)>();

        if (!File.Exists(path))
            return returns;

        var lines = File.ReadAllLines(path, Encoding.UTF8);

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = line.Split('|');
            if (parts.Length != 3)
                continue;

            var bookId = int.Parse(parts[0]);
            var readerId = int.Parse(parts[1]);
            var date = DateTime.ParseExact(parts[2], "yyyy-MM-dd HH:mm:ss", null);

            returns.Add((bookId, readerId, date));
        }

        return returns;
    }

    // === JSON ФАЙЛЫ ===

    public void SaveBooksToJson(List<Book> books)
    {
        var path = Path.Combine(dataDir, "books.json");
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(books, options);
        File.WriteAllText(path, json, Encoding.UTF8);
    }

    public List<Book> LoadBooksFromJson()
    {
        var path = Path.Combine(dataDir, "books.json");

        if (!File.Exists(path))
            return new List<Book>();

        var json = File.ReadAllText(path, Encoding.UTF8);
        return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
    }

    public void SaveReadersToJson(List<Reader> readers)
    {
        var path = Path.Combine(dataDir, "readers.json");
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(readers, options);
        File.WriteAllText(path, json, Encoding.UTF8);
    }

    public List<Reader> LoadReadersFromJson()
    {
        var path = Path.Combine(dataDir, "readers.json");

        if (!File.Exists(path))
            return new List<Reader>();

        var json = File.ReadAllText(path, Encoding.UTF8);
        return JsonSerializer.Deserialize<List<Reader>>(json) ?? new List<Reader>();
    }

    public void SaveIssuesToJson(List<(int BookId, int ReaderId, DateTime Date)> issues)
    {
        var path = Path.Combine(dataDir, "issues.json");
        var issueData = issues.Select(i => new { BookId = i.BookId, ReaderId = i.ReaderId, IssueDate = i.Date }).ToList();
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(issueData, options);
        File.WriteAllText(path, json, Encoding.UTF8);
    }

    public List<(int BookId, int ReaderId, DateTime Date)> LoadIssuesFromJson()
    {
        var path = Path.Combine(dataDir, "issues.json");
        var issues = new List<(int, int, DateTime)>();

        if (!File.Exists(path))
            return issues;

        var json = File.ReadAllText(path, Encoding.UTF8);
        var issueData = JsonSerializer.Deserialize<List<JsonElement>>(json);

        if (issueData == null)
            return issues;

        foreach (var item in issueData)
        {
            var bookId = item.GetProperty("BookId").GetInt32();
            var readerId = item.GetProperty("ReaderId").GetInt32();
            var date = item.GetProperty("IssueDate").GetDateTime();

            issues.Add((bookId, readerId, date));
        }

        return issues;
    }

    public void SaveReturnsToJson(List<(int BookId, int ReaderId, DateTime Date)> returns)
    {
        var path = Path.Combine(dataDir, "returns.json");
        var returnData = returns.Select(r => new { BookId = r.BookId, ReaderId = r.ReaderId, ReturnDate = r.Date }).ToList();
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(returnData, options);
        File.WriteAllText(path, json, Encoding.UTF8);
    }

    public List<(int BookId, int ReaderId, DateTime Date)> LoadReturnsFromJson()
    {
        var path = Path.Combine(dataDir, "returns.json");
        var returns = new List<(int, int, DateTime)>();

        if (!File.Exists(path))
            return returns;

        var json = File.ReadAllText(path, Encoding.UTF8);
        var returnData = JsonSerializer.Deserialize<List<JsonElement>>(json);

        if (returnData == null)
            return returns;

        foreach (var item in returnData)
        {
            var bookId = item.GetProperty("BookId").GetInt32();
            var readerId = item.GetProperty("ReaderId").GetInt32();
            var date = item.GetProperty("ReturnDate").GetDateTime();

            returns.Add((bookId, readerId, date));
        }

        return returns;
    }
}
