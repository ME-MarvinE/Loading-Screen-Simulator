using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.PlatformConfiguration.AndroidSpecific;
using LoadingScreenSimulator.Themes;
using AppTheme = LoadingScreenSimulator.Models.Enums.AppTheme;

namespace LoadingScreenSimulator
{
    public partial class App : Application
    {
        #region Properties
        public static string PlayStoreUrl { get; } = "https://play.google.com/store/apps/details?id=com.leradonstudios.LoadingScreenSimulator";
        public static StatusBarBehavior StatusBarBehavior { get; set; } = new StatusBarBehavior() { StatusBarColor = Color.FromArgb("#586565") };
        public static Color NavigationBarColor { get; private set; } = Color.FromArgb("#251FBFB");
        public static NavigationBarStyle NavigationBarStyle { get; private set; } = NavigationBarStyle.Default;
        #endregion

        #region Constructors
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            SetStatusBarColor(StatusBarBehavior.StatusBarColor);
            RequestedThemeChanged += App_RequestedThemeChanged;
            PageAppearing += App_PageAppearing;
            UpdateAppThemeAccordingToOSTheme();
        }
        #endregion

        #region Methods
        public static void SetStatusBarColor(Color Color, StatusBarStyle? StatusBarStyle = null)
        {
            if (Current?.MainPage != null && !Current.MainPage.Behaviors.Contains(StatusBarBehavior))
            {
                Current?.MainPage.Behaviors.Add(StatusBarBehavior);
            }

            StatusBarBehavior.StatusBarColor = Color;

            if (StatusBarStyle != null)
            {
                StatusBarBehavior.StatusBarStyle = StatusBarStyle.Value;
            }
        }
        public static void SetNavigationBarColor(Color Color, NavigationBarStyle? NavigationBarStyle = null)
        {
            NavigationBarColor = Color;

            if (NavigationBarStyle != null)
            {
                App.NavigationBarStyle = NavigationBarStyle.Value;
            }

            if (Current?.MainPage != null)
            {
                NavigationBar.SetColor(Current.MainPage, NavigationBarColor);
                NavigationBar.SetStyle(Current.MainPage, App.NavigationBarStyle);
            }
        }
        public static async Task OpenUrl(string Url)
        {
            //Using OpenAsync instead of TryOpenAsync due to it never working and always returning false.
            try
            {
                await Launcher.OpenAsync(Url);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Something went wrong when loading the URL, please try again. If this doesn't work, try restarting the app. If that doesn't work, try reinstalling the app.", "Ok");
            }

            //bool UrlOpened = await Launcher.TryOpenAsync(Url);

            //if (!UrlOpened)
            //{
            //    await Shell.Current.DisplayAlert("Error", "Something went wrong when loading the URL, please try again. If this doesn't work, try restarting the app. If that doesn't work, try reinstalling the app.", "Ok");
            //}
        }
        /// <summary>
        /// Sets the <see cref="ResourceDictionary"/> used to theme the app.
        /// </summary>
        /// <param name="Theme">The theme who's <see cref="ResourceDictionary"/> will be used.</param>
        public static void SetAppTheme(AppTheme Theme)
        {
            ICollection<ResourceDictionary> MergedDictionaries = Current.Resources.MergedDictionaries;
            StatusBarStyle StatusBarStyle;
            bool IsLightTheme;
            MergedDictionaries.Clear();

            switch (Theme)
            {
                case AppTheme.Red:
                    MergedDictionaries.Add(new RedTheme());
                    StatusBarStyle = StatusBarStyle.LightContent;
                    IsLightTheme = true;
                    break;

                case AppTheme.RedDark:
                    MergedDictionaries.Add(new RedDarkTheme());
                    StatusBarStyle = StatusBarStyle.LightContent;
                    IsLightTheme = false;
                    break;

                case AppTheme.Blue:
                    MergedDictionaries.Add(new BlueTheme());
                    StatusBarStyle = StatusBarStyle.LightContent;
                    IsLightTheme = true;
                    break;

                case AppTheme.BlueDark:
                    MergedDictionaries.Add(new BlueDarkTheme());
                    StatusBarStyle = StatusBarStyle.LightContent;
                    IsLightTheme = false;
                    break;

                case AppTheme.Green:
                    MergedDictionaries.Add(new GreenTheme());
                    StatusBarStyle = StatusBarStyle.LightContent;
                    IsLightTheme = true;
                    break;

                case AppTheme.GreenDark:
                    MergedDictionaries.Add(new GreenDarkTheme());
                    StatusBarStyle = StatusBarStyle.LightContent;
                    IsLightTheme = false;
                    break;

                default:
                    throw new NotImplementedException($"There is no implementation corresponding to theme '{Theme}'");
            }

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                ResourceDictionary CurrentTheme = Current.Resources.MergedDictionaries.FirstOrDefault(x => x.ContainsKey("PrimaryColor"));
                Color PrimaryColor;
                Color PageBackgroundColor;

                if (CurrentTheme.TryGetValue("PrimaryColor", out object PrimaryColorObject))
                {
                    PrimaryColor = (Color)PrimaryColorObject;
                }
                else
                {
                    PrimaryColor = Colors.Black;
                }

                if (CurrentTheme.TryGetValue("PageBackgroundColor", out object PageBackgroundColorObject))
                {
                    PageBackgroundColor = (Color)PageBackgroundColorObject;
                }
                else
                {
                    PageBackgroundColor = Colors.White;
                }

                SetStatusBarColor(PrimaryColor, StatusBarStyle);
                SetNavigationBarColor(IsLightTheme ? Color.FromArgb("#FBFBFB") : Color.FromArgb("#040404"), IsLightTheme ? NavigationBarStyle.DarkContent : NavigationBarStyle.LightContent);
            }
        }
        public void UpdateAppThemeAccordingToOSTheme()
        {
            if (RequestedTheme == Microsoft.Maui.ApplicationModel.AppTheme.Light)
            {
                SetAppTheme(AppTheme.Blue);
            }
            else if (RequestedTheme == Microsoft.Maui.ApplicationModel.AppTheme.Dark)
            {
                SetAppTheme(AppTheme.BlueDark);
            }
        }
        private static void App_PageAppearing(object sender, Page e)
        {
            if (!e.Behaviors.Contains(StatusBarBehavior))
            {
                e.Behaviors.Add(StatusBarBehavior);
            }

            NavigationBar.SetColor(e, NavigationBarColor);
            NavigationBar.SetStyle(e, NavigationBarStyle);
        }
        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            UpdateAppThemeAccordingToOSTheme();
        }
        #endregion
    }
}
