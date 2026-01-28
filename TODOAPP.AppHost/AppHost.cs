var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.TODOAPP>("todoapp");

builder.Build().Run();
