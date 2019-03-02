using Reply.Feature.BulkEdit.Models;

namespace Reply.Feature.BulkEdit.Services
{
    public interface IBulkEditService
    {
        bool RootHasChildrenWithSpecificTemplate(string rootId, string templateId); 

        BulkEditViewModel GetBulkEditViewModel(string rootId, string rootLanguage, string templateId);

        void BulkUpdateFields(BulkEditViewModel model);
    }
}