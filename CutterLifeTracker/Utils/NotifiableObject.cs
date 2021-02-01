using System.ComponentModel;

namespace CutterLifeTracker.Utils
{
    public class NotifiableObject : INotifyPropertyChanged
    {
        //used to notify properties in the UI that they have changed
        //this is how they know that they need to update
        public event PropertyChangedEventHandler PropertyChanged;

        //send out a notification when the poperty status changes
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged(params string[] propertyNames)
        {
            foreach (string propertyName in propertyNames)
            {
                OnPropertyChanged(propertyName);
            }
        }
    }
}
