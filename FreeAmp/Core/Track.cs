namespace FreeAmp.Core
{
    /// <summary>
    ///     Представляет имя и путь к файлу композиции
    /// </summary>
    public class Track 
    {
        /// <summary>
        ///     Конструктор по умолчанию
        /// </summary>
        public Track()
        {
            Name = string.Empty;
        }

        /// <summary>
        ///     Создает новое описание файла композиции
        /// </summary>
        /// <param name="name">
        ///     Имя и путь к файлу композиции
        /// </param>
        public Track(string name) : this()
        {
            Name = name;
        }

        /// <summary>
        ///     Имя и путь к файлу композиции
        /// </summary>
        public string Name { get; set; }
    }
}