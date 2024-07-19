using RestSharp;
using RestSharpProject.Helpers;
using RestSharpProject.Config;

namespace RestSharpProject.Requests
{
    public class BoardRequests
    {
        private readonly RestClient _client;
        private readonly AuthHelper _authHelper;

        public BoardRequests()
        {
            _authHelper = new AuthHelper();
            _client = new RestClient(RequestConfig.BaseUrl);
        }

        public RestResponse GetBoards(string membersId)
        {
            var request = new RestRequest($"1/members/{membersId}/boards")
                .AddQueryParameter("fields", "id,name");
            request = _authHelper.AddKeyAndToken(request);
            return _client.Get(request);
        }

        public RestResponse GetBoard(string boardId)
        {
            var request = new RestRequest($"1/boards/{boardId}")
                .AddQueryParameter("fields", "id,name");
            request = _authHelper.AddKeyAndToken(request);
            return _client.Get(request);
        }

        public RestResponse GetBoardsInvalidId()
        {
            var request = new RestRequest($"1/boards/InvalidId");
            request = _authHelper.AddKeyAndToken(request);
            return _client.Get(request);
        }

        public RestResponse GetBoardsNoAuth(string membersId)
        {
            var request = new RestRequest($"1/members/{membersId}/boards")
                .AddQueryParameter("fields", "id,name")
                .AddQueryParameter("key", "invalidKey")
                .AddQueryParameter("token", "invalidToken");
            return _client.Get(request);
        }
    }
}
