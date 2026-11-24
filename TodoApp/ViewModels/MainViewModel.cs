using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TodoApp.Models;

namespace TodoApp.ViewModels;

// "Мозг" приложения. Реализует INotifyPropertyChanged для обновления экрана.
public class MainViewModel : INotifyPropertyChanged
{
    // Список задач.
    public ObservableCollection<TodoItem> Tasks { get; set; } = new();

    // Текст, который вводит пользователь.
    private string _newTaskTitle = string.Empty;
    public string NewTaskTitle
    {
        get => _newTaskTitle;
        set { _newTaskTitle = value; OnPropertyChanged(); }
    }

    // Команды для кнопок "+" и "🗑"
    public ICommand AddTaskCommand { get; }
    public ICommand DeleteTaskCommand { get; }

    // Команды для режима редактирования 
    public ICommand StartEditCommand { get; }
    public ICommand FinishEditCommand { get; }

    // Конструктор: срабатывает при запуске.
    public MainViewModel()
    {
        // Привязка команд к методам
        AddTaskCommand = new Command(AddTask);
        DeleteTaskCommand = new Command<TodoItem>(DeleteTask);
        StartEditCommand = new Command<TodoItem>(StartEdit);
        FinishEditCommand = new Command<TodoItem>(FinishEdit);

        // Стартовые задачи 
        Tasks.Add(new TodoItem { Title = "Купи-батон", IsDone = true });
        Tasks.Add(new TodoItem { Title = "Купить еды", IsDone = false });
    }

    // Добавление задачи (нажатие на "+")
    private void AddTask()
    {
        // Если поле не пустое — создаем задачу и очищаем поле ввода
        if (!string.IsNullOrWhiteSpace(NewTaskTitle))
        {
            Tasks.Add(new TodoItem { Title = NewTaskTitle, IsDone = false });
            NewTaskTitle = string.Empty;
        }
    }

    // Удаление задачи (нажатие на "🗑"). 
    private void DeleteTask(TodoItem task)
    {
        if (Tasks.Contains(task))
            Tasks.Remove(task);
    }

    // Включение режима редактирования.
    private void StartEdit(TodoItem task)
    {
        if (task != null)
            task.IsEditing = true;
    }

    // Выключение режима редактирования.
    private void FinishEdit(TodoItem task)
    {
        if (task != null)
            task.IsEditing = false;
    }

    // Механизм сигнализации для XAML 
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}