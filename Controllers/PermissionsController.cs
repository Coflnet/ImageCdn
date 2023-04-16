using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Coflnet.ImageCdn.Controllers;

[ApiController]
[Route("[controller]")]
public class PermissionsController : ControllerBase
{

    private readonly ILogger<PermissionsController> _logger;

    public PermissionsController(ILogger<PermissionsController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<string> Get()
    {
        Ory.Keto.Client.Api.RelationshipApi api = new ("http://localhost:4467");
        var res = await api.CreateRelationshipWithHttpInfoAsync(new (_namespace:"videos", _object:"img", relation:"view", "ich"));
        _logger.LogInformation(res.StatusCode.ToString());
        _logger.LogInformation(res.Data.ToString());
        var checkApi = new Ory.Keto.Client.Api.PermissionApi("http://localhost:4466");
        var check = await checkApi.CheckPermissionAsync(_namespace:"videos", _object:"img", relation:"view", subjectId: "ich");
        //var check = checkApi.GetCheckAsync(_namespace:"videos", _object:"img", relation:"view", "ich", "videos");
        Console.WriteLine(check.Allowed);
        return JsonConvert.SerializeObject(check);
    }
}
