using System.Collections.Generic;
using System.Linq;
using CutterLifeTracker.Model;
using CutterLifeTracker.Services;
using CutterLifeTracker.Utils;
using Microsoft.Windows.Data.DomainServices;
using Toolsheet_Library.Web.EntityModel;

namespace CutterLifeTracker.ViewModel
{
    public class CutterHistoryReportViewModel : ViewModelBase
    {
        private ICutterDataService CutterDataService { get; set; }

        public DelegateCommand SelectedCutterChangedCommand { get; set; }
        public DelegateCommand SelectedCutterInstanceChangedCommand { get; set; }


        #region Properties        
        
        

        private CutterHistoryDataModel _cutterHistoryData;
        public CutterHistoryDataModel CutterHistoryData
        {
            get { return _cutterHistoryData; }
            set
            {
                if (_cutterHistoryData != value)
                {
                    _cutterHistoryData = value;
                    OnPropertyChanged("CutterHistoryData");
                }
            }
        }

        private CutterInstance _selectedCutterInstance;
        public CutterInstance SelectedCutterInstance
        {
            get { return _selectedCutterInstance; }
            set
            {
                if (_selectedCutterInstance != value)
                {
                    _selectedCutterInstance = value;
                    OnPropertyChanged("SelectedCutterInstance");
                }
            }
        }

        private List<CutterInstance> _cutterInstanceCollection;
        public List<CutterInstance> CutterInstanceCollection
        {
            get { return _cutterInstanceCollection; }
            set
            {
                if (_cutterInstanceCollection != value)
                {
                    _cutterInstanceCollection = value;
                    OnPropertyChanged("CutterInstanceCollection");
                }
            }
        }

        private DomainCollectionView<Cutter> _cutterCollection;
        public DomainCollectionView<Cutter> CutterCollection
        {
            get { return _cutterCollection; }
            set
            {
                if (_cutterCollection != value)
                {
                    _cutterCollection = value;
                    OnPropertyChanged("CutterCollection");
                }
            }
        }

        private Cutter _selectedCutter;
        public Cutter SelectedCutter
        {
            get { return _selectedCutter; }
            set
            {
                if (_selectedCutter != value)
                {
                    _selectedCutter = value;
                    OnPropertyChanged("SelectedCutter");
                }
            }
        }


        #endregion

        public CutterHistoryReportViewModel(ICutterDataService cutterDataService)
        {
            CutterDataService = cutterDataService;

            RegisterCommands();
            LoadCutters();
        }

        private void RegisterCommands()
        {
            SelectedCutterChangedCommand = new DelegateCommand(HandleSelectedCutterChanged);
            SelectedCutterInstanceChangedCommand = new DelegateCommand(HandleSelectedCutterInstanceChanged);
        }

        private void LoadCutters()
        {
            IsBusy = true;
            CutterDataService.GetAllCutters(20, GetAllCuttersCallback);
        }

        private void GetAllCuttersCallback(DomainCollectionView<Cutter> cutters)
        {
            if (CollectionContainsData(cutters))
            {
                CutterCollection = cutters;

                //set the selected cutter and the cutterInstance
                SelectedCutter = CutterCollection.First();
            }
            IsBusy = false;
        }

        private bool CollectionContainsData(DomainCollectionView<Cutter> cutters)
        {
            return ((cutters != null) && (!cutters.IsEmpty) );
        }

        private void HandleSelectedCutterInstanceChanged(object parameter)
        {
            DisplaySelectedCutterProperties();
        }

        private void DisplaySelectedCutterProperties()
        {
            if (SelectedCutterInstance != null)
            {
                CutterDataService.GetPanelsWithSpecifiedCutterIdAndChangeNumber(SelectedCutterInstance, PanelsWorkedOnByCutterInstance);
            }
        }

        private void PanelsWorkedOnByCutterInstance(IEnumerable<PanelReportView> panels)
        {
            CutterHistoryData = new CutterHistoryDataModel(SelectedCutterInstance, panels);
        }

        private void HandleSelectedCutterChanged(object parameter)
        {
            DisplaySelectedCutterInstances();
        }

        private void DisplaySelectedCutterInstances()
        {
            if (CutterInstanceCollection == null)
            {
                CutterInstanceCollection = new List<CutterInstance>();
            }

            if (SelectedCutter != null)
            {
                CutterInstanceCollection.Clear();
                CutterInstanceCollection = SelectedCutter.CutterInstances
                                                         .Where(c => c.CutterID == SelectedCutter.CutterID)
                                                         .OrderBy(c => c.ChangeNumber)
                                                         .ToList();
            }
        }
    }
}