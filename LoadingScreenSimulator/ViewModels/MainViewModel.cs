using LoadingScreenSimulator.Helpers;
using PropertyChanged;
using System.Windows.Input;

namespace LoadingScreenSimulator.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        #region Fields
        public const string SCORE_KEY = "Score";
        #endregion

        #region Properties
        [OnChangedMethod(nameof(OnMainProgressBarColorChanged))]
        public Color MainProgressBarColor { get; set; }

        [OnChangedMethod(nameof(OnMainProgressBarColorRedChanged))]
        public float MainProgressBarColorRed { get; set; }
        [OnChangedMethod(nameof(OnMainProgressBarColorGreenChanged))]
        public float MainProgressBarColorGreen { get; set; }
        [OnChangedMethod(nameof(OnMainProgressBarColorBlueChanged))]
        public float MainProgressBarColorBlue { get; set; }
        public bool IsChangingColor { get; set; }
        public int MainProgressBarLoadCount { get; set; }
        public bool IsMainProgressBarActivated { get; set; }
        public double MainProgressBarProgress { get; set; }
        [OnChangedMethod(nameof(OnProgressBarAnimationEasingChanged))]
        public Easing MainProgressBarAnimationEasing { get; set; }
        public string MainProgressBarAnimationEasingText { get; private set; }
        public int MainProgressBarAnimationLength { get; set; } = 3000;
        public int MainProgressBarAnimationFinishDelay { get; set; } = 150;
        #endregion

        #region Events
        public event EventHandler MainProgressBarLoadStarted;
        #endregion

        #region Commands
        public ICommand ShowMainProgressBarColorDialogCommand { get; set; }
        public ICommand StartProgressBarLoadCommand { get; set; }
        public ICommand ShowSelectAnimationDialogCommand { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            ShowMainProgressBarColorDialogCommand = new Command<Color>(ShowMainProgressBarColorDialog);
            StartProgressBarLoadCommand = new Command(StartProgressBarLoad);
            ShowSelectAnimationDialogCommand = new Command<Easing>(ShowSelectAnimationDialog);

            MainProgressBarAnimationEasing = Easing.CubicInOut;
            MainProgressBarColor = Color.FromArgb("#FFFA5252");
            MainProgressBarLoadCount = Preferences.Get(SCORE_KEY, 0);
        }
        #endregion

        #region Methods
        public void StartProgressBarLoad()
        {
            OnMainProgressBarLoadStarted(this, EventArgs.Empty);
        }
        public async void ShowMainProgressBarColorDialog(Color initialColor)
        {
            MainProgressBarColor = await PopupHelper.ShowColorDialogAsync(initialColor);
        }
        public async void ShowSelectAnimationDialog(Easing initialSelectedEasing)
        {
            string selectedEasingText = await PopupHelper.ShowSelectItemDialogAsync(PopupHelper.EasingToTextDict[initialSelectedEasing], PopupHelper.AllEasings.Select(x => PopupHelper.EasingToTextDict[x]));
            MainProgressBarAnimationEasing = PopupHelper.EasingToTextDict.FirstOrDefault(x => x.Value == selectedEasingText).Key ?? initialSelectedEasing;
        }
        public void OnMainProgressBarColorChanged()
        {
            IsChangingColor = true;
            MainProgressBarColorRed = MainProgressBarColor.Red;
            MainProgressBarColorGreen = MainProgressBarColor.Green;
            MainProgressBarColorBlue = MainProgressBarColor.Blue;
            IsChangingColor = false;
        }
        public void OnMainProgressBarColorRedChanged()
        {
            if (!IsChangingColor)
            {
                MainProgressBarColor = new Color(MainProgressBarColorRed, MainProgressBarColorGreen, MainProgressBarColorBlue, MainProgressBarColor.Alpha);
            }
        }
        public void OnMainProgressBarColorGreenChanged()
        {
            if (!IsChangingColor)
            {
                MainProgressBarColor = new Color(MainProgressBarColorRed, MainProgressBarColorGreen, MainProgressBarColorBlue, MainProgressBarColor.Alpha);
            }
        }
        public void OnMainProgressBarColorBlueChanged()
        {
            if (!IsChangingColor)
            {
                MainProgressBarColor = new Color(MainProgressBarColorRed, MainProgressBarColorGreen, MainProgressBarColorBlue, MainProgressBarColor.Alpha);
            }
        }
        public void OnProgressBarAnimationEasingChanged()
        {
            MainProgressBarAnimationEasingText = PopupHelper.EasingToTextDict.GetValueOrDefault(MainProgressBarAnimationEasing, null);
        }
        protected virtual void OnMainProgressBarLoadStarted(object sender, EventArgs e)
        {
            IsMainProgressBarActivated = !IsMainProgressBarActivated;
            MainProgressBarLoadStarted?.Invoke(sender, e);
        }
        #endregion
    }
}
