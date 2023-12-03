namespace DFP.API.Controllers
{
    public class FileInfo
    {
        /// <summary>
        /// Идентификатор файлика
        /// </summary>
        public string ID { get; set; } = string.Empty;
        /// <summary>
        /// Имя файла
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Когда последний раз был изменен
        /// </summary>
        public string Modify { get; set; } = DateTime.Now.ToString();
        /// <summary>
        /// Состояние файла
        /// </summary>
        public int State { get; set; } = 0;
        /// <summary>
        /// Ссылочка для скачивания
        /// </summary>
        public string? URL {  get; set; }
    }
}
