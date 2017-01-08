using System.Data;
using System.Diagnostics.Contracts;

namespace TaskManager.DataLayer.MsSql
{
    /// <summary>
    /// Структура представялет команду SQL
    /// </summary>
    public struct SqlCommandInfo
    {
        public SqlCommandInfo(string command, CommandType commandType) : this()
        {
            Contract.Requires(!string.IsNullOrEmpty(command));

            this.Command = command;
            this.CommandType = commandType;
        }

        /// <summary>
        /// Текст команды (название хранимой процедуры или запрос)
        /// </summary>
        public string Command { get; private set; }

        /// <summary>
        /// Тип команды
        /// </summary>
        public CommandType CommandType { get; private set; }
    }
    
    /// <summary>
    /// Полный набор CRUD-команд, объединённый в одну связку
    /// </summary>
    public class CrudCommandsBundle
    {
        /// <summary>
        /// Команда для получения всех сущностей репозитория
        /// </summary>
        public SqlCommandInfo GetAllCommand { get; set; }

        /// <summary>
        /// Команда для получения сущности по идентификатору
        /// </summary>
        public SqlCommandInfo GetByIdCommand { get; set; }

        /// <summary>
        /// Команда для добавления новой сущности
        /// </summary>
        public SqlCommandInfo CreateCommand { get; set; }

        /// <summary>
        /// Команда для обновления сущности
        /// </summary>
        public SqlCommandInfo UpdateCommand { get; set; }

        /// <summary>
        /// Команда для удаления сущности
        /// </summary>
        public SqlCommandInfo DeleteCommand { get; set; }
    }
}