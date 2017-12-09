using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace NeelBotDemo.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog
    {
        public async Task StartAsync(IDialogContext context)
        {
            // My dialog initiates and waits for the next message from the user.              
            await context.PostAsync("Hi I am Movie Booking Bot created by Neel.");
            await Respond(context);
            // When a message arrives, call MessageReceivedAsync.  
            context.Wait(MessageReceivedAsync);
        }

        private static async Task Respond(IDialogContext context)
        {
            var userName = string.Empty;
            context.UserData.TryGetValue<string>("Name", out userName);
            if (string.IsNullOrEmpty(userName))
            {
                await context.PostAsync("What is your Name?");
                context.UserData.SetValue<bool>("GetName", true);
            }
            else
            {
                await context.PostAsync(string.Format("Hi {0}.", userName));
            }
        }
        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;// We've got a message!  
            var userName = String.Empty;
            var getName = false;
            context.UserData.TryGetValue<string>("Name", out userName);
            context.UserData.TryGetValue<bool>("GetName", out getName);
            if (getName)
            {
                userName = message.Text;
                context.UserData.SetValue<string>("Name", userName);
                context.UserData.SetValue<bool>("GetName", false);
            }
            await Respond(context);
            context.Done(message);
        }
    }
}
