namespace DFP.CORE
{
    /// <summary>
    /// Описание состояния обработки файла
    /// </summary>
    public enum FileProcessState
    {
        /// <summary>
        /// Загружен
        /// </summary>
        Upload = 0,
        /// <summary>
        /// Обработка
        /// </summary>
        Process,
        /// <summary>
        /// Обработан
        /// </summary>
        Ready,
        /// <summary>
        /// Ошибки при выполнении
        /// </summary>
        Error,

    }
}
