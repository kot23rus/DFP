using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFP.CORE
{
    public class FileInfo
    {
        public Guid ID;
        /// <summary>
        /// Когда файл был загружен
        /// </summary>
        public DateTime UTime;
        /// <summary>
        /// Статус файла
        /// </summary>
        public FileProcessState State;
        /// <summary>
        /// Когда файл попал на обработку
        /// </summary>
        public DateTime? RTime;
        /// <summary>
        /// Когда файл был обработан
        /// </summary>
        public DateTime? PTime;
        /// <summary>
        /// Имя файлика
        /// </summary>
        public string FileName;
        
        /// <summary>
        /// Получить время обновления файлика
        /// </summary>
        /// <returns></returns>
        public DateTime LastModify()
        {
            if( PTime.HasValue)  return PTime.Value;
            if (RTime.HasValue) return RTime.Value;
            return UTime;

        }
    }
}
