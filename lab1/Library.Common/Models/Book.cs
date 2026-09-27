namespace Library.Common.Models;

public class Book : LibraryItem
{
    public string AuthorName { get; set; }
    public int PageCount { get; set; }
    public string ISBN { get; set; }

    // Подія
    public event Action<string>? BookBorrowed;

    // Конструктор
    public Book(
        string title,
        int publicationYear,
        string authorName,
        int pageCount,
        string isbn)
        : base(title, publicationYear)
    {
        AuthorName = authorName;
        PageCount = pageCount;
        ISBN = isbn;
    }

    // Метод
    public void Borrow()
    {
        if (!IsAvailable)
        {
            Console.WriteLine($"Книга \"{Title}\" вже позичена.");
            return;
        }

        IsAvailable = false;
        BookBorrowed?.Invoke($"Книгу \"{Title}\" позичено читачеві.");
    }

    public override string GetInfo()
    {
        return $"Книга: {Title}; автор: {AuthorName}; сторінок: {PageCount}; ISBN: {ISBN}; доступна: {IsAvailable}";
    }
}
