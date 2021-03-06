using Blazored.LocalStorage;
using Common;
using HiddenVilla_Client.Service.IService;
using Microsoft.AspNetCore.Components.Authorization;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;


        public AuthenticationService(HttpClient client, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
        {
            _client = client;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }
        public async Task<AuthenticationResponseDTO> Login(AuthenticationDTO userForAuthentication)
        {
            var content = JsonConvert.SerializeObject(userForAuthentication);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var responce = await _client.PostAsync("api/account/signin", bodyContent);

            var contentTemp = await responce.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AuthenticationResponseDTO>(contentTemp);

            if (responce.IsSuccessStatusCode)
            {
                //user details to local storage
                await _localStorage.SetItemAsync(SD.Local_Token, result.Token);
                await _localStorage.SetItemAsync(SD.Local_UserDetails, result.UserDTO);
                ((AuthStateProvider)_authStateProvider).NotifyUserLoggedIn(result.Token);//convert Authstate and call notifyUSerlogin
                //((AuthStateProvider)_authStateProvider).NotifyUserLoggedIn(result.Token);//convert Authstate and call notifyUSerlogin

                //add bearer token
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

                //only return that login successful or now
                return new AuthenticationResponseDTO { isAuthSuccessful = true};
            }
            else //if login unsuccessful
            {
                return result;
            }
        }

        public async Task Logout()
        {
            //remove token and user details
            await _localStorage.RemoveItemAsync(SD.Local_Token);
            await _localStorage.RemoveItemAsync(SD.Local_UserDetails);
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout(); //convert Authstate and call notifyUSerlogout
            //set auth to null
            _client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<RegistrationResponceDTO> RegisterUser(UserRequestDTO userForRegistration)
        {
            var content = JsonConvert.SerializeObject(userForRegistration);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var responce = await _client.PostAsync("api/account/signup", bodyContent);
           

            var contentTemp = await responce.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RegistrationResponceDTO>(contentTemp);

            if (responce.IsSuccessStatusCode)
            {
                return new RegistrationResponceDTO { isRegistrationSuccessful = true };
            }
            else 
            {
                return result;
            }
        }
    }
}
