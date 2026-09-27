namespace Library.Common.Models;

public class Author
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Country { get; set; }
    public int BirthYear { get; set; }

    // Конструктор
    public Author(string fullName, string country, int birthYear)
    {
        Id = Guid.NewGuid();
        FullName = fullName;
        Country = country;
        BirthYear = birthYear;
    }

    // Метод
    public string GetInfo()
    {
        return $"{FullName}, країна: {Country}, рік народження: {BirthYear}";
    }
}
