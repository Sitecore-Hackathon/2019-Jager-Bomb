using System.Web.Mvc;
using Reply.Feature.BulkEdit.Models;
using Reply.Feature.BulkEdit.Services;

namespace Reply.Feature.BulkEdit.Controllers
{
    public class BulkEditController : Controller
    {
        private readonly IBulkEditService bulkEditService;

        public BulkEditController()
        {
            // todo: use Dependency Injection
            bulkEditService = new BulkEditService();
        }

        [HttpGet]
        public ActionResult Index(string rootId, string rootLanguage, string templateId)
        {
            if (!bulkEditService.RootHasChildrenWithSpecificTemplate(rootId, templateId))
            {
                return Content("There is no children with specified template");
            }

            var model = bulkEditService.GetBulkEditViewModel(rootId, rootLanguage, templateId);

            return View(model);
        }



        [HttpPost]
        public ActionResult Index(BulkEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                bulkEditService.BulkUpdateFields(model);
                return Redirect("/sitecore/shell/sitecore/client/Applications/Launchpad");
            }

            return View(model);
        }
    }
}