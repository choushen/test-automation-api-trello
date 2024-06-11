using System;
using RestSharp;

namespace RestSharpProject.Helpers
{
    public class AuthHelper
    {

        // PROPERTIEs
        private static string _key = Environment.GetEnvironmentVariable("TRELLO_RESTSHARP_KEY") ?? "key";
        private static string _token = Environment.GetEnvironmentVariable("TRELLO_RESTSHARP_TOKEN") ?? "token";


        // METHODS
        public RestRequest AddKeyAndToken(RestRequest request) 
        {
            var key = _key;
            request.AddQueryParameter("key", _key);
            request.AddQueryParameter("token", _token);
            return request;
        } // AddKeyAndToken end

    }
}
