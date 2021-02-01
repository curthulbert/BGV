

namespace CutterLifeTracker.Utils
{
    public class ViewModelBase : NotifiableObject
    {
        //used to indicate that data is beig loaded
        //will cause the UI to display a progress bar
        //to let the user know something is actually happening
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged("IsBusy");  //send out a notification when IsBusy status changes
            }
        }
    }
}
