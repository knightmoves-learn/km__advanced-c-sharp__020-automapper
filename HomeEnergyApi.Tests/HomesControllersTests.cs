using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using System.Text.Json;
using System.Dynamic;
using System.Data.SQLite;
using HomeEnergyApi.Models;
using HomeEnergyApi.Controllers;
using HomeEnergyApi.Tests.Extensions;

[TestCaseOrderer("HomeEnergyApi.Tests.Extensions.PriorityOrderer", "HomeEnergyApi.Tests")]
public class HomesControllersTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private HomeDto testHomeDto = new();

    public HomesControllersTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory, TestPriority(1)]
    [InlineData("/Homes")]
    public async Task HomeEnergyApiReturnsSuccessfulHTTPResponseCodeOnGETHomes(string url)
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(url);

        Assert.True(response.IsSuccessStatusCode,
            $"HomeEnergyApi did not return successful HTTP Response Code on GET request at {url}; instead received {(int)response.StatusCode}: {response.StatusCode}");
    }

    [Theory, TestPriority(2)]
    [InlineData("admin/Homes/Location/50313")]
    public async Task ZipLocationServiceRespondsWith200CodeAndPlaceWhenGivenValidZipCode(string url)
    {
        var client = _factory.CreateClient();

        var response = await client.PostAsync(url, null);

        Assert.True((int)response.StatusCode == 200,
             $"HomeEnergyApi did not return \"200: OK\" HTTP Response Code on POST request at {url}; instead received {(int)response.StatusCode}: {response.StatusCode}");

        string responseContent = await response.Content.ReadAsStringAsync();
        bool hasPlace = responseContent.Contains("\"place name\":\"Des Moines\"");
        bool hasState = responseContent.Contains("\"state\":\"Iowa\"");


        Assert.True(hasPlace && hasState,
            $"HomeEnergyApi did not return the expected `place name` and `state` from POST request at {url}\nExpected: `place name` of `Des Moines` and `state` of `Iowa`\nReceived: {responseContent}");
    }

    [Theory, TestPriority(3)]
    [InlineData("/admin/Homes")]
    public async Task HomeEnergyApiCanPOSTAHomeGivenAValidHomeDto(string url)
    {
        var client = _factory.CreateClient();

        HomeDto postTestHomeDto = testHomeDto;
        postTestHomeDto.OwnerLastName = "Test";
        postTestHomeDto.StreetAddress = "123 Test St.";
        postTestHomeDto.City = "Test City";
        postTestHomeDto.MonthlyElectricUsage = 123;
        postTestHomeDto.ProvidedUtilities = new List<string>() { "gas", "electric"};

        string strPostTestHomeDto = JsonSerializer.Serialize(postTestHomeDto);

        HttpRequestMessage sendRequest = new HttpRequestMessage(HttpMethod.Post, url);
        sendRequest.Content = new StringContent(strPostTestHomeDto,
                                                Encoding.UTF8,
                                                "application/json");

        var response = await client.SendAsync(sendRequest);
        Assert.True((int)response.StatusCode == 201,
            $"HomeEnergyApi did not return \"201: Created\" HTTP Response Code on POST request at {url}; instead received {(int)response.StatusCode}: {response.StatusCode}");

        string responseContent = await response.Content.ReadAsStringAsync();
        responseContent = responseContent.ToLower();

        bool ownerLastMatch = responseContent.Contains("\"ownerlastname\":\"test\"");
        bool streetAddMatch = responseContent.Contains("\"streetaddress\":\"123 test st.\"");
        bool cityMatch = responseContent.Contains("\"city\":\"test city\"");

        string homeUsageResponse = responseContent.Substring(responseContent.IndexOf("homeusagedata"), responseContent.IndexOf("utilityproviders") - responseContent.IndexOf("homeusagedata"));
        bool monthlyUsageMatch = homeUsageResponse.Contains("\"monthlyelectricusage\":123,");

        string utilityResponse = responseContent.Substring(responseContent.IndexOf("utilityproviders"));
        bool utilityMatch = utilityResponse.Contains("\"providedutility\":\"electric\"") && utilityResponse.Contains("\"providedutility\":\"gas\"");

        bool hasExpected = ownerLastMatch && streetAddMatch && cityMatch && monthlyUsageMatch && utilityMatch;

        Assert.True(hasExpected,
            $"Home Energy Api did not return the correct Home being created on POST at {url}\nHomeDto Sent: {strPostTestHomeDto}\nHome Received:{responseContent}");
    
    }

    [Theory, TestPriority(3)]
    [InlineData("/admin/Homes")]
    public async Task HomeEnergyApiCanPUTAHomeGivenAValidHomeDto(string url)
    {
        var client = _factory.CreateClient();

        var getAllResponse = await client.GetAsync("/Homes");
        string getAllResponseStr = await getAllResponse.Content.ReadAsStringAsync();
        dynamic getAllResponseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(getAllResponseStr);
        string urlId = getAllResponseObj[getAllResponseObj.Count - 1].Id;
        url = url + $"/{urlId}";

        HomeDto putTestHomeDto = testHomeDto;
        putTestHomeDto.OwnerLastName = "Putty";
        putTestHomeDto.StreetAddress = "123 Put St.";
        putTestHomeDto.City = "Put City";
        putTestHomeDto.MonthlyElectricUsage = 456;
        putTestHomeDto.ProvidedUtilities = new List<string>() { "water", "recycling"};

        string strPutTestHomeDto = JsonSerializer.Serialize(putTestHomeDto);

        HttpRequestMessage sendRequest = new HttpRequestMessage(HttpMethod.Put, url);
        sendRequest.Content = new StringContent(strPutTestHomeDto,
                                                Encoding.UTF8,
                                                "application/json");

        var response = await client.SendAsync(sendRequest);
        Assert.True((int)response.StatusCode == 200,
            $"HomeEnergyApi did not return \"200: Ok\" HTTP Response Code on PUT request at {url}; instead received {(int)response.StatusCode}: {response.StatusCode}");

        string responseContent = await response.Content.ReadAsStringAsync();
        responseContent = responseContent.ToLower();

        bool ownerLastMatch = responseContent.Contains("\"ownerlastname\":\"putty\"");
        bool streetAddMatch = responseContent.Contains("\"streetaddress\":\"123 put st.\"");
        bool cityMatch = responseContent.Contains("\"city\":\"put city\"");

        string homeUsageResponse = responseContent.Substring(responseContent.IndexOf("homeusagedata"), responseContent.IndexOf("utilityproviders") - responseContent.IndexOf("homeusagedata"));
        bool monthlyUsageMatch = homeUsageResponse.Contains("\"monthlyelectricusage\":456,");

        string utilityResponse = responseContent.Substring(responseContent.IndexOf("utilityproviders"));
        bool utilityMatch = utilityResponse.Contains("\"providedutility\":\"water\"") && utilityResponse.Contains("\"providedutility\":\"recycling\"");

        bool hasExpected = ownerLastMatch && streetAddMatch && cityMatch && monthlyUsageMatch && utilityMatch;

        Assert.True(hasExpected,
            $"Home Energy Api did not return the correct Home being updated on PUT at {url}\nHomeDto Sent: {strPutTestHomeDto}\nHome Received:{responseContent}");
    }
}
