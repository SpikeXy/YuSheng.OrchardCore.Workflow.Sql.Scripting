using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using OrchardCore.Workflows.Abstractions.Models;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Models;
using OrchardCore.Workflows.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YesSql;

namespace YuSheng.OrchardCore.Workflow.Sql.Scripting.Activities
{
    public class SqlScriptTask : TaskActivity
    {
        private readonly IStore _store;
        private readonly IWorkflowScriptEvaluator _scriptEvaluator;
        private readonly IStringLocalizer S;
        private readonly IHtmlHelper _htmlHelper;
        private readonly IWorkflowExpressionEvaluator _expressionEvaluator;
        public SqlScriptTask(IWorkflowScriptEvaluator scriptEvaluator,
            IWorkflowExpressionEvaluator expressionEvaluator,
            IHtmlHelper htmlHelper,
            IStore store,
            IStringLocalizer<SqlScriptTask> localizer)
        {
            _scriptEvaluator = scriptEvaluator;
            _expressionEvaluator = expressionEvaluator;
            S = localizer;
            _store = store;
            _htmlHelper = htmlHelper;

        }

        public override string Name => nameof(SqlScriptTask);

        public override LocalizedString DisplayText => S["Sql Script Task"];

        public override LocalizedString Category => S["Script"];

        /// <summary>
        /// The script can call any available functions, including setOutcome().
        /// </summary>
        public WorkflowExpression<object> Script
        {
            get => GetProperty(() => new WorkflowExpression<object>("setOutcome('Done');"));
            set => SetProperty(value);
        }

        public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Outcomes(S["Done"]);
        }

        public override async Task<ActivityExecutionResult> ExecuteAsync(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {

            var connection = _store.Configuration.ConnectionFactory.CreateConnection();
            var rawQuery = Script.Expression;
            string code = "";
            try
            {
                using (connection)
                {
                    await connection.OpenAsync();
                    var Documents = await connection.QueryAsync(rawQuery,null);
                    code = JsonConvert.SerializeObject(Documents);
                }
            }
            catch (Exception ex)
            {
                code = ex.Message;
            }

            workflowContext.Output["SqlScript"] = _htmlHelper.Raw(_htmlHelper.Encode(code));

            return Outcomes("Done");
        }
    }
}
