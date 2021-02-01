using System.Collections.ObjectModel;
using CutterLifeTracker.Utils;

namespace CutterLifeTracker.Model
{
    public class HoleCountDataModel : NotifiableObject
    {
        private string _projectID;
        public string ProjectID
        {
            get { return _projectID; }
            set
            {
                if (_projectID != value)
                {
                    _projectID = value;
                    OnPropertyChanged("ProjectID");
                }
            }
        }

        private ObservableCollection<string> _sum;
        public ObservableCollection<string> Sum
        {
            get { return _sum; }
            set
            {
                if (_sum != value)
                {
                    _sum = value;
                    OnPropertyChanged("Sum");
                }
            }
        }

        public HoleCountDataModel()
        {
            InitializeModel();
        }

        private void InitializeModel()
        {
            Sum = new ObservableCollection<string>();
        }
    }
}
