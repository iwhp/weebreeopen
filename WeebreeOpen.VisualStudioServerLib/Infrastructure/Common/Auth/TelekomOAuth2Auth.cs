// This file is part of the Telekom .NET SDK
// Copyright 2010 Deutsche Telekom AG
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telekom.Common.Auth.OAuth2;
using Telekom.Common.WebRequest;
using Telekom.Common.Model;

namespace Telekom.Common.Auth
{
    /// <summary>
    /// Authentication to Telekom services with OAuth2 (redirection)
    /// </summary>
    public class TelekomOAuth2Auth : TelekomAuth
    {
        /// <summary>
        /// URL to OAuth server. Can be overwritten.
        /// </summary>
        public static string BaseUrl = "https://global.telekom.com/gcp-web-api";

        /// <summary>
        /// Your client ID at telekom services
        /// </summary>
        public string ClientId { get; private set; }

        /// <summary>
        /// Your client secret at telekom services
        /// </summary>
        public string ClientSecret { get; private set; }

        /// <summary>
        /// Your scope at telekom services
        /// </summary>
        public string Scope { get; private set; }

        /// <summary>
        /// Create a new OAuth2 authentication object without tokens
        /// </summary>
        /// <param name="clientId">Your application's OAuth client ID</param>
        /// <param name="clientSecret">Your application's OAuth secret (null if none)</param> 
        /// <param name="scope">Your application's OAuth scope</param> 
        public TelekomOAuth2Auth(string clientId, string clientSecret, string scope)
            : base()
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            Scope = scope;
        }

        /// <summary>
        /// Restore an OAuth2 authentication from an already authenticated session
        /// </summary>
        /// <param name="clientId">Your application's OAuth client ID</param>
        /// <param name="clientSecret">Your application's OAuth secret (null if none)</param>/// 
        /// <param name="accessToken">Saved access token for Telekom services</param>
        /// <param name="accessTokenValidUntil">Saved validity of the access token</param>
        /// <param name="scope">Your application's OAuth scope</param> 
        public TelekomOAuth2Auth(string clientId, string clientSecret, string accessToken, DateTime accessTokenValidUntil, string scope)
            : base(accessToken, accessTokenValidUntil)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            Scope = scope;
        }

        private void ParseAccessTokenResponse(AccessTokenResponse response)
        {
            AccessToken = response.AccessToken;
            AccessTokenValidUntil = DateTime.Now.AddSeconds(response.ExpiresIn);
        }

        private TelekomJsonWebRequest<AccessTokenResponse> CreateRequestAccessTokenParams()
        {
            string uri = BaseUrl + "/oauth";

            TelekomJsonWebRequest<AccessTokenResponse> request = new TelekomJsonWebRequest<AccessTokenResponse>(uri, HttpMethod.POST);

            string credentials = String.Format("{0}:{1}", ClientId, ClientSecret);
            byte[] bytes = Encoding.UTF8.GetBytes(credentials);
            string base64 = Convert.ToBase64String(bytes);
            string authorization = String.Concat("Basic ", base64);
            request.AuthHeader = authorization;

            System.Net.NetworkCredential nc = new System.Net.NetworkCredential(ClientId, ClientSecret);
            request.Credentials = nc;

            // convert string to stream
            byte[] byteArray = Encoding.UTF8.GetBytes("grant_type=client_credentials&scope=" + Scope);
            //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
            System.IO.MemoryStream streamData = new System.IO.MemoryStream(byteArray);

            request.SetRawContent(streamData, "application/x-www-form-urlencoded");

            return request;
        }

        /// <summary>
        /// Request the token to access Telekom services
        /// </summary>
        public void RequestAccessToken()
        {
            var webRequest = CreateRequestAccessTokenParams();
            var response = webRequest.Execute();

            if (response != null)
            {
                ParseAccessTokenResponse(response);
            }
        }

        /// <summary>
        /// Returns if there is a valid access token.
        /// </summary>
        /// <returns>if there is a valid access token</returns>
        public override bool HasValidToken()
        {
            // Is the current access token still valid?
            if (base.HasValidToken())
            {
                // it is
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns if there is a valid access token (async).
        /// </summary>
        /// <param name="callback">Method to call with the result if there is a valid token</param>
        public override void HasValidToken(Action<bool> callback)
        {
            // Is the current access token still valid?
            if (base.HasValidToken())
            {
                // it is
                callback(true);
            }
            else
            {
                callback(false);
            }
        }
    }
}
