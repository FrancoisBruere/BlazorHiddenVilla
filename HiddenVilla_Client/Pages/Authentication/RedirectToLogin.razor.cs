using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Pages.Authentication
{
    public partial class RedirectToLogin
    {
        [Inject]
        private NavigationManager navigationManager { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> authenticationState { get; set; }
        bool notAuthorized { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            // get auth state check if user logged in 
            var authState = await authenticationState;

            if (authState?.User.Identity is null || !authState.User.Identity.IsAuthenticated) // user not loged in or isAuth should be false - user not logged in
            {
                var returnUrl = navigationManager.ToBaseRelativePath(navigationManager.Uri); // get return path
                if (string.IsNullOrEmpty(returnUrl))
                {
                    navigationManager.NavigateTo("login", true);

                }
                else
                {
                    navigationManager.NavigateTo($"login?returnUrl={returnUrl}", true);
                }
            }
            else
            {
                notAuthorized = true;

            }

            
        }

    }
}
