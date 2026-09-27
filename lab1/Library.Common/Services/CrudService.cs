using Library.Common.Models;

namespace Library.Common.Services;

public class CrudService<T> : ICrudService<T>
    where T : LibraryItem
{
    private readonly List<T> _items = new();

    // Делегат
    public delegate void ItemChangedHandler(string message);

    // Подія
    public event ItemChangedHandler? ItemChanged;

    public void Create(T element)
    {
        _items.Add(element);
        ItemChanged?.Invoke($"Додано елемент: {element.Title}");
    }

    public T? Read(Guid id)
    {
        return _items.FirstOrDefault(item => item.Id == id);
    }

    public IEnumerable<T> ReadAll()
    {
        return _items;
    }

    public void Update(T element)
    {
        var existingItem = Read(element.Id);

        if (existingItem is null)
        {
            throw new InvalidOperationException("Елемент не знайдено.");
        }

        var index = _items.IndexOf(existingItem);
        _items[index] = element;

        ItemChanged?.Invoke($"Оновлено елемент: {element.Title}");
    }

    public void Remove(T element)
    {
        if (_items.Remove(element))
        {
            ItemChanged?.Invoke($"Видалено елемент: {element.Title}");
        }
    }
}
