using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppStarter.Pages;

public class IndexModel : PageModel
{
    public string? CurrentDay { get; set; }
    
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        CurrentDay = DateTime.Now.ToString("dddd" , CultureInfo.InvariantCulture);
        //return Page();
    }
}