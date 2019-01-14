using System.Threading.Tasks;
using AspNetCore.SignalR.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AspNetCore.SignalR.Controllers
{
   public class AnnouncementController : Controller
   {
      private IHubContext<MessageHub> _hubContext;

      public AnnouncementController (IHubContext<MessageHub> hubContext)
      {
         _hubContext = hubContext;
      }

      [HttpGet ("/announcement")]
      public IActionResult Index ()
      {
         return View ();
      }

      [HttpPost ("/announcement")]
      public async Task<IActionResult> Post ([FromForm] string message)
      {
         await _hubContext.Clients.All.SendAsync ("ReceiveMessage", message);
         return RedirectToAction ("Index");
      }
   }
}