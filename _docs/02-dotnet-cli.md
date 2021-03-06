# dotnet CLI

dotnet new globaljson
dotnet new sln -n MiniTrade
dotnet new console -n MiniTrade.ConsoleApp
dotnet sln .\MiniTrade.sln add .\MiniTrade.ConsoleApp\
dotnet user-secrets init --project .\MiniTrade.ConsoleApp\

dotnet add .\MiniTrade.ConsoleApp\ package 

dotnet add .\MiniTrade.ConsoleApp\ package Microsoft.Extensions.Configuration
dotnet add .\MiniTrade.ConsoleApp\ package Microsoft.Extensions.Configuration.CommandLine
dotnet add .\MiniTrade.ConsoleApp\ package Microsoft.Extensions.Configuration.EnvironmentVariables
dotnet add .\MiniTrade.ConsoleApp\ package Microsoft.Extensions.Configuration.Json

dotnet add .\MiniTrade.ConsoleApp\ package Microsoft.Extensions.Logging
dotnet add .\MiniTrade.ConsoleApp\ package Microsoft.Extensions.Logging.Console
dotnet add .\MiniTrade.ConsoleApp\ package Microsoft.Extensions.Logging.Debug

dotnet add .\MiniTrade.ConsoleApp\ package Microsoft.Extensions.Hosting
dotnet add .\MiniTrade.ConsoleApp\ package Microsoft.Extensions.DependencyInjection
dotnet add .\MiniTrade.ConsoleApp\ package Microsoft.Extensions.Http


dotnet add .\MiniTrade.ConsoleApp\ package Microsoft.Extensions.Configuration.UserSecrets

dotnet add .\MiniTrade.ConsoleApp\ package Azure.Messaging.WebPubSub
dotnet add .\MiniTrade.ConsoleApp\ package Azure.Storage.Queues
dotnet add .\MiniTrade.ConsoleApp\ package Azure.Data.Tables
dotnet add .\MiniTrade.ConsoleApp\ package Azure.Storage.Blobs
dotnet add .\MiniTrade.ConsoleApp\ package MongoDB.Driver

dotnet add .\MiniTrade.ConsoleApp\ package Microsoft.Net.Http.Headers

dotnet add .\MiniTrade.ConsoleApp\ package Microsoft.Extensions.Caching.Memory

dotnet add .\MiniTrade.ConsoleApp\ package Microsoft.Data.Analysis

dotnet add .\MiniTrade.ConsoleApp\ package Microsoft.ML

Microsoft.ML.TimeSeries
Microsoft.ML.Recommender
Microsoft.ML.AutoML

Needed to save to Onnx format
Microsoft.ML.OnnxConverter 
Microsoft.ML.OnnxRuntime

Although there's a "AspNetCore", it does not use AspNetCore components
dotnet add .\MiniTrade.ConsoleApp\ package MiniProfiler.AspNetCore

dotnet add .\MiniTrade.ConsoleApp\ package SkiaSharp

Microsoft.ML.Probabilistic
Microsoft.ML.Probabilistic.Compiler
Microsoft.ML.Probabilistic.Learners
Microsoft.ML.Probabilistic.Visualizers.Windows
Microsoft.ML.Probabilistic.Compiler.Visualizers.Window


----

SixLabors.ImageSharp

MathNet.Numerics
ALGLIB
DotNumerics
NLinear
Finaquant Protos


dotnet add .\MiniTrade.ConsoleApp\ package Serilog
dotnet add .\MiniTrade.ConsoleApp\ package Serilog.Extensions.Hosting
dotnet add .\MiniTrade.ConsoleApp\ package Serilog.Formatting.Compact
dotnet add .\MiniTrade.ConsoleApp\ package Serilog.Settings.Configuration
dotnet add .\MiniTrade.ConsoleApp\ package Serilog.Sinks.Console
dotnet add .\MiniTrade.ConsoleApp\ package Serilog.Sinks.File


dotnet new specflowproject -n MiniTrade.ConsoleApp.Tests --framework net6.0 --unittestprovider mstest
dotnet add package Moq


