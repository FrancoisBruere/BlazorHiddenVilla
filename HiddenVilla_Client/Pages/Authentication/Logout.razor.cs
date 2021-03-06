using HiddenVilla_Client.Service.IService;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Pages.Authentication
{
    public partial class Logout
    {

        [Inject]
        public IAuthenticationService authenticationService { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await authenticationService.Logout();
            navigationManager.NavigateTo("/");
        }
    }
}
