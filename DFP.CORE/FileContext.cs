using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFP.CORE
{
    /// <summary>
    /// Базовая инициализация класса
    /// </summary>
    /// <remarks>
    /// Добавляем миграцию командой: <code>Add-Migration ChangesName -OutputDir DBMigrations -Project DFP.CORE -Context FileContext</code>
    /// Обновляем БД: <code>Update-Database -project DFP.CORE -context FileContext</code>
    /// Возможно в консоль диспетчеров пакетов нужно добавить инструмент
    /// <code>Install-Package Microsoft.EntityFrameworkCore.Tools</code>
    /// </remarks>
    public class FileContext(DbContextOptions<FileContext> options) : DbContext(options)
    {
        /// <summary>
        /// Таблица файликов
        /// </summary>
        public DbSet<FileEntity> Files { get; set; }

        public IEnumerable<FileInfo> GetFileList()
        {
            return Files.AsNoTracking().Select(x=> new FileInfo()
            {
                FileName = x.FileName,
                ID = x.ID,
                PTime = x.PTime,
                RTime = x.RTime,
                State = x.State,
                UTime = x.UTime,
            }).ToArray();
        }
    }
}
