using Library.Common.Models;

namespace Library.Common.Extensions;

public static class LibraryExtensions
{
    // Метод розширення
    public static string ToShortDescription(this LibraryItem item)
    {
        return $"{item.Title} ({item.PublicationYear})";
    }

    // Ще один метод розширення
    public static IEnumerable<T> AvailableOnly<T>(
        this IEnumerable<T> items)
        where T : LibraryItem
    {
        return items.Where(item => item.IsAvailable);
    }
}
