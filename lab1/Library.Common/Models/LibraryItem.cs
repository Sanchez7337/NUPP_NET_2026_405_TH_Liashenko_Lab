namespace Library.Common.Models;

public abstract class LibraryItem
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int PublicationYear { get; set; }
    public bool IsAvailable { get; set; }

    // Статичне поле
    public static int TotalItems { get; private set; }

    // Статичний конструктор
    static LibraryItem()
    {
        TotalItems = 0;
    }

    // Конструктор
    protected LibraryItem(string title, int publicationYear)
    {
        Id = Guid.NewGuid();
        Title = title;
        PublicationYear = publicationYear;
        IsAvailable = true;

        TotalItems++;
    }

    // Метод
    public virtual string GetInfo()
    {
        return $"{Title}, {PublicationYear}, доступний: {IsAvailable}";
    }

    // Статичний метод
    public static string GetLibraryStatistics()
    {
        return $"Загальна кількість створених елементів: {TotalItems}";
    }
}
