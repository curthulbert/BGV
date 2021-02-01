using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using CutterLifeTracker.Model;
using CutterLifeTracker.Services;
using CutterLifeTracker.Utils;
using Toolsheet_Library.Web.EntityModel;

namespace CutterLifeTracker.ViewModel
{
    public class HoleCountViewModel : ViewModelBase
    {              
        #region Properties        
        
        public IHoleCountReportDataService HoleCountReportDataService { get; set; }

        private HoleCountContainerDataModel _projects;
        private ObservableCollection<CutterCostView> CutterCostViewCollection { get; set; }

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

        private PagedCollectionView _projectCollectionView;
        public PagedCollectionView ProjectCollectionView
        {
            get { return _projectCollectionView; }
            set
            {
                if (_projectCollectionView != value)
                {
                    _projectCollectionView = value;
                    OnPropertyChanged("ProjectCollectionView");
                }
            }
        }

        private ObservableCollection<string> _machineNumbers;
        public ObservableCollection<string> MachineNumbers
        {
            get { return _machineNumbers; }
            set
            {
                if (_machineNumbers != value)
                {
                    _machineNumbers = value;
                    OnPropertyChanged("MachineNumbers");
                }
            }
        }

        private int _selectedMill;
        public int SelectedMill
        {
            get { return _selectedMill; }
            set
            {
                if (_selectedMill != value)
                {
                    _selectedMill = value;
                    OnPropertyChanged("SelectedMill");

                    LoadData();
                }
            }
        }

        private ObservableCollection<int> _headerColumnsCollection;
        public ObservableCollection<int> HeaderColumnsCollection
        {
            get { return _headerColumnsCollection; }
            set
            {
                if (_headerColumnsCollection != value)
                {
                    _headerColumnsCollection = value;
                    OnPropertyChanged("HeaderColumnsCollection");
                }
            }
        }

        #endregion

        /*
         * The goal for this report is to know the discrete cutters used on each panel.
         * 
         * Each row of the report is going to 
         * list the distinct CutterID used in a given panel 
         * where each panel is made up of ProjectID + PartID
         */        

        public HoleCountViewModel(            
            IHoleCountReportDataService holeCountReportDataService
            )
        {
            HoleCountReportDataService = holeCountReportDataService;                       

            InitializeModels();        
           
            //set mill list                                    
            LoadMillComboBox();
        }        

        private void InitializeModels()
        {
            ProjectCollection = new ObservableCollection<HoleCountDataModel>();
            CutterCostViewCollection = new ObservableCollection<CutterCostView>();
            HeaderColumnsCollection = new ObservableCollection<int>();
            MachineNumbers = new ObservableCollection<string>();
            _projects = new HoleCountContainerDataModel();
        }

        #region Load Data

        private void LoadMillComboBox()
        {
            IsBusy = true;

            MachineNumbers.Clear();
            HoleCountReportDataService.GetMillsForHoleCountReport(GetMillsCallback);
        }

        private void GetMillsCallback(IEnumerable<int> mills)
        {
            if (mills != null)
            {
                foreach (var mill in mills)
                    MachineNumbers.Add(mill.ToString());
            }

            IsBusy = false;
        }

        private void LoadData()
        {
            IsBusy = true;
            CutterCostViewCollection.Clear();
            HoleCountReportDataService.GetProjectInfoByMillQuery(SelectedMill, GetProjectInfoByMillCallback);           
        }
       
        private void GetProjectInfoByMillCallback(IEnumerable<CutterCostView> projects)
        {
            //get list of column headers
            LoadHeaderData();

            if (projects != null)
            {
                foreach (var project in projects)
                    CutterCostViewCollection.Add(project);

                LoadShapedData();
            }            
        }

        private void LoadHeaderData()
        {
            HoleCountReportDataService.GetHeaderData(SelectedMill, GetHeaderDataCallback);
        }

        private void GetHeaderDataCallback(IEnumerable<int> headerData)
        {
            HeaderColumnsCollection.Clear();
            if (headerData != null)
            {
                foreach (int h in headerData)
                    HeaderColumnsCollection.Add(h);
            }            
        }        

        #endregion

        #region Shape Data

        private void LoadShapedData()
        {            
            _projects.GetShapedData(CutterCostViewCollection, OnShapedDataLoaded);            
        }

        private void OnShapedDataLoaded(IEnumerable<HoleCountDataModel> projectCollection)
        {
            ProjectCollection.Clear();

            if (projectCollection != null)
            {
                foreach (var project in projectCollection)                
                    ProjectCollection.Add(project);                
            }

            //by converting this to a PagedCollectionView we are able to easily apply
            //filtering, sorting & paging if we wish.
            if(ProjectCollection.Count > 0)
                ProjectCollectionView = new PagedCollectionView(ProjectCollection);
            
            IsBusy = false;
        }

        #endregion        
    }
}
