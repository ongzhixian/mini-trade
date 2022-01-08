using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiniTrade.ConsoleApp.Exceptions;
using MiniTrade.ConsoleApp.Models;
using MiniTrade.ConsoleApp.Models.Oanda;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text.Json;

namespace MiniTrade.ConsoleApp.Services;

public class OandaApiService
{
    private readonly ILogger<OandaApiService> logger;
    private readonly IOptions<OandaAccountSetting> options;
    private readonly HttpClient httpClient;

    private readonly JsonSerializerOptions jsonSerializerOptions;

    public OandaApiService(
        ILogger<OandaApiService> logger
        , IOptions<OandaAccountSetting> options
        , HttpClient httpClient
        , IOptions<JsonSerializerOptions> jsonSerializerOptions
        )
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.options = options ?? throw new ArgumentNullException(nameof(options));
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        this.jsonSerializerOptions = jsonSerializerOptions.Value ?? throw new ArgumentNullException(nameof(jsonSerializerOptions));

        this.httpClient.BaseAddress = new Uri(options.Value.ApiUrl);
        this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.Value.ApiKey);
        this.httpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(MediaTypeNames.Application.Json));
    }

    internal async Task<AccountsResponse> GetAccountsAsync(CancellationToken stopToken)
    {
        string url = $"{options.Value.ApiUrl}/v3/accounts";

        using HttpResponseMessage? httpResponse = await httpClient.GetAsync(url, stopToken);

        httpResponse.EnsureSuccessStatusCode();

        using Stream? strm = await httpResponse.Content.ReadAsStreamAsync(stopToken);

        AccountsResponse? result =
            await JsonSerializer.DeserializeAsync<AccountsResponse>(strm, jsonSerializerOptions, stopToken);

        result = null;
        throw new ObjectNullException<AccountsResponse>(nameof(result));

        if (result == null)
            throw new ObjectNullException<AccountsResponse>(nameof(result));

        return result;
    }


#pragma warning disable S125 // Sections of code should not be commented out

    // Examples of using HttpClient

    //internal async Task<AccountsResponse> GetAccountsAsync2(CancellationToken stopToken)
    //{
    //    // Similar implementation as `GetAccountsAsync`
    //    // Uses `ReadAsStringAsync` instead of `ReadAsStreamAsync` to read response content

    //    string url = $"{options.Value.ApiUrl}/v3/accounts";

    //    using HttpResponseMessage? httpResponse = await httpClient.GetAsync(url, stopToken);

    //    string json = await httpResponse.Content.ReadAsStringAsync(stopToken);

    //    AccountsResponse? result = JsonSerializer.Deserialize<AccountsResponse>(json, jsonSerializerOptions);

    //    if (result == null)
    //        throw new NullResultException(result);

    //    return result;

    //    // Alternate syntax (use this syntax is we want to specify request content-type 

    //    //using (var request = new HttpRequestMessage(HttpMethod.Get, url))
    //    //{
    //    //    //request.Content = new StringContent(
    //    //    //    "{\"name\":\"John Doe\",\"age\":33}", 
    //    //    //    Encoding.UTF8,
    //    //    //    MediaTypeNames.Application.Json);//CONTENT-TYPE header

    //    //    var response = await httpClient.SendAsync(request);

    //    //    response.EnsureSuccessStatusCode();

    //    //    var json = await response.Content.ReadAsStringAsync();

    //    //    try
    //    //    {
    //    //        // Async
    //    //        var result = JsonSerializer.Deserialize<AccountsResponse>(json);
    //    //        Console.WriteLine(result);
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        Console.WriteLine(ex);
    //    //        throw;
    //    //    }
    //    //}


    //}

    //internal async Task<AccountsResponse> GetAccountsAsync3(CancellationToken stopToken)
    //{
    //    // Similar implementation as `GetAccountsAsync`
    //    // Use `SendAsync` to send a HttpRequestMessage
    //    // The key benefit is to set a `Content-Type` for the content being sent

    //    string url = $"{options.Value.ApiUrl}/v3/accounts";

    //    using HttpRequestMessage requestMessage = new(HttpMethod.Get, url);

    //    //request.Content = new StringContent(
    //    //    "{\"name\":\"John Doe\",\"age\":33}",
    //    //    Encoding.UTF8,
    //    //    MediaTypeNames.Application.Json);//CONTENT-TYPE header

    //    using HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage);

    //    responseMessage.EnsureSuccessStatusCode();

    //    using Stream? strm = await responseMessage.Content.ReadAsStreamAsync(stopToken);

    //    AccountsResponse? result =
    //        await JsonSerializer.DeserializeAsync<AccountsResponse>(strm, jsonSerializerOptions, stopToken);

    //    if (result == null)
    //        throw new NullResultException(result);

    //    return result;
    //}

#pragma warning restore S125 // Sections of code should not be commented out

}
