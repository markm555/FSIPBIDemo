using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FSIPBIDemo.Models;
using FSIPBIDemo.Services;

namespace FSIPBIDemo.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

         private PowerBiServiceApi powerBiServiceApi; 
         public HomeController(PowerBiServiceApi powerBiServiceApi) { 
         this.powerBiServiceApi = powerBiServiceApi;
         }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        /*
        public async Task<IActionResult> Embed() { 
 
        // replace these two GUIDs with the workspace ID and report ID you recorded earlier 
        Guid workspaceId = new Guid("2d038cde-39d8-405d-a57a-72eacf4d19b3"); 
        Guid reportId = new Guid("3b5ed814-fd17-4d30-8732-289a396aaa11"); 
 
        var viewModel = await powerBiServiceApi.GetReport(workspaceId, reportId); 
 
        return View(viewModel); 
        } 
        */

        [AllowAnonymous]
        public async Task<IActionResult> Demo(string workspaceId) { 
 
        try { 
             Guid guidTest = new Guid(workspaceId); 
             var viewModel = await this.powerBiServiceApi.GetEmbeddedViewModel(workspaceId); 
             return View(viewModel as object); 
            } 
        catch { 
            var firstWorkspace = await this.powerBiServiceApi.GetFirstWorkspace(); 
            if (firstWorkspace == null) { 
            return RedirectToPage("/Error"); 
            } 
        else { 
            return RedirectToPage("/Demo", null, new { workspaceId = firstWorkspace.Id }); 
        } 
    } 
} 

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
