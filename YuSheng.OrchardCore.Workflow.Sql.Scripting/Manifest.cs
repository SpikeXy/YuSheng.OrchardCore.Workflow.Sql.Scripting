using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "YuSheng OrchardCore Workflow Sql Scripting",
    Author = "spike",
    Website = "",
    Version = "0.0.1"
)]

[assembly: Feature(
    Id = "YuSheng OrchardCore Workflow Sql Scripting",
    Name = "YuSheng OrchardCore Workflow Sql Scripting",
    Description = "Provides sql scripting ",
    Dependencies = new[] { "OrchardCore.Workflows" },
    Category = "Workflows"
)]
