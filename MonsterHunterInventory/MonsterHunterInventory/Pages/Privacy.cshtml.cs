using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MonsterHunterInventory.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public string Message = "Intial Request";

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {



        }

        //On Request (Post/Get/Put) Handler (key word specific as form)

        public void OnPostEdit()
        {
            Message = "Edit Post";
        }

        public void OnPostDelete(int id)
        {
            Message = $"{id} was Deleted";
        }

        public void OnPostView(int id)
        {
            Message = $"{id} was Viewed";
        }
    }
}
