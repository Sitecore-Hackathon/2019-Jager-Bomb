using System;
using Sitecore;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;

namespace Reply.Feature.BulkEdit.Commands
{
    public class BulkEditCommand : BaseCommand
    {
        private const string FinishedResult = "finished";
        private const string RootId = "rootId";
        private const string RootLanguage = "rootLanguage";
        
        public override CommandState QueryState(CommandContext context)
        {
            return context.Items[0].Children.Count == 0 ? CommandState.Disabled : base.QueryState(context);
        }

        public override ClientPipelineArgs InitalizeClientPipelineArgs(CommandContext context)
        {
            var sourceItem = context.Items[0];
            var args = new ClientPipelineArgs();

            args.Parameters.Add(RootId, sourceItem.ID.ToString());
            args.Parameters.Add(RootLanguage, sourceItem.Language.ToString());

            return args;
        }

        public override void Run(ClientPipelineArgs args)
        {
            if (!args.IsPostBack)
            {
                var bulkeditDialog = Sitecore.Configuration.Settings.GetSetting("item:bulkedit:dialog");
                SheerResponse.ShowModalDialog(new ModalDialogOptions(bulkeditDialog)
                {
                    Response = true,
                    ForceDialogSize = true
                });
                args.WaitForPostBack();
                return;
            }

            if (!args.HasResult)
            {
                return;
            }

            if (!args.Result.Equals(FinishedResult, StringComparison.InvariantCultureIgnoreCase))
            {
                var item = Context.ContentDatabase.GetItem(args.Result);
                if (item == null)
                {
                    SheerResponse.Alert("Template path not found or invalid");
                    args.WaitForPostBack();
                    return;
                }

                var bulkeditPage = Sitecore.Configuration.Settings.GetSetting("item:bulkedit:page");
                var url = $"{bulkeditPage}?rootId={args.Parameters[RootId]}&rootLanguage={args.Parameters[RootLanguage]}&templateId={item.ID}";

                SheerResponse.Eval($"window.top.location.href='{url}';");
            }
        }
    }
}