using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Progress_Bar_Simulator
{
    public partial class MainPage : ContentPage
    {
        int MainProgressBarLoadCount = 0;
        private bool _MainButtonActivated = false;
        bool MainButtonActivated
        {
            get
            {
                return _MainButtonActivated;
            }
            set
            {
                _MainButtonActivated = value;
                MainButton.IsEnabled = !value;
                MainButton.BackgroundColor = value ? Color.FromHex("#35D435") : Color.FromHex("#D43535");
                MainButton.Text = value ? "In Progress" : "Not Activated";
            }
        }

        public MainPage()
        {
            InitializeComponent();
            SliRed.Value = 250;
            SliGreen.Value = 82;
            SliBlue.Value = 82;
        }
        private void MainButton_Clicked(object sender, EventArgs e)
        {
            ActivateProgressBar();
        }
        private void SliRed_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            MainProgressBar.ProgressColor = new Color(SliRed.Value / 255, SliGreen.Value / 255, SliBlue.Value / 255);
        }
        private void SliGreen_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            MainProgressBar.ProgressColor = new Color(SliRed.Value / 255, SliGreen.Value / 255, SliBlue.Value / 255);
        }
        private void SliBlue_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            MainProgressBar.ProgressColor = new Color(SliRed.Value / 255, SliGreen.Value / 255, SliBlue.Value / 255);
        }
        private async void ActivateProgressBar()
        {
            uint ProgressLength = 3000;
            MainButtonActivated = !MainButtonActivated;
            if (MainButtonActivated)
            {

                if (RadBounceIn.IsChecked)
                {
                    await MainProgressBar.ProgressTo(1, ProgressLength, Easing.BounceIn);
                }
                else if (RadBounceOut.IsChecked)
                {
                    await MainProgressBar.ProgressTo(1, ProgressLength, Easing.BounceOut);
                }
                else if (RadCubicIn.IsChecked)
                {
                    await MainProgressBar.ProgressTo(1, ProgressLength, Easing.CubicIn);
                }
                else if (RadCubicOut.IsChecked)
                {
                    await MainProgressBar.ProgressTo(1, ProgressLength, Easing.CubicOut);
                }
                else if (RadCubicInOut.IsChecked)
                {
                    await MainProgressBar.ProgressTo(1, ProgressLength, Easing.CubicInOut);
                }
                else if (RadLinear.IsChecked)
                {
                    await MainProgressBar.ProgressTo(1, ProgressLength, Easing.Linear);
                }
                else if (RadSinIn.IsChecked)
                {
                    await MainProgressBar.ProgressTo(1, ProgressLength, Easing.SinIn);
                }
                else if (RadSinOut.IsChecked)
                {
                    await MainProgressBar.ProgressTo(1, ProgressLength, Easing.SinOut);
                }
                else if (RadSinInOut.IsChecked)
                {
                    await MainProgressBar.ProgressTo(1, ProgressLength, Easing.SinInOut);
                }
                else if (RadSpringIn.IsChecked)
                {
                    await MainProgressBar.ProgressTo(1, ProgressLength, Easing.SpringIn);
                }
                else if (RadSpringOut.IsChecked)
                {
                    await MainProgressBar.ProgressTo(1, ProgressLength, Easing.SpringOut);
                }
                await Task.Delay(250);
                MainProgressBar.Progress = 0;
                MainProgressBarLoadCount += 1;
                MainButtonActivated = false;
                LblLoadCount.FormattedText.Spans[1].Text = MainProgressBarLoadCount.ToString();
            }
        }



    }
}
