using RestSharp;
using RestSharpProject.Helpers;
using RestSharpProject.Config;

namespace RestSharpProject.Requests
{
    public class ListRequests
    {
        private readonly RestClient _client;
        private readonly AuthHelper _authHelper;

        public ListRequests()
        {
            _authHelper = new AuthHelper();
            _client = new RestClient(RequestConfig.BaseUrl);
        }

        public RestResponse GetLists(string boardId)
        {
            var request = new RestRequest($"/1/boards/{boardId}/lists")
                .AddQueryParameter("fields", "id,name");
            request = _authHelper.AddKeyAndToken(request);
            return _client.Get(request);
        }

        public RestResponse GetListsInvalidId()
        {
            var request = new RestRequest("/1/boards/InvalidId/lists")
                .AddQueryParameter("fields", "id,name");
            request = _authHelper.AddKeyAndToken(request);
            return _client.Get(request);
        }

        public RestResponse GetListsNoAuth(string boardId)
        {
            var request = new RestRequest($"/1/boards/{boardId}/lists")
                .AddQueryParameter("fields", "id,name")
                .AddQueryParameter("key", "invalidKey")
                .AddQueryParameter("token", "invalidToken");
            return _client.Get(request);
        }
    }
}
