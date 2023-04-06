using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using OpenQA.Selenium;

var authToken = "";
using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/json"));
client.DefaultRequestHeaders.Add("User-Agent", "Proteus Memo-Count Modifier");
client.DefaultRequestHeaders.Authorization =  new AuthenticationHeaderValue("Bearer", authToken);


HttpContent httpContent = new StringContent("External API Call");
httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue ("application/json"); 

await ProcessRepositoriesAsync(client, httpContent);

static async Task ProcessRepositoriesAsync(HttpClient client, HttpContent httpContent)
{
    var queryParam = string.Empty;
    var response = await client.GetAsync($"url/{queryParam}"); 

    //await client.PostAsync($"url/{queryParam}", httpContent); -- Post Async
    //await client.PutAsync($"url/{queryParam}", httpContent); -- Put Async

    var responseStatus = response.StatusCode == System.Net.HttpStatusCode.OK ? "Success" : "Failed";

    string folder = @"C:\Temp\";
    string fileName = "APICallStatus.txt";
    string fullPath = folder + fileName;
    if(!File.Exists(fullPath))
    {
        File.Create(fullPath);
    }

    string readText = File.ReadAllText(fullPath);
    readText += $"url/{queryParam} - {responseStatus} - {response?.ReasonPhrase}";
    File.WriteAllText(fullPath, readText);

    Console.WriteLine($"*****Completed API Call******");
}

//  static async Task<string> GetAccessTken()
//  {
//      IWebDriver driver; 
//      driver = LaunchBrowser("chrome");
//      IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
//      var keyToken = (string)js.ExecuteAsyncScript("return window.sessionStorage.key('access-token')");
//      var authToken = (staring)js.ExecuteAsyncScript("return window.sessionStorage.getItem('" + keyToken + "')");
//      return "";
//  }