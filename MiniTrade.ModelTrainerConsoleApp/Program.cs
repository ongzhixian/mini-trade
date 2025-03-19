//Create ML Context with seed for repeteable/deterministic results
using Microsoft.Extensions.Configuration;
using Microsoft.ML;
using Microsoft.ML.Data;

using MiniTrade.ModelTrainerConsoleApp.Models;

IConfigurationRoot configuration = GetConfiguration();

var sampleDataPath = configuration["SampleDataPath"] ?? throw new Exception("Configuration SampleDataPath");

var TrainDataPath = Path.Combine(sampleDataPath, "taxi-fare", "taxi-fare-train.csv");
var TestDataPath = Path.Combine(sampleDataPath, "taxi-fare", "taxi-fare-test.csv");


MLContext mlContext = new MLContext(seed: 0);

// STEP 1: Common data loading configuration
IDataView baseTrainingDataView = mlContext.Data.LoadFromTextFile<TaxiTrip>(TrainDataPath, hasHeader: true, separatorChar: ',');
IDataView testDataView = mlContext.Data.LoadFromTextFile<TaxiTrip>(TestDataPath, hasHeader: true, separatorChar: ',');

//Sample code of removing extreme data like "outliers" for FareAmounts higher than $150 and lower than $1 which can be error-data 
var cnt = baseTrainingDataView.GetColumn<float>(nameof(TaxiTrip.FareAmount)).Count();
IDataView trainingDataView = mlContext.Data.FilterRowsByColumn(baseTrainingDataView, nameof(TaxiTrip.FareAmount), lowerBound: 1, upperBound: 150);
var cnt2 = trainingDataView.GetColumn<float>(nameof(TaxiTrip.FareAmount)).Count();

// STEP 2: Common data process configuration with pipeline data transformations
var dataProcessPipeline = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: nameof(TaxiTrip.FareAmount))
                            .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "VendorIdEncoded", inputColumnName: nameof(TaxiTrip.VendorId)))
                            .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "RateCodeEncoded", inputColumnName: nameof(TaxiTrip.RateCode)))
                            .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "PaymentTypeEncoded", inputColumnName: nameof(TaxiTrip.PaymentType)))
                            .Append(mlContext.Transforms.NormalizeMeanVariance(outputColumnName: nameof(TaxiTrip.PassengerCount)))
                            .Append(mlContext.Transforms.NormalizeMeanVariance(outputColumnName: nameof(TaxiTrip.TripTime)))
                            .Append(mlContext.Transforms.NormalizeMeanVariance(outputColumnName: nameof(TaxiTrip.TripDistance)))
                            .Append(mlContext.Transforms.Concatenate("Features", "VendorIdEncoded", "RateCodeEncoded", "PaymentTypeEncoded", nameof(TaxiTrip.PassengerCount)
                            , nameof(TaxiTrip.TripTime), nameof(TaxiTrip.TripDistance)));


// STEP 3: Set the training algorithm, then create and config the modelBuilder - Selected Trainer (SDCA Regression algorithm)                            
var trainer = mlContext.Regression.Trainers.Sdca(labelColumnName: "Label", featureColumnName: "Features");
var trainingPipeline = dataProcessPipeline.Append(trainer);


static IConfigurationRoot GetConfiguration()
{
    //Console.WriteLine($"Environment.CurrentDirectory is {Environment.CurrentDirectory}");
    //Console.WriteLine($"Directory.GetCurrentDirectory() is {Directory.GetCurrentDirectory()}");
    //Findings: They are the same for console app; but we should really be using AppContext.BaseDirectory
    //          This is because if we run the executable in a different directory, CurrentDirectory will reference that location

    var builder = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddUserSecrets("47df7034-c1c1-4b87-8373-89f5e42fc9ec")
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //A plain old console app does not have "env" (that comes from host builder)
        //Another way to check is use: https://stackoverflow.com/questions/1611410/how-to-check-if-a-app-is-in-debug-or-release
        //But that not strikes me particularly flexible either; 
        //KIV, until we find a better solution for simple console jobs
        //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        .AddEnvironmentVariables()
        ;

    return builder.Build();
}
