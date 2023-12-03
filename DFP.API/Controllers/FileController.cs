using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DFP.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FileController(ILogger<FileController> sysLog, CORE.FileContext db ) : ControllerBase
    {
        [ActionName("upload")]
        [HttpPost]
        public async Task<IActionResult> OnPostUploadAsync(IFormFile file)
        {
            var uFile = new CORE.FileEntity()
            {
                FileName = file.FileName
            };
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                uFile.FileData = stream.ToArray();
            }
            db.Files.Add(uFile);
            await db.SaveChangesAsync();
            sysLog.LogInformation($"Получен файл {file.FileName} прочитано {uFile.FileData.Length} байт");
            return Ok(new { count = 1, uFile.FileData });
        }

        [ActionName("list")]
        [HttpGet]
        public IEnumerable<FileInfo> GetFilesList()
        {
            var r = new List<FileInfo>();
            var fileList = db.GetFileList();
            if (fileList != null) 
            {
                foreach(var file in fileList)
                {
                    var fi = new FileInfo()
                    {
                        Modify = file.LastModify().ToString(),
                        Name = file.FileName,
                        State = (int)file.State,
                        ID = file.ID.ToString(),
                    };
                    if (file.State== CORE.FileProcessState.Ready)
                    {
                        var ub = new UriBuilder
                        {
                            Path = "\\File\\download",
                            Query = $"?id={fi.ID}",
                            Scheme = Request.Scheme,
                            Host = Request.Host.Host
                        };
                        if (Request.Host.Port.HasValue)
                        {
                            ub.Port = Request.Host.Port.Value;
                        }
                        fi.URL = ub.ToString();
                    }
                    r.Add(fi);
                }
            }
            return r;
        }

        /// <summary>
        /// Получить файл по запросу
        /// </summary>
        /// <param name="id">Идентификатор файлика</param>
        /// <returns></returns>
        [ActionName("download")]
        [HttpGet]
        public IActionResult GetFile(Guid? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var file = db.Files.AsNoTracking().Where(x=> x.ID == id.Value).FirstOrDefault();
                    if (file != null) 
                    {
                        if (file.ResultData!= null) 
                        {
                            return File(file.ResultData, "application/pdf", $"{file.FileName}.pdf");
                        }
                        
                    }
                }
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                sysLog.LogError(ex, "Чет пошло не так при создании файлика");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
