using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Linq;
using CutterLifeTracker.Model;
using CutterLifeTracker.Services;
using CutterLifeTracker.Utils;
using Toolsheet_Library.Web.EntityModel;
using Toolsheet_Library.Web.Service;

namespace CutterLifeTracker.ViewModel
{
    public class PanelReportViewModel : ViewModelBase
    {
        #region Properties

        public ICutterDataService CutterDataService { get; set; }
        public IPanelReportDataService PanelReportDataService { get; set; }        
        public IJSFSkyNetDataService JSFSKyNetDataService { get; set; }

        #region Command Properties

        //Command Properties  
        public DelegateCommand CutterIdSelectionChangedCommand { get; set; }
        public DelegateCommand CutterIdFilterResetButtonCommand { get; set; }

        #endregion

        private PanelReportContainerDataModel _panels;
        private ObservableCollection<PanelReportView> PanelReportViewCollection { get; set; }
        private ObservableCollection<PanelReportDataModel> PanelCollection { get; set; }

        private PagedCollectionView _panelCollectionView;
        public PagedCollectionView PanelCollectionView
        {
            get { return _panelCollectionView; }
            set
            {
                if (_panelCollectionView != value)
                {
                    _panelCollectionView = value;
                    OnPropertyChanged("PanelCollectionView");
                }
            }
        }

        private ObservableCollection<int> _cutterIdCollection;
        public ObservableCollection<int> CutterIdCollection
        {
            get { return _cutterIdCollection; }
            set
            {
                if (_cutterIdCollection != value)
                {
                    _cutterIdCollection = value;
                    OnPropertyChanged("CutterIdCollection");
                }
            }
        }

        private int _selectedCutterId;
        public int SelectedCutterId
        {
            get { return _selectedCutterId; }
            set
            {
                if (_selectedCutterId != value)
                {
                    _selectedCutterId = value;
                    OnPropertyChanged("SelectedCutterId");
                }
            }
        }

        #endregion

        public PanelReportViewModel(            
            ICutterDataService cutterDataService,
            IPanelReportDataService panelReportDataService,
            IJSFSkyNetDataService jsfSkyNetDataService
            )
        {
            CutterDataService = cutterDataService;
            PanelReportDataService = panelReportDataService;
            JSFSKyNetDataService = jsfSkyNetDataService;

            InitializeModel();
            RegisterCommands(); //register delegate commands for buttons & behaviors

            LoadPanelData();
        }

        #region Initialization

        private void InitializeModel()
        {
            PanelReportViewCollection = new ObservableCollection<PanelReportView>();
            PanelCollection = new ObservableCollection<PanelReportDataModel>();
            CutterIdCollection = new ObservableCollection<int>();

            _panels = new PanelReportContainerDataModel() { JSFSkyNetDataService = this.JSFSKyNetDataService };
        }

        private void RegisterCommands()
        {
            CutterIdSelectionChangedCommand = new DelegateCommand(HandleSelectedCutterIdChanged);
            CutterIdFilterResetButtonCommand = new DelegateCommand(HandleFilterResetButtonClicked);
        }

        #endregion

        #region Load Data

        private void LoadPanelData()
        {
            IsBusy = true;
            PanelReportDataService.GetCutterIdsForPanelViewReport(GetCutterIdsCallback);
        }

        private void GetCutterIdsCallback(IEnumerable<int> cutterIds)
        {
            CutterIdCollection.Clear();

            if (cutterIds != null)
            {
                foreach (var cutterId in cutterIds)
                    CutterIdCollection.Add(cutterId);
            }
            PanelReportDataService.GetPanelReport(GetPanelDataCallback);
        }

        private void GetPanelDataCallback(IEnumerable<PanelReportView> panels)
        {
            PanelReportViewCollection.Clear();

            if (panels != null)
            {
                foreach (var panel in panels)
                    PanelReportViewCollection.Add(panel);

                if (PanelReportViewCollection.Count > 0)
                    LoadShapedData();
                else
                    IsBusy = false;
            }
        }

        #endregion

        #region Load Shaped Data

        private void LoadShapedData()
        {
            _panels.GetShapedData(PanelReportViewCollection, OnShapedDataLoaded);
        }

        private void OnShapedDataLoaded(IEnumerable<PanelReportDataModel> panelCollection)
        {
            PanelCollection.Clear();

            if (panelCollection != null)
            {
                foreach (var panel in panelCollection)
                    PanelCollection.Add(panel);

                // By turning the ObservableCollection in to a PagedCollectionView we can 
                // easily filter the results.                                  
                if (PanelCollection.Count > 0)
                    PanelCollectionView = new PagedCollectionView(PanelCollection);
            }
            IsBusy = false;
        }

        #endregion

        #region Filter Results

        private void LoadFilteredPanels()
        {
            PanelCollectionView.Filter = null;
            PanelCollectionView.Filter = new Predicate<object>(FilterOnCutterId);
        }

        private bool FilterOnCutterId(object obj)
        {
            /*
             * In order to display all the cutters used
             * on a given panel, what we are really need to filter
             * on is the PanelID.             
             */
            PanelReportDataModel m = (PanelReportDataModel)obj;

            //get a list of panel Ids for panels that use the given cutterId
            var panelIds = PanelReportViewCollection.Where(p => p.CutterID == SelectedCutterId)
                                                    .Select(p => p.PanelID)
                                                    .Distinct();

            //return true if the panelId is in the distinct filtered collection
            return panelIds.Contains(m.PanelID);
        }

        #endregion

        #region Handle UI events

        private void HandleSelectedCutterIdChanged(object parameter)
        {
            if (PanelCollectionView != null)
                LoadFilteredPanels();
        }

        private void HandleFilterResetButtonClicked(object parameter)
        {
            if (PanelCollectionView != null)
                PanelCollectionView.Filter = null;
        }

        #endregion
    }
}
