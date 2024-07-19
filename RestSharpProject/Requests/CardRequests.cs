using RestSharp;
using RestSharpProject.Helpers;
using RestSharpProject.Config;

namespace RestSharpProject.Requests
{
    public class CardRequests
    {
        private readonly RestClient _client;
        private readonly AuthHelper _authHelper;

        public CardRequests()
        {
            _authHelper = new AuthHelper();
            _client = new RestClient(RequestConfig.BaseUrl);
        }

        public RestResponse GetCards(string listId)
        {
            var request = new RestRequest($"1/lists/{listId}/cards")
                .AddQueryParameter("fields", "id,name,desc");
            request = _authHelper.AddKeyAndToken(request);
            return _client.Get(request);
        }

        public RestResponse GetCard(string cardId)
        {
            var request = new RestRequest($"1/cards/{cardId}")
                .AddQueryParameter("fields", "id,name,desc");
            request = _authHelper.AddKeyAndToken(request);
            return _client.Get(request);
        }

        public RestResponse GetCardInvalidId()
        {
            var request = new RestRequest($"1/cards/InvalidId")
                .AddQueryParameter("fields", "id,name,desc");
            request = _authHelper.AddKeyAndToken(request);
            return _client.Get(request);
        }

        public RestResponse GetCardNoAuth(string cardId)
        {
            var request = new RestRequest($"1/cards/{cardId}")
                .AddQueryParameter("fields", "id,name,desc")
                .AddQueryParameter("key", "invalidKey")
                .AddQueryParameter("token", "invalidToken");
            return _client.Get(request);
        }
    }
}
