using System.ComponentModel.DataAnnotations;

namespace Domain
{
    /// <summary>
    /// Базовый класс для классов-таблиц БД
    /// </summary>
    public abstract class DbObject : IMarkDeleted
    {
        [Key]
        public int Id { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
