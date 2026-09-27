namespace Library.Common.Models;

public class Magazine : LibraryItem
{
    public int IssueNumber { get; set; }
    public string Category { get; set; }
    public string Editor { get; set; }

    // Конструктор
    public Magazine(
        string title,
        int publicationYear,
        int issueNumber,
        string category,
        string editor)
        : base(title, publicationYear)
    {
        IssueNumber = issueNumber;
        Category = category;
        Editor = editor;
    }

    // Метод
    public override string GetInfo()
    {
        return $"Журнал: {Title}; номер: {IssueNumber}; категорія: {Category}; редактор: {Editor}";
    }
}
