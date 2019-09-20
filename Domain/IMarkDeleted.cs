namespace Domain
{
    /// <summary>
    /// Классы, реализующие этот интерфейс, при удалении из БД
    /// будут помечаться, как удалённые, но не удаляться из БД
    /// </summary>
    public interface IMarkDeleted
    {
        bool IsDeleted { get; set; }
    }
}
