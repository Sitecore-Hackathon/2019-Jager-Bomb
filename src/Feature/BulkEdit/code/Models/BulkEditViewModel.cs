using System.Collections.Generic;

namespace Reply.Feature.BulkEdit.Models
{
    public class BulkEditViewModel
    {
        public BulkEditViewModel()
        {
            Fields = new List<BulkEditFieldViewModel>();
        }

        public IList<BulkEditFieldViewModel> Fields { get; set; }

        public string RootId { get; set; }

        public string RootLanguage{ get; set; }

        public string TemplateId { get; set; }
    }
}