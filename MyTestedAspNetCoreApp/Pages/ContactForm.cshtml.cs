using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MyTestedAspNetCoreApp.Pages
{
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class ContactFormModel : PageModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [BindProperty]
        public string Email { get; set; }

        [Required]
        [BindProperty]
        public string Name { get; set; }

        [Required]
        [BindProperty]
        public string Content { get; set; }

        [Required]
        [BindProperty]
        public string Title { get; set; }

        public string InfoMessage { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                this.InfoMessage = "Thank you for submitting contact form! ";
                //TODO: Send Email, etc.
            }
        }
    }
}
