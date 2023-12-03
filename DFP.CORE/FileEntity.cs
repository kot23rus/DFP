using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DFP.CORE
{
    /// <summary>
    /// Данные файла
    /// </summary>
    [Table("Files")]
    [Index(nameof(ID), IsUnique = true)]
    [Index(nameof(State), IsUnique = false)]
    [Index(nameof(UTime), IsUnique = false)]
    public class FileEntity
    {
        [Key]
        public Guid ID { get; set; }
        /// <summary>
        /// Когда файл был загружен
        /// </summary>
        public DateTime UTime {  get; set; } = DateTime.Now;
        /// <summary>
        /// Статус файла
        /// </summary>
        public FileProcessState State { get; set; } = FileProcessState.Upload;
        /// <summary>
        /// Когда файл попал на обработку
        /// </summary>
        public DateTime? RTime { get; set; }
        /// <summary>
        /// Когда файл был обработан
        /// </summary>
        public DateTime? PTime {  get; set; }
        /// <summary>
        /// Данные файла
        /// </summary>
        public byte[]? FileData { get; set; }
        /// <summary>
        /// Имя файлика
        /// </summary>
        public string FileName { get; set; }  = string.Empty;
        /// <summary>
        /// Результат обработки файлика
        /// </summary>
        public byte[]? ResultData { get; set; }
    }
}
