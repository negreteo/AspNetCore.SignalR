using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace AspNetCore.SignalR.Hubs
{
   public class MessageHub : Hub
   {
      public Task SendMessageToAll (string message)
      {
         return Clients.All.SendAsync ("ReceiveMessage", message);
      }

      // Sends message back to who initiated the request coming from the hub
      public Task SendMessageToCaller (string message)
      {
         return Clients.Caller.SendAsync ("ReceiveMessage", message);
      }

      // Sends message to a specific connection
      public Task SendMessageToUser (string connectionId, string message)
      {
         return Clients.Client (connectionId).SendAsync ("ReceiveMessage", message);
      }

      // Adds a group to a connection
      public Task JoinGroup (string group)
      {
         return Groups.AddToGroupAsync (Context.ConnectionId, group);
      }

      // Sends message to a group
      public Task SendMessageToGroup (string group, string message)
      {
         return Clients.Group (group).SendAsync ("ReceiveMessage", message);
      }

      // A connection is made
      public override async Task OnConnectedAsync ()
      {
         // Tells all clients a connection has been made
         await Clients.All.SendAsync ("UserConnected", Context.ConnectionId);
         await base.OnConnectedAsync ();
      }

      // A connection is terminated
      public override async Task OnDisconnectedAsync (Exception ex)
      {
         // Tells all clients a disconnection has occurr
         await Clients.All.SendAsync ("UserDisconnected", Context.ConnectionId);
         await base.OnDisconnectedAsync (ex);
      }
   }
}
