using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TodoApp.Models;

// Модель одной задачи. Сообщает экрану об изменениях через INotifyPropertyChanged.
public class TodoItem : INotifyPropertyChanged
{
    private string _title = string.Empty;
    private bool _isDone;
    private bool _isEditing;
    // Свойство для режима редактирования
    public bool IsEditing
    {
        get => _isEditing;
        set
        {
            _isEditing = value;
            OnPropertyChanged();
        }
    }

    // Свойство для текста задачи
    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged(); 
        }
    }

    // Свойство для статуса выполнения
    public bool IsDone
    {
        get => _isDone;
        set
        {
            _isDone = value;
            OnPropertyChanged(); // Сигнал экрану: галочка изменилась
        }
    }

    // Механизм сигнализации для XAML
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        // Передаем имя изменившегося свойства
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}