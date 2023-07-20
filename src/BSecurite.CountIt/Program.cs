// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BSecurite.CountIt.Abstractions;
using BSecurite.CountIt.Services;
using Microsoft.Extensions.Logging;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddTransient<IFileReader, FileReader>();

using IHost host = builder.Build();

var fileReader = host.Services.GetRequiredService<IFileReader>();

await host.RunAsync();
