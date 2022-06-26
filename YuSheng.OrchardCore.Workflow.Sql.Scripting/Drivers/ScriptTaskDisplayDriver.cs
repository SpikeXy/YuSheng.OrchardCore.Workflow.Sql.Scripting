using OrchardCore.Workflows.Display;
using OrchardCore.Workflows.Models;
using YuSheng.OrchardCore.Workflow.Sql.Scripting.Activities;
using YuSheng.OrchardCore.Workflow.Sql.Scripting.ViewModels;

namespace YuSheng.OrchardCore.Workflow.Sql.Scripting.Drivers
{
    public class ScriptTaskDisplayDriver : ActivityDisplayDriver<SqlScriptTask, ScriptTaskViewModel>
    {
        protected override void EditActivity(SqlScriptTask source, ScriptTaskViewModel model)
        {
            model.Script = source.Script.Expression;
        }

        protected override void UpdateActivity(ScriptTaskViewModel model, SqlScriptTask activity)
        {
            activity.Script = new WorkflowExpression<object>(model.Script);
        }
    }
}
