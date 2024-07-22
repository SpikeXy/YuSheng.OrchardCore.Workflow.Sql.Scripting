using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using OrchardCore.Workflows.Helpers;
using YuSheng.OrchardCore.Workflow.Sql.Scripting.Activities;
using YuSheng.OrchardCore.Workflow.Sql.Scripting.Drivers;

namespace YuSheng.OrchardCore.Workflow.Sql.Scripting
{
    [Feature("YuSheng.OrchardCore.Workflow.Sql.Scripting")]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {

            services.AddActivity<SqlScriptTask, ScriptTaskDisplayDriver>(); ;


        }
    }
}
