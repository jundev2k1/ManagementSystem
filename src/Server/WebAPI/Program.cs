// Copyright (c) 2025 - Jun Dev. All rights reserved

using WebAPI;
using Infrastructure;
using Application;
using Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication(builder.Configuration)
	.AddWebApi(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseWebApi();

// Run migration
using (var scope = app.Services.CreateScope())
{
    var dbInitialize = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
    await dbInitialize.InitialiseDatabaseAsync();
}

app.Run();
