namespace Library.Common.Models;

public class Reader
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public int BorrowedBooksCount { get; set; }

    // Конструктор
    public Reader(string fullName, string email)
    {
        Id = Guid.NewGuid();
        FullName = fullName;
        Email = email;
        BorrowedBooksCount = 0;
    }

    // Метод
    public void BorrowBook()
    {
        BorrowedBooksCount++;
    }
}
