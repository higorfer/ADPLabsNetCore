// See https://aka.ms/new-console-template for more information

using ADPClient;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

while (true)
{
    // attempting to pretty print the json into console
    //var options = new JsonSerializerOptions
    //{
    //    WriteIndented = true
    //};

    // setting up HttpClient using url from launchSettings.json
    HttpClient client = new();
    client.BaseAddress = new Uri("https://localhost:7282");
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    HttpResponseMessage response = await client.GetAsync("/ProcessTask");
    //response.EnsureSuccessStatusCode();

    //var task = await response.Content.ReadFromJsonAsync<TaskTable>(); //should but does not work
    var task = await response.Content.ReadAsStringAsync();
    Console.WriteLine(task);

    Thread.Sleep(1000 * 60 * 1); // Sleep for a minute
}