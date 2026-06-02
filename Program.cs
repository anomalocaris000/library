using System;
using LibraryApp;

Library library = new Library();

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

Console.WriteLine("=== ИНФОРМАЦИЯ О КНИГЕ ===");
Console.WriteLine(book1.GetInfo());

Console.WriteLine();

Console.WriteLine("=== ИНФОРМАЦИЯ О ЧИТАТЕЛЕ ===");
Console.WriteLine(reader1.GetInfo());

Console.WriteLine();

Console.WriteLine("=== ВЫДАЧА КНИГИ ===");
BookIssue issue = library.IssueBook(1, 1);
Console.WriteLine(issue.GetInfo());

Console.WriteLine();

Console.WriteLine("=== СОСТОЯНИЕ КНИГИ ===");
Console.WriteLine(book1.GetInfo());

Console.WriteLine();

Console.WriteLine("=== ВОЗВРАТ КНИГИ ===");
BookReturn returnedBook = library.ReturnBook(1, 1);
Console.WriteLine(returnedBook.GetInfo());

Console.WriteLine();

Console.WriteLine("=== СОСТОЯНИЕ КНИГИ ПОСЛЕ ВОЗВРАТА ===");
Console.WriteLine(book1.GetInfo());

Console.ReadKey();