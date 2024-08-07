﻿namespace LoadingScreenSimulator
{
    public partial class MainPage : ContentPage
    {
        private int MainProgressBarLoadCount = 0;
        private bool _IsMainProgressBarActivated = false;
        private bool IsMainProgressBarActivated
        {
            get
            {
                return _IsMainProgressBarActivated;
            }
            set
            {
                _IsMainProgressBarActivated = value;
                BtnLoadMainProgressBar.IsEnabled = !value;
                BtnLoadMainProgressBar.BackgroundColor = value ? Color.FromArgb("#35D435") : Color.FromArgb("#D43535");
                BtnLoadMainProgressBar.Text = value ? "In Progress" : "Not Activated";
            }
        }

        public MainPage()
        {
            InitializeComponent();
            //Set the values of the sliders to the initial colour of MainProgressBar's progress colour.
            SliRed.Value = 250;
            SliGreen.Value = 82;
            SliBlue.Value = 82;
        }

        #region Methods
        /// <summary>
        /// Animate the MainProgressBar based on the radiobutton selected.
        /// </summary>
        private async Task AnimateMainProgressBarAsync()
        {
            uint ProgressBarAnimationLength = 3000;
            Easing SelectedEasingMode = Easing.CubicInOut;
            Dictionary<String, Easing> EasingModeOptions = new Dictionary<string, Easing>()
            {
                {"Cubic In", Easing.CubicIn},
                {"Cubic Out", Easing.CubicOut},
                {"Cubic In Out", Easing.CubicInOut},
                {"Sin In", Easing.SinIn},
                {"Sin Out", Easing.SinOut},
                {"Sin In Out", Easing.SinInOut},
                {"Bounce In", Easing.BounceIn},
                {"Bounce Out", Easing.BounceOut},
                {"Spring In", Easing.SpringIn},
                {"Spring Out", Easing.SpringOut},
                {"Linear", Easing.Linear}
            };

            //Update the easing mode according to whichever radiobutton is checked.
            foreach (RadioButton RadLoadOption in GrdLoadModeOptions.Children.OfType<RadioButton>().Where(x => x.IsChecked))
            {
                try
                {
                    SelectedEasingMode = EasingModeOptions[(string)RadLoadOption.Content];
                }
                catch
                {
                    await DisplayAlert("Error", "A non-existent load mode was selected", "Cancel");
                }
            }
            //Start the MainProgressBar animation and update the load count after it's done.
            await MainProgressBar.ProgressTo(1, ProgressBarAnimationLength, SelectedEasingMode);
            await Task.Delay(250);
            MainProgressBar.Progress = 0;
            MainProgressBarLoadCount += 1;
            IsMainProgressBarActivated = false;
            LblLoadCount.FormattedText.Spans[1].Text = MainProgressBarLoadCount.ToString();
        }
        /// <summary>
        /// Update MainProgressBar's progress colour and update the colour of the sliders to match it.
        /// </summary>
        private void UpdateMainProgressBarColour()
        {
            if (SliRed == null || SliGreen == null || SliBlue == null)
            {
                return;
            }
            MainProgressBar.ProgressColor = new Color(Convert.ToSingle(SliRed.Value / 255), Convert.ToSingle(SliGreen.Value / 255), Convert.ToSingle(SliBlue.Value / 255));
            SliRed.MaximumTrackColor = MainProgressBar.ProgressColor;
            SliGreen.MaximumTrackColor = MainProgressBar.ProgressColor;
            SliBlue.MaximumTrackColor = MainProgressBar.ProgressColor;
        }
        #endregion

        #region Event Handlers
        private async void BtnLoadMainProgressBar_Clicked(object sender, EventArgs e)
        {
            IsMainProgressBarActivated = !IsMainProgressBarActivated;
            if (IsMainProgressBarActivated)
            {
                await AnimateMainProgressBarAsync();
            }
        }
        private void SliRed_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            UpdateMainProgressBarColour();
        }
        private void SliGreen_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            UpdateMainProgressBarColour();
        }
        private void SliBlue_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            UpdateMainProgressBarColour();
        }
        #endregion
    }

}
