using LMS.Blazor.Client.Models;
using LMS.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace LMS.Blazor.Client.Services;

public class ClientApiService(IHttpClientFactory httpClientFactory, NavigationManager navigationManager) : IApiService
{
    private readonly HttpClient httpClient = httpClientFactory.CreateClient("BffClient");

    private readonly JsonSerializerOptions _jsonSerializerOptions = new ()
        { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public async Task<TResponse?> CallApiAsync<TResponse>(string endpoint) 
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"proxy-endpoint/{endpoint}");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response = await httpClient.SendAsync(request);

        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden
           || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            navigationManager.NavigateTo("AccessDenied");
        }

        response.EnsureSuccessStatusCode();

        var res = await JsonSerializer.DeserializeAsync<TResponse>(await response.Content.ReadAsStreamAsync(), _jsonSerializerOptions, CancellationToken.None);
        return res;
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest dto)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"proxy-endpoint/{endpoint}");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //Not much difference from the CallAPIAsync method you can easy combine to one.
        //Only thease 3 lines is added and of course Post instead for get
        //Or make overloads with private helper method for clarity
        var serializedLecture = JsonSerializer.Serialize(dto);
        request.Content = new StringContent(serializedLecture);
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");


        var response = await httpClient.SendAsync(request);

        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden
           || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            navigationManager.NavigateTo("AccessDenied");
        }

        response.EnsureSuccessStatusCode();

        var res = await JsonSerializer.DeserializeAsync<TResponse>(await response.Content.ReadAsStreamAsync(), _jsonSerializerOptions, CancellationToken.None);
        return res;
    }
}
