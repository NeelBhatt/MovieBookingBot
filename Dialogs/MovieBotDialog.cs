using BotFlightEnquiry.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BotFlightEnquiry.Dialogs
{
    public class MovieBotDialog
    {
        public static readonly IDialog<string> dialog = Chain.PostToChain()
            .Select(msg => msg.Text)
            .Switch(
            new RegexCase<IDialog<string>>(new Regex("^hi", RegexOptions.IgnoreCase), (context, text) =>
            {
                return Chain.ContinueWith(new RootDialog(), AfterMyDialogContinue);
            }),
            new DefaultCase<string, IDialog<string>>((context, text) =>
            {
                return Chain.ContinueWith(FormDialog.FromForm(MovieBooking.BuildForm, FormOptions.PromptInStart), AfterMyDialogContinue);
            }))
            .Unwrap()
            .PostToUser();

        private async static Task<IDialog<string>> AfterMyDialogContinue(IBotContext context, IAwaitable<object> item)
        {
            var token = await item;
            var name = "User";
            context.UserData.TryGetValue<string>("Name", out name);
            return Chain.Return($"Please continue typing if you want to book a movie ticket.");
        }
    }
}