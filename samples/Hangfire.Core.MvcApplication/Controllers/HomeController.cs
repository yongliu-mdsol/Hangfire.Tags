using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Hangfire.Core.MvcApplication.Models;
using Hangfire.Server;
using Hangfire.Tags;
using Hangfire.Tags.Attributes;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Hangfire.Core.MvcApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Buffer()
        {
            return Content(TextBuffer.ToString());
        }

        [HttpPost]
        public async Task<ActionResult> Create()
        {


            await DoAsync(0);
            TextBuffer.WriteLine("Background Job completed succesfully!");

            return RedirectToAction("Index");
        }

        [JobDisplayName("my custom job $#{0}")]
        public Task DoAsync(int id)
        {
            BackgroundJob.Enqueue(() => Process($"segment{id}", $"study{id}", null));
            return Task.CompletedTask;
        }

        [JobDisplayName("my custom job $#{0} and study name {1}")]
        public void Process(string segmentName, string studyName, PerformContext cx)
        {
            var now = DateTime.Now;

            cx.AddTags(new string[] { segmentName,studyName, Guid.NewGuid().ToString() });

        }


        [Tag("job", "{0}", "{1}")]
        public void Job(string name, PerformContext ctx)
        {
            var now = DateTime.Now;

            ctx.AddTags(new string[] { "Segment" + now.Second.ToString(), "Study" + now.Second + now.Millisecond, Guid.NewGuid().ToString() });

            TextBuffer.WriteLine("Background Job completed succesfully!");
        }
    }
}
