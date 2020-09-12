using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.App
{
    public class ConfirmationModel : PageModel
    {
        public string Message { get; set; }

        public void OnGetContact()
        {
            Message = "Your email was sent.";
        }

        public void OnGetOrderSubmitted()
        {
            Message = "Your order submitted successfully.";
        }
    }
}