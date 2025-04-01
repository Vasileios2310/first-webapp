using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppStarter.Pages.Students;

public class Success : PageModel
{
    public string Message { get; set; }

    public void OnGet()
    {
        Message = TempData["Message"] as string ?? "Operation completed successfully.";
        
    }
}