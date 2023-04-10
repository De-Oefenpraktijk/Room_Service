using System.Net;

namespace Room_Service.Data
{
    public class SocialServiceData : ISocialServiceData
    {
        private string hostURI;
        public SocialServiceData(IConfiguration configuration)
        {
            hostURI = configuration.GetValue<string>("SocialService:URI");
        }

        public async Task<bool> IsUserValid(string id)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{hostURI}/Person/getUserById/{id}");
            var responseData = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
            return true;

        }
    }
}
