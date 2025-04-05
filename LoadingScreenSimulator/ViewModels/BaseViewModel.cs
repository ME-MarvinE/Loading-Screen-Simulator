using PropertyChanged;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LoadingScreenSimulator.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    /// <summary>
    /// A base class for ViewModels.
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Methods
        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion
    }
}
