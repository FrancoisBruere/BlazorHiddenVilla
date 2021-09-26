using Blazored.LocalStorage;
using Common;
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

        public AuthenticationService(HttpClient client, ILocalStorageService localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }
        public async Task<AuthenticationResponseDTO> Login(AuthenticationDTO userForAuthentication)
        {
            var content = JsonConvert.SerializeObject(userForAuthentication);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var responce = await _client.PostAsync("api/accountcontroller/signin", bodyContent);

            var contentTemp = await responce.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AuthenticationResponseDTO>(contentTemp);

            if (responce.IsSuccessStatusCode)
            {
                //user details to local storage
                await _localStorage.SetItemAsync(SD.Local_Token, result.Token);
                await _localStorage.SetItemAsync(SD.Local_UserDetails, result.UserDTO);

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

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public Task<RegistrationResponceDTO> RegisterUser(UserRequestDTO userForRegistration)
        {
            throw new NotImplementedException();
        }
    }
}
