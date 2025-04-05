using CommunityToolkit.Maui.Views;
using LoadingScreenSimulator.Popups;
using XCalendar.Core.Extensions;

namespace LoadingScreenSimulator.Helpers
{
    public static class PopupHelper
    {
        #region Properties
        public static List<DayOfWeek> AllDaysOfWeek { get; set; } = DayOfWeek.Monday.GetWeekAsFirst();
        public static List<Easing> AllEasings { get; set; } =
            [
                Easing.CubicIn,
                Easing.CubicOut,
                Easing.CubicInOut,
                Easing.SinIn,
                Easing.SinOut,
                Easing.SinInOut,
                Easing.BounceIn,
                Easing.BounceOut,
                Easing.SpringIn,
                Easing.SpringOut,
                Easing.Linear
            ];

        public static Dictionary<Easing, string> EasingToTextDict = new Dictionary<Easing, string>()
        {
                { Easing.CubicIn, "Cubic In" },
                { Easing.CubicOut, "Cubic Out" },
                { Easing.CubicInOut, "Cubic In Out" },
                { Easing.SinIn, "Sin In" },
                { Easing.SinOut, "Sin Out" },
                { Easing.SinInOut, "Sin In Out" },
                { Easing.BounceIn, "Bounce In" },
                { Easing.BounceOut, "Bounce Out" },
                { Easing.SpringIn, "Spring In "},
                { Easing.SpringOut, "Spring Out" },
                { Easing.Linear, "Linear" }
        };
        #endregion

        #region Methods
        public static async Task<T> ShowSelectItemDialogAsync<T>(T initialValue, IEnumerable<T> itemsSource)
        {
            return (T)await Shell.Current.ShowPopupAsync(new SelectItemDialogPopup(initialValue, itemsSource));
        }
        public static async Task<IEnumerable<T>> ShowConstructListDialogPopupAsync<T>(IEnumerable<T> initialValue, IEnumerable<T> daysOfWeek)
        {
            return ((List<object>)await Shell.Current.ShowPopupAsync(new ConstructListDialogPopup(initialValue, daysOfWeek))).Cast<T>();
        }
        public static async Task<Color> ShowColorDialogAsync(Color color)
        {
            return (Color)await Shell.Current.ShowPopupAsync(new ColorDialogPopup(color));
        }
        #endregion
    }
}
