using LoadingScreenSimulator.ViewModels;

namespace LoadingScreenSimulator.Views
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel ViewModel { get; set; }

        public MainPage()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
            ViewModel.MainProgressBarLoadStarted += ViewModel_MainProgressBarLoadStarted;
            BindingContext = ViewModel;
        }

        #region Methods
        /// <summary>
        /// Animate the MainProgressBar based on the radiobutton selected.
        /// </summary>
        private async Task AnimateMainProgressBarAsync()
        {
            await MainProgressBar.ProgressTo(1, (uint)ViewModel.MainProgressBarAnimationLength, ViewModel.MainProgressBarAnimationEasing);
            await Task.Delay(ViewModel.MainProgressBarAnimationFinishDelay);
            ViewModel.MainProgressBarProgress = 0;
            ViewModel.MainProgressBarLoadCount += 1;
            ViewModel.IsMainProgressBarActivated = false;
            Preferences.Set(MainViewModel.SCORE_KEY, ViewModel.MainProgressBarLoadCount);
        }
        private async void ViewModel_MainProgressBarLoadStarted(object? sender, EventArgs e)
        {
            await AnimateMainProgressBarAsync();
        }
        #endregion
    }

}
