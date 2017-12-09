using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;

namespace NeelBotDemo.FormFlow
{
    [Serializable]
    public class MovieBooking
    {
        public MovieTheatreLocation movieTheatreLocation;
        public MovieTheatre movieTheatre;
        public MovieTypes movieTypes;
        public ClassTypes classTypes;
        [Optional]
        public DoYouNeedAMeal doYouNeedAMeal;
        public FoodMenu foodMenu;
        public DateTime? Date;
        [Numeric(1, 5)]
        public int? NumberOfAdult;

        public int? NumberOfChild;

        public static IForm<MovieBooking> BuildForm()
        {
            return new FormBuilder<MovieBooking>()
                .Message("Welcome to the Movie Booking BOT created by Neel.")
                 .OnCompletion(async (context, profileForm) =>
                 {
                     var userName = string.Empty;
                     context.UserData.TryGetValue<string>("Name", out userName);
                     // Tell the user that the form is complete  
                     await context.PostAsync("Thanks for the confirmation " + userName + ". Your booking is successfull");
                 })
                .Build();
        }
    }

    [Serializable]
    public enum MovieTheatreLocation
    {
        Pune = 1, Bangalore = 2, Mumbai = 3
    }
    [Serializable]
    public enum MovieTheatre
    {
        PVR = 1, INOX = 2, CinePolis = 3
    }
    [Serializable]
    public enum MovieTypes
    {
        Hindi = 1, English = 2, Regional = 3
    }
    [Serializable]
    public enum ClassTypes
    {
        PlatinumClass = 1,
        GoldClass = 2,
        Economy = 3
    }
    [Serializable]
    public enum DoYouNeedAMeal
    {
        Yes = 1,
        No = 2
    }
    [Serializable]
    public enum FoodMenu
    {
        Sandwich = 1,
        Noodles = 2,
        Samosa = 3,
        Cookies = 4,
        Juice = 5,
        Tea = 6,
        Coffee = 7
    }
}
