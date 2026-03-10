using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function;

public class HttpTrigger1Function
{
    private readonly ILogger<HttpTrigger1Function> _logger;

    public HttpTrigger1Function(ILogger<HttpTrigger1Function> logger)
    {
        _logger = logger;
    }

    [Function("HttpTrigger1Function")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        string? name = req.Query["name"];
        string requewstBody = await new StreamReader(req.Body).ReadToEndAsync();
        dynamic? data = !string.IsNullOrEmpty(requewstBody) ? JsonConvert.DeserializeObject(requewstBody) : null;
        name ??= data?.name;
        string responseMessage = string.IsNullOrEmpty(name)
            ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            : $"Hello, {name}. This HTTP triggered function executed successfully.  Trying from a deleted branch";

        return new OkObjectResult(responseMessage);
    }
}