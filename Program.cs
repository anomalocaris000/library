using System;
using LibraryApp;

// Инициализация файлового хранилища
FileStorage storage = new FileStorage();

// Загрузка данных из файлов при запуске программы
Console.WriteLine("=== ЗАГРУЗКА ДАННЫХ ===");
Library library = new Library();

var booksText = storage.LoadBooksFromText();
var readersText = storage.LoadReadersFromText();
var issuesText = storage.LoadIssuesFromText();
var returnsText = storage.LoadReturnsFromText();

foreach (var book in booksText)
    library.AddBook(book);

foreach (var reader in readersText)
    library.AddReader(reader);

Console.WriteLine($"Загружено книг (из txt): {booksText.Count}");
Console.WriteLine($"Загружено читателей (из txt): {readersText.Count}");
Console.WriteLine();

// Если данные не загружены, используем тестовые данные
if (library.GetBooks().Count == 0)
{
    Console.WriteLine("=== СОЗДАНИЕ ТЕСТОВЫХ ДАННЫХ ===");
    
    Book book1 = new Book(
        1,
        "Война и мир",
        "Л.Н. Толстой",
        1869);

    Book book2 = new Book(
        2,
        "Преступление и наказание",
        "Ф.М. Достоевский",
        1866);

    library.AddBook(book1);
    library.AddBook(book2);

    Reader reader1 = new Reader(
        1,
        "Иван Иванов");

    library.AddReader(reader1);
}

Console.WriteLine("=== ИНФОРМАЦИЯ О КНИГЕ ===");
var firstBook = library.GetBooks().FirstOrDefault();
if (firstBook != null)
    Console.WriteLine(firstBook.GetInfo());

Console.WriteLine();

Console.WriteLine("=== ИНФОРМАЦИЯ О ЧИТАТЕЛЕ ===");
var firstReader = library.GetReaders().FirstOrDefault();
if (firstReader != null)
    Console.WriteLine(firstReader.GetInfo());

Console.WriteLine();

Console.WriteLine("=== ВЫДАЧА КНИГИ ===");
if (firstBook != null && firstReader != null)
{
    BookIssue issue = library.IssueBook(firstBook.Id, firstReader.Id);
    Console.WriteLine(issue.GetInfo());
}

Console.WriteLine();

Console.WriteLine("=== СОСТОЯНИЕ КНИГИ ===");
if (firstBook != null)
    Console.WriteLine(firstBook.GetInfo());

Console.WriteLine();

Console.WriteLine("=== ВОЗВРАТ КНИГИ ===");
if (firstBook != null && firstReader != null)
{
    BookReturn returnedBook = library.ReturnBook(firstBook.Id, firstReader.Id);
    Console.WriteLine(returnedBook.GetInfo());
}

Console.WriteLine();

Console.WriteLine("=== СОСТОЯНИЕ КНИГИ ПОСЛЕ ВОЗВРАТА ===");
if (firstBook != null)
    Console.WriteLine(firstBook.GetInfo());

Console.WriteLine();

// Сохранение данных в текстовые файлы при выходе
Console.WriteLine("=== СОХРАНЕНИЕ ДАННЫХ В ТЕКСТОВЫЕ ФАЙЛЫ ===");
storage.SaveBooksToText(library.GetBooks());
storage.SaveReadersToText(library.GetReaders());
storage.SaveIssuesToText(library.GetIssues());
storage.SaveReturnsToText(library.GetReturns());
Console.WriteLine("Данные сохранены в текстовые файлы");

Console.WriteLine();

// Сохранение данных в JSON файлы
Console.WriteLine("=== СОХРАНЕНИЕ ДАННЫХ В JSON ФАЙЛЫ ===");
storage.SaveBooksToJson(library.GetBooks());
storage.SaveReadersToJson(library.GetReaders());
storage.SaveIssuesToJson(library.GetIssues());
storage.SaveReturnsToJson(library.GetReturns());
Console.WriteLine("Данные сохранены в JSON файлы");

Console.WriteLine();
Console.WriteLine("Нажмите любую клавишу для выхода...");
Console.ReadKey();
