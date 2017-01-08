namespace TaskManager.DataLayer.MsSql.Dto
{
    /// <summary>
    /// Базовый класс для всех DTO, предназначенных для работы с MS SQL Server
    /// </summary>
    public abstract class SqlDto
    {
        /// <summary>
        /// Метод формирует набор параметров для передачи в хранимую процедуру добавления
        /// новой записи. Названия свойств должны совпадать с названиями параметров процедуры
        /// </summary>
        /// <returns></returns>
        public abstract object GetParametersForInsert();

        /// <summary>
        /// Метод формирует набор параметров для передачи в хранимую процедуру редактирования
        /// записи. Названия свойств должны совпадать с названиями параметров процедуры
        /// </summary>
        /// <returns></returns>
        public abstract object GetParametersForUpdate();
    }
}