using Freelancer.Configuration;

using Slimsy.DependencyInjection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddSlimsy()
    .AddDeliveryApi()
    .AddComposers()
    .Build();

builder.Services.Configure<FreelancerConfig>(
    builder.Configuration.GetSection(FreelancerConfig.SectionName));

WebApplication app = builder.Build();

await app.BootUmbracoAsync();


app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseInstallerEndpoints();
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();