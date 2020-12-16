using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaberdin_PostItNotes.Data;
using Kaberdin_PostItNotes.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Kaberdin_PostItNotes.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        public IndexModel(ILogger<IndexModel> logger)
        {
            this.logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
