using System.Linq;
using Reply.Feature.BulkEdit.Models;
using Sitecore;
using Sitecore.Data;
using Sitecore.Globalization;
using Sitecore.SecurityModel;

namespace Reply.Feature.BulkEdit.Services
{
    public class BulkEditService : IBulkEditService
    {
        public bool RootHasChildrenWithSpecificTemplate(string rootId, string templateId)
        {
            var root = GetDatabase().GetItem(new ID(rootId));
            var children = root.Children.Where(x => x.TemplateID == new TemplateID(new ID(templateId)));
            return children.Any();
        }

        public BulkEditViewModel GetBulkEditViewModel(string rootId, string rootLanguage, string templateId)
        {
            var template = GetDatabase().GetTemplate(new ID(templateId));

            var model = new BulkEditViewModel
            {
                RootId = rootId,
                RootLanguage = rootLanguage,
                TemplateId = templateId
            };

            var fields = template.Fields.Where(x => !x.Name.StartsWith("__"));

            foreach (var field in fields)
            {
                model.Fields.Add(new BulkEditFieldViewModel
                {
                    Id = field.ID.ToString(),
                    Name = field.Name
                });
            }

            return model;
        }

        public void BulkUpdateFields(BulkEditViewModel model)
        {
            var root = GetDatabase().GetItem(new ID(model.RootId));
            if (root == null) return;

            var children = root.Children.Where(x => x.TemplateID == new TemplateID(new ID(model.TemplateId))).ToList();
            if (children.Any())
            {
                using (new SecurityDisabler())
                {
                    if (Language.TryParse(model.RootLanguage, out var language))
                    {
                        using (new LanguageSwitcher(language))
                        {
                            foreach (var child in children)
                            {
                                child.Editing.BeginEdit();

                                foreach (var field in model.Fields)
                                {
                                    if (!string.IsNullOrEmpty(field.Value))
                                    {
                                        child.Fields[field.Id].Value = field.Value;
                                    } 
                                }

                                child.Editing.EndEdit();
                            }
                        }
                    }
                }
            }
        }

        private Database GetDatabase()
        {
            return Context.ContentDatabase ?? Database.GetDatabase("master");
        }
    }
}