using Library.Common.Extensions;
using Library.Common.Models;
using Library.Common.Services;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var bookService = new CrudService<Book>();

bookService.Create(new Book(
    "Кобзар",
    1840,
    "Тарас Шевченко",
    320,
    "978-966-03-1234-5"));

bookService.Create(new Book(
    "Тигролови",
    1944,
    "Іван Багряний",
    280,
    "978-966-03-5678-9"));

bookService.ItemChanged += message =>
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\n[ПОДІЯ] {message}");
    Console.ResetColor();
};

bool isRunning = true;

while (isRunning)
{
    ShowMenu();

    Console.Write("Оберіть пункт меню: ");
    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            ShowAllBooks(bookService);
            break;

        case "2":
            AddBook(bookService);
            break;

        case "3":
            FindBook(bookService);
            break;

        case "4":
            UpdateBook(bookService);
            break;

        case "5":
            RemoveBook(bookService);
            break;

        case "6":
            BorrowBook(bookService);
            break;

        case "7":
            ShowAvailableBooks(bookService);
            break;

        case "8":
            ShowStatistics();
            break;

        case "0":
            isRunning = false;
            Console.WriteLine("\nПрограму завершено.");
            break;

        default:
            Console.WriteLine("\nНевірний пункт меню.");
            break;
    }

    if (isRunning)
    {
        Console.WriteLine("\nНатисніть Enter, щоб продовжити...");
        Console.ReadLine();
        Console.Clear();
    }
}

static void ShowMenu()
{
    Console.WriteLine("======================================");
    Console.WriteLine("    СИСТЕМА КЕРУВАННЯ БІБЛІОТЕКОЮ");
    Console.WriteLine("======================================");
    Console.WriteLine("1. Показати всі книги");
    Console.WriteLine("2. Додати книгу");
    Console.WriteLine("3. Знайти книгу за Id");
    Console.WriteLine("4. Оновити книгу");
    Console.WriteLine("5. Видалити книгу");
    Console.WriteLine("6. Позичити книгу");
    Console.WriteLine("7. Показати доступні книги");
    Console.WriteLine("8. Статистика бібліотеки");
    Console.WriteLine("0. Вийти");
    Console.WriteLine("======================================");
}

static void ShowAllBooks(CrudService<Book> bookService)
{
    Console.WriteLine("\n========== УСІ КНИГИ ==========");

    var books = bookService.ReadAll().ToList();

    if (books.Count == 0)
    {
        Console.WriteLine("У бібліотеці немає книг.");
        return;
    }

    foreach (var book in books)
    {
        PrintBook(book);
    }
}

static void AddBook(CrudService<Book> bookService)
{
    Console.WriteLine("\n========== ДОДАВАННЯ КНИГИ ==========");

    Console.Write("Введіть назву книги: ");
    string title = Console.ReadLine() ?? "Без назви";

    Console.Write("Введіть рік видання: ");
    int publicationYear = ReadPositiveNumber();

    Console.Write("Введіть автора: ");
    string authorName = Console.ReadLine() ?? "Невідомий автор";

    Console.Write("Введіть кількість сторінок: ");
    int pageCount = ReadPositiveNumber();

    Console.Write("Введіть ISBN: ");
    string isbn = Console.ReadLine() ?? "Не вказано";

    var book = new Book(
        title,
        publicationYear,
        authorName,
        pageCount,
        isbn);

    bookService.Create(book);

    Console.WriteLine($"Книгу успішно додано.");
    Console.WriteLine($"Id книги: {book.Id}");
}

static void FindBook(CrudService<Book> bookService)
{
    Console.WriteLine("\n========== ПОШУК КНИГИ ==========");

    Console.Write("Введіть Id книги: ");
    string? idText = Console.ReadLine();

    if (!Guid.TryParse(idText, out Guid id))
    {
        Console.WriteLine("Помилка: некоректний формат Id.");
        return;
    }

    Book? book = bookService.Read(id);

    if (book is null)
    {
        Console.WriteLine("Книгу не знайдено.");
        return;
    }

    PrintBook(book);
}

static void UpdateBook(CrudService<Book> bookService)
{
    Console.WriteLine("\n========== ОНОВЛЕННЯ КНИГИ ==========");

    Console.Write("Введіть Id книги: ");
    string? idText = Console.ReadLine();

    if (!Guid.TryParse(idText, out Guid id))
    {
        Console.WriteLine("Помилка: некоректний формат Id.");
        return;
    }

    Book? book = bookService.Read(id);

    if (book is null)
    {
        Console.WriteLine("Книгу не знайдено.");
        return;
    }

    Console.WriteLine("Якщо не хочете змінювати поле, натисніть Enter.");

    Console.Write($"Назва [{book.Title}]: ");
    string? title = Console.ReadLine();

    Console.Write($"Автор [{book.AuthorName}]: ");
    string? authorName = Console.ReadLine();

    Console.Write($"Кількість сторінок [{book.PageCount}]: ");
    string? pageCountText = Console.ReadLine();

    Console.Write($"ISBN [{book.ISBN}]: ");
    string? isbn = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(title))
    {
        book.Title = title;
    }

    if (!string.IsNullOrWhiteSpace(authorName))
    {
        book.AuthorName = authorName;
    }

    if (int.TryParse(pageCountText, out int pageCount) && pageCount > 0)
    {
        book.PageCount = pageCount;
    }

    if (!string.IsNullOrWhiteSpace(isbn))
    {
        book.ISBN = isbn;
    }

    bookService.Update(book);

    Console.WriteLine("Книгу успішно оновлено.");
}

static void RemoveBook(CrudService<Book> bookService)
{
    Console.WriteLine("\n========== ВИДАЛЕННЯ КНИГИ ==========");

    Console.Write("Введіть Id книги: ");
    string? idText = Console.ReadLine();

    if (!Guid.TryParse(idText, out Guid id))
    {
        Console.WriteLine("Помилка: некоректний формат Id.");
        return;
    }

    Book? book = bookService.Read(id);

    if (book is null)
    {
        Console.WriteLine("Книгу не знайдено.");
        return;
    }

    Console.WriteLine($"Ви обрали книгу: {book.Title}");
    Console.Write("Ви дійсно хочете її видалити? Введіть 'так': ");

    string? answer = Console.ReadLine();

    if (answer?.Trim().ToLower() == "так")
    {
        bookService.Remove(book);
        Console.WriteLine("Книгу успішно видалено.");
    }
    else
    {
        Console.WriteLine("Видалення скасовано.");
    }
}

static void BorrowBook(CrudService<Book> bookService)
{
    Console.WriteLine("\n========== ПОЗИЧАННЯ КНИГИ ==========");

    Console.Write("Введіть Id книги: ");
    string? idText = Console.ReadLine();

    if (!Guid.TryParse(idText, out Guid id))
    {
        Console.WriteLine("Помилка: некоректний формат Id.");
        return;
    }

    Book? book = bookService.Read(id);

    if (book is null)
    {
        Console.WriteLine("Книгу не знайдено.");
        return;
    }

    // Підписка на подію позичання книги
    book.BookBorrowed += message =>
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[ПОДІЯ КНИГИ] {message}");
        Console.ResetColor();
    };

    book.Borrow();
}

static void ShowAvailableBooks(CrudService<Book> bookService)
{
    Console.WriteLine("\n========== ДОСТУПНІ КНИГИ ==========");

    var availableBooks = bookService
        .ReadAll()
        .AvailableOnly()
        .ToList();

    if (availableBooks.Count == 0)
    {
        Console.WriteLine("Доступних книг немає.");
        return;
    }

    foreach (var book in availableBooks)
    {
        Console.WriteLine(book.ToShortDescription());
    }
}

static void ShowStatistics()
{
    Console.WriteLine("\n========== СТАТИСТИКА ==========");

    Console.WriteLine(
        LibraryItem.GetLibraryStatistics());
}

static void PrintBook(Book book)
{
    Console.WriteLine("--------------------------------------");
    Console.WriteLine($"Id:                 {book.Id}");
    Console.WriteLine($"Назва:              {book.Title}");
    Console.WriteLine($"Автор:              {book.AuthorName}");
    Console.WriteLine($"Рік видання:        {book.PublicationYear}");
    Console.WriteLine($"Кількість сторінок: {book.PageCount}");
    Console.WriteLine($"ISBN:               {book.ISBN}");
    Console.WriteLine(
        $"Доступна:           {(book.IsAvailable ? "Так" : "Ні")}");
}

static int ReadPositiveNumber()
{
    while (true)
    {
        string? input = Console.ReadLine();

        if (int.TryParse(input, out int number) && number > 0)
        {
            return number;
        }

        Console.Write("Введіть додатне число: ");
    }
}
