using Microsoft.EntityFrameworkCore;
using PuppeteerSharp;
using System.Runtime.CompilerServices;
using System.Text;

namespace DFP.SERVICE
{
    public class Worker(ILogger<Worker> sysLog, IConfiguration config) : BackgroundService
    {
        protected CORE.FileContext GetBD()
        {
            DbContextOptionsBuilder<CORE.FileContext> ob = new();
            var ConnectionString = config.GetConnectionString("DefaultConnection");
            ob.UseSqlServer(ConnectionString);
            var db = new CORE.FileContext(ob.Options);
            return db;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var db = GetBD())
                {
                    file = db.Files.AsNoTracking().Where(x => x.State == CORE.FileProcessState.Upload).OrderBy(x => x.UTime).FirstOrDefault();
                    await ProcessFile(db);
                }
                Thread.Sleep(1000);//Имитируем дикую занятость сервиса...
                
            }
        }
        /// <summary>
        /// Текущее файло для обработки
        /// </summary>
        private CORE.FileEntity? file;

        private async Task ProcessFile(CORE.FileContext db)
        {
            if (file != null)
            {
                if (file.FileData!= null) 
                {
                    try
                    {
                        var source = System.Text.Encoding.UTF8.GetString(file.FileData);
                        if (!string.IsNullOrEmpty(source))
                        {
                            MarkProcess(CORE.FileProcessState.Process, db);
                            Thread.Sleep(1000);//Имитируем дикую занятость сервера
                            using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true, IgnoredDefaultArgs = ["--disable-extensions"] });
                            using var page = await browser.NewPageAsync();
                            await page.SetContentAsync(source);
                            file.ResultData = await page.PdfDataAsync();
                            file.State = CORE.FileProcessState.Ready;
                            file.PTime = DateTime.Now;
                            db.Files.Update(file);
                            db.SaveChanges();
                            Thread.Sleep(1000);//Имитируем дикую занятость сервера
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MarkProcess(CORE.FileProcessState.Error, db);
                        sysLog.LogError(ex, "Крашнуло трошки на создании PDF");
                    }
                }
                MarkProcess(CORE.FileProcessState.Error, db);
            }   
        }
        /// <summary>
        /// Отмечаем что файло в работе
        /// </summary>
        /// <param name="nState">Новый статус файла</param>
        private void MarkProcess(CORE.FileProcessState nState, CORE.FileContext db)
        {
            if(file!=null)
            {
                file.State = nState;
                file.RTime = DateTime.Now;
                db.Files.Attach(file);
                db.Entry(file).Property(c => c.State).IsModified = true;
                db.Entry(file).Property(c => c.RTime).IsModified = true;
                db.SaveChanges();
            }
        }
        
    }
}
