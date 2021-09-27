using HiddenVilla_Client.Service.IService;
using Microsoft.AspNetCore.Components;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Pages.Authentication
{
    public partial class Register
    {

        private UserRequestDTO UserForRegistration = new UserRequestDTO();
        public bool IsProcessing { get; set; } = false;
        public bool ShowRegistrationErros { get; set; }

        public IEnumerable<string> Errors { get; set; }

        [Inject]
        public IAuthenticationService authenticationService { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }

        private async Task RegisterUser()
        {

            ShowRegistrationErros = false;
            IsProcessing = true;

            var result = await authenticationService.RegisterUser(UserForRegistration);



            if (result.isRegistrationSuccessful)
            {
                IsProcessing = false;
                navigationManager.NavigateTo("/login");


            }
            else
            {
                IsProcessing = false;
                Errors = result.Errors;
                ShowRegistrationErros = true;

            }

        }



    }
}
