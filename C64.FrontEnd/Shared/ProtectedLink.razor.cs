using C64.FrontEnd.Extensions;
using Microsoft.AspNetCore.Components;

namespace C64.FrontEnd.Shared
{
    public partial class ProtectedLink : ComponentBase
    {
        protected bool CheckAccess()
        {
            if (!httpContextAccessor.HttpContext.IsLoggedIn())
            {
                Modal.Show<LoginModal>();
                return false;
            }

            if (!httpContextAccessor.HttpContext.CanEdit())
            {
                navigationManager.NavigateTo("/help/contribute");
                toastService.ShowInfo("Editing requires an updated account - please drop us a note to have your account updated!");
                return false;
            }

            return true;
        }
    }
}