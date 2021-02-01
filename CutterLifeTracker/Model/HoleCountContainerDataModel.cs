using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CutterLifeTracker.Utils;
using Toolsheet_Library.Web.EntityModel;

namespace CutterLifeTracker.Model
{
    public class HoleCountContainerDataModel : NotifiableObject
    {
        private Action<IEnumerable<HoleCountDataModel>> _getShapedDataCallback;
        private IEnumerable<CutterCostView> _cutterCostCollection;

        private ObservableCollection<HoleCountDataModel> _projectCollection;
        public ObservableCollection<HoleCountDataModel> ProjectCollection
        {
            get { return _projectCollection; }
            set
            {
                if (_projectCollection != value)
                {
                    _projectCollection = value;
                    OnPropertyChanged("ProjectCollection");
                }
            }
        }

        public HoleCountContainerDataModel()
        {
            InitializeModel();
        }

        private void InitializeModel()
        {
            ProjectCollection = new ObservableCollection<HoleCountDataModel>();
        }

        public void GetShapedData(IEnumerable<CutterCostView> cutterCostCollection, Action<IEnumerable<HoleCountDataModel>> getShapedDataCallback)
        {
            ProjectCollection.Clear();

            if (cutterCostCollection != null)
            {
                _cutterCostCollection = cutterCostCollection;
                _getShapedDataCallback = getShapedDataCallback;

                ShapeProjectCollection();
            }
        }

        private void ShapeProjectCollection()
        {
            //group records by ProjectID
            var panels = from c in _cutterCostCollection
                         group c by c.ProjectID into p
                         select new { ProjectID = p.Key, CutterList = p };

            List<int> cutterTypeIDs = _cutterCostCollection
                                            .OrderBy(i => i.CutterID)
                                            .Select(i => i.CutterID)
                                            .Distinct()
                                            .ToList();

            foreach (var panel in panels)
            {
                //create a panel to attach the cutter list to
                HoleCountDataModel newPanel = new HoleCountDataModel();

                newPanel.ProjectID = panel.ProjectID;

                /*
                     * If the project doesn't use one of the
                     * cutter types listed, we don't want to
                     * display a sum below the cutter.
                     * Instead of displaying a zero, we won't
                     * display anything.
                     * 
                     * In order to do this the list of cutterIDs
                     * is made up of strings. If the panel
                     * doesn't call for a given cutter I
                     * set the value in List<string> Sum to
                     * an empty string.
                     * To accomplish this I set a default
                     * value for the computed value (0) and
                     * an empty string for the value to be
                     * added.                    
                */
                foreach (int cutter in cutterTypeIDs)
                {
                    double cost = 0;
                    string sum = "0";

                    var query = panel.CutterList
                                       .Where(i => i.CutterID == cutter);

                    if (query != null)
                    {
                        cost = query.Select(i => i.CutterCost).Sum();
                        sum = String.Format("{0:F0}", cost);
                    }

                    newPanel.Sum.Add(sum);
                }
                ProjectCollection.Add(newPanel);
                newPanel = null;
            }
            _getShapedDataCallback(ProjectCollection);
        }
    }
}
