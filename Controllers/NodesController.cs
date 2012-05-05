using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Orchard.ContentManagement;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Xml;
using Orchard.Themes;
using Orchard.Mvc;
using System.IO;

namespace Associativy.WebServices.Controllers
{
    public class NodesController : Controller
    {
        private readonly IContentManager _contentManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NodesController(
            IContentManager contentManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _contentManager = contentManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public JsonResult Index(int id)
        {
            var doc = new XDocument();
            var element = _contentManager.Export(_contentManager.Get(id));
            element.SetAttributeValue("NumericId", id);
            doc.Add(element);
            return new JsonResult { Data = JsonConvert.SerializeXNode(doc), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPut, ActionName("Index")]
        public JsonResult IndexPut(int id = 0)
        {
            // Create if id == 0 or update
            var inputStream = _httpContextAccessor.Current().Request.InputStream;
            inputStream.Seek(0, SeekOrigin.Begin);
            using (var sr = new StreamReader(inputStream))
            {
                var putData = sr.ReadToEnd();
                var xNode = JsonConvert.DeserializeXNode(putData);
                _contentManager.Import(xNode.Element(xNode.Root.Name), new ImportContentSession(_contentManager));
            }

            return null;
        }

        [Themed]
        public ActionResult Test()
        {
            return View();
        }
    }
}
