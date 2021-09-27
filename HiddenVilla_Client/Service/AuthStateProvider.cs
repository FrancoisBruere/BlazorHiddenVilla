using Blazored.LocalStorage;
using Common;
using HiddenVilla_Client.Helper;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service
{
    public class AuthStateProvider:AuthenticationStateProvider
    {

        private readonly HttpClient _httpclient;
        private readonly ILocalStorageService _localStorage;
        public AuthStateProvider(HttpClient client, ILocalStorageService localStorage)
        {
            _httpclient = client;
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>(SD.Local_Token); // retrive token from local storage
            if (token == null) // pass anonom request
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token); //add bearer token to http client
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
        }

        public void NotifyUserLoggedIn( string token)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser)); // creating new authentication state and pass claims token
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))); // set auth state to new and will clear existing auth state
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
