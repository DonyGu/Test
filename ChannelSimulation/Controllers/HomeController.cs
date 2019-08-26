using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ChannelSimulation.Controllers
{
    public class HomeController : Controller
    {
        string path = System.AppDomain.CurrentDomain.BaseDirectory;

        // GET: Home
        public ActionResult Index()
        {
            FileStream stream = new FileStream($"{path}Data/sendMessage.json", FileMode.Open);
            using (var reader=new StreamReader(stream))
            {
                string json = reader.ReadToEnd();
                return View((object)json);
            }
        }
        [HttpPost]
        public ActionResult Index(string content)
        {
            var outputStream = System.IO.File.OpenWrite($"{path}Data/sendMessage.json");
            using (var writer=new StreamWriter(outputStream))
            {
                byte[] preamble = Encoding.UTF8.GetPreamble();
                outputStream.Write(preamble,0,preamble.Length);
                writer.Write(content);
            }
            FileStream stream = new FileStream($"{path}Data/sendMessage.json", FileMode.Open);
            using (var reader = new StreamReader(stream))
            {
                string json = reader.ReadToEnd();
                return View((object)json);
            }
        }
        public JsonResult Test()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            FileStream stream = new FileStream($"{path}Data/sendMessage.json", FileMode.Open);
            using (var reader = new StreamReader(stream))
            {
                string json = reader.ReadToEnd();
                return Json(json,JsonRequestBehavior.AllowGet);
            }
        }
    }
}