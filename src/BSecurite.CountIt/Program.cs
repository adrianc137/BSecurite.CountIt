// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BSecurite.CountIt.Abstractions;
using BSecurite.CountIt.Services;
using Microsoft.Extensions.Configuration;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddTransient<IFileReader, FileReader>();
builder.Services.AddTransient<IWordMatcher, WordMatcher>();
builder.Services.AddTransient<IWordSorter, WordSorter>();
builder.Services.AddTransient<IWordProcessor, WordProcessor>();
builder.Services.AddTransient<IOutputGenerator, OutputGenerator>();

using var host = builder.Build();
var config = host.Services.GetRequiredService<IConfiguration>();

var wordProcessor = host.Services.GetRequiredService<IWordProcessor>();
var results = await wordProcessor.ProcessFileContents(config.GetSection("InputFilePath").Value ?? string.Empty);

var outputGenerator = host.Services.GetRequiredService<IOutputGenerator>();
outputGenerator.PrintResults(results);

await host.RunAsync();
