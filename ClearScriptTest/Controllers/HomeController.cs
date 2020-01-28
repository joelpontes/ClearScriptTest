using ClearScriptTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace ClearScriptTest.Controllers
{
    public class LoggerConsole
    {
        public void Log(string l)
        {
            Debug.WriteLine(l);
        }
    }
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            string result = "Ok";
            try
            {
                var engine = new V8ScriptEngine(V8ScriptEngineFlags.EnableDateTimeConversion);
                engine.AddHostObject("host", new HostFunctions());
                engine.AddHostType("LoggerConsole", typeof(LoggerConsole));
                // engine.Execute(@" var o=new LoggerConsole();   o.Log(2+3+''); ");
                result = engine.Evaluate(@" 2+3 ") + "";
            }
            catch(Exception ex)
            {
                result ="Error "+ ex.Message;
            }
            return View("Index",result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
