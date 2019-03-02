using System;
using Sitecore;
using Sitecore.Jobs;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Shell.Framework.Jobs;
using Sitecore.Web.UI.Sheer;

namespace Reply.Feature.BulkEdit.Commands
{
    public abstract class BaseCommand : Command
    {
        private string handleString;


        public override void Execute(CommandContext context)
        {
            Context.ClientPage.Start(this, nameof(Run), InitalizeClientPipelineArgs(context));
        }

        public void ExecuteSync(string title, Action<ClientPipelineArgs> action, Action<ClientPipelineArgs> postAction)
        {
            if (!(Context.ClientPage.CurrentPipelineArgs is ClientPipelineArgs args))
            {
                return;
            }

            if (args.IsPostBack && !string.IsNullOrEmpty(handleString))
            {
                postAction?.Invoke(args);
                handleString = null;
                return;
            }

            var jobOption = new JobOptions(title, "UI", Client.Site.Name, action.Target, action.Method.Name, new object[] { args })
            {
                ContextUser = Context.User,
                ClientLanguage = Context.Language
            };

            var job = JobManager.Start(jobOption);
            var longRunningOption = new LongRunningOptions(job.Handle.ToString())
            {
                Title = title,
                Icon = string.Empty
            };

            handleString = job.Handle.ToString();

            longRunningOption.ShowModal(true);
            args.WaitForPostBack();
        }


        public abstract ClientPipelineArgs InitalizeClientPipelineArgs(CommandContext context);

        public abstract void Run(ClientPipelineArgs args);
    }
}