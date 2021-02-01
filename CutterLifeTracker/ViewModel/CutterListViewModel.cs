using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using System.Text;
using System.Windows;
using CutterLifeTracker.Messages;
using CutterLifeTracker.Services;
using CutterLifeTracker.Utils;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Windows.Data.DomainServices;
using Toolsheet_Library.Web.EntityModel;

namespace CutterLifeTracker.ViewModel
{
    public class CutterListViewModel : ViewModelBase
    {
        #region Properties
        
        private IDataService DataService { get; set; }
        private ICutterDataService CutterDataService { get; set; }
        private readonly MediatorService _ms;

        #region Command Properties
        //Command Properties        

        public DelegateCommand SubmitButtonCommand { get; set; }
        public DelegateCommand AddNewCutterTypeButtonCommand { get; set; }
        public DelegateCommand DiscardButtonCommand { get; set; }        
        public DelegateCommand SelectedCutterChangedCommand { get; set; }
        public DelegateCommand MillSelectionChangedCommand { get; set; }
        public DelegateCommand DeleteCutterTypeButtonCommand { get; set; }

        #endregion        
        
        /* 
         * used when creating a new cutter
         * if the user is viewing cutters
         * the current mill value will be used
         */ 
        private readonly int DEFAULT_MILL = 6;
        
        //sets the size of the page for data paging
        private readonly int PAGE_SIZE = 20;

        #region Collections        

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

        private ObservableCollection<string> _machineNumbers;
        public ObservableCollection<string> MachineNumbers
        {
            get { return _machineNumbers; }
            set
            {
                _machineNumbers = value;
                OnPropertyChanged("MachineNumbers"); //send out a notification when List<string>MachineNumbers changes
            }
        }

        #endregion

        #region Boolean Control Properties                

        private bool _enableSubmitButton;
        public bool EnableSubmitButton
        {
            get { return _enableSubmitButton; }
            set
            {
                if (_enableSubmitButton != value)
                {
                    _enableSubmitButton = value;
                    OnPropertyChanged("EnableSubmitButton");
                }
            }
        }

        private bool _hasChanges;
        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged("HasChanges");
                }
            }
        }

        #endregion

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

        #region Add New Cutter Properties

        //used when creating a new Cutter & CutterInsance.
        //A message is sent to the mediator for creating the AddNewCutterWindow
        private NewCutterContainerMessage NewCutterContainer { get; set; }        

        //passed to AddNewCutter window
        //when user creates a new cutter
        private Cutter _newCutter;
        public Cutter NewCutter
        {
            get { return _newCutter; }
            set
            {
                if (_newCutter != value)
                {
                    _newCutter = value;
                    OnPropertyChanged("NewCutter");
                }
            }
        }

        //passed to AddNewCutter window
        //when user creates a new cutter
        private CutterInstance _newCutterInstance;
        public CutterInstance NewCutterInstance
        {
            get { return _newCutterInstance; }
            set
            {
                if (_newCutterInstance != value)
                {
                    _newCutterInstance = value;
                    OnPropertyChanged("NewCutterInstance");
                }
            }
        }

        #endregion
        
        #endregion

        #region Constructor

        public CutterListViewModel(
            IDataService dataService,
            ICutterDataService cutterDataService)
        {            
            DataService = dataService;
            CutterDataService = cutterDataService;
            _ms = MediatorService.Instance;


            IsBusy = false;
            
            EnableSubmitButton = false;            

            //listen for property changes to the context
            //this will allow us to set properties based on
            //whether the context has been changed
            DataService.NotifyHasChanges += DataService_NotifyHasChanges;
            CutterDataService.NotifyHasChanges += DataService_NotifyHasChanges;

            InitializeModel();  //initialize collections used in the VM
            RegisterCommands(); //register delegate commands for buttons & behaviors

            LoadMillComboBox();
        }

        #endregion

        #region Initialization

        private void InitializeModel()
        {                        
            MachineNumbers = new ObservableCollection<string>();            
        }

        private void DataService_NotifyHasChanges(object sender, HasChangesEventArgs e)
        {
            HasChanges = e.HasChanges;

            if (HasChanges)
            {
                EnableSubmitButton = true;
                SubmitButtonCommand.RaiseCanExecuteChanged();               
            }
            else
                EnableSubmitButton = false;
        }

        private void RegisterCommands()
        {
            SubmitButtonCommand = new DelegateCommand(HandleSubmitButtonClicked, SubmitButtonIsEnabled);
            AddNewCutterTypeButtonCommand = new DelegateCommand(HandleAddNewCutterTypeButtonClicked);            
            SelectedCutterChangedCommand = new DelegateCommand(HandleSelectedCutterChanged);
            MillSelectionChangedCommand = new DelegateCommand(HandleSelectedMillChanged);
            DeleteCutterTypeButtonCommand = new DelegateCommand(HandleDeleteCutterTypeButtonClicked);
        }

        #endregion

        #region Load Data

        public void LoadMillComboBox()
        {
            IsBusy = true;

            MachineNumbers.Clear();
            CutterDataService.GetMillsForCutterListReport(GetMillsCallback);
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

        public void LoadCutters()
        {
            IsBusy = true;
            CutterDataService.GetCuttersByMill(SelectedMill, PAGE_SIZE, GetCuttersCallback);
        }
        
        private void GetCuttersCallback(DomainCollectionView<Cutter> cutters)
        {                                    
            if(cutters != null && !cutters.IsEmpty)
            {
                CutterCollection = cutters;

                //set the selected cutter and the cutterInstance
                SelectedCutter = CutterCollection.First();
                SetSelectedCutterInstance();                            
            }
            IsBusy = false;
        }

        #endregion

        #region Handle UI Events

        #region Delete Cutter Button Clicked

        private void HandleDeleteCutterTypeButtonClicked(object parameter)
        {
            if (SelectedCutter != null)
            {
                string messageContent = "Are you sure you want to delete cutter: " + SelectedCutter.CutterID;
                string title = "Delete Cutter";

                //verify the user really wants to delete this cutter
                var dialogMessage = new VerificationMessage(messageContent, title, OnDeleteCutterUserResponseCallback);
                Messenger.Default.Send<VerificationMessage>(dialogMessage);
            }
            else
            {
                var dialogMessage = new SavedNewCutterDialogMessage("This deletes the currently selected cutter.\nYou must select a cutter from the list in order to use this.", "Delete Cutter");
                Messenger.Default.Send<SavedNewCutterDialogMessage>(dialogMessage);
            }
        }

        private void OnDeleteCutterUserResponseCallback(MessageBoxResult answer)
        {
            if(answer == MessageBoxResult.OK)
                CutterDataService.Delete(SelectedCutter, OnDeleteCutterCallback, null);
        }

        private void OnDeleteCutterCallback(SubmitOperation op)
        {
            string msg = op.HasError ? "Delete was unsuccessful\n\n" : "Delete was successful";
            bool errorsFound = HandleErrors("Delete Cutter", msg, op);
        }

        #endregion

        #region Add New Cutter Button Clicked

        private void HandleAddNewCutterTypeButtonClicked(object parameter)
        {            
            NewCutter = new Cutter();
            NewCutterInstance = new CutterInstance();

            //set default values for new cutter & cutterInstance
            NewCutterInstance.DateActivated = DateTime.Today;            
            NewCutterInstance.ChangeNumber = 1;

            if (SelectedMill != 0)
                NewCutter.MachineNumber = SelectedMill;
            else
                NewCutter.MachineNumber = DEFAULT_MILL;

            //create the message to be sent to open a new cutter window
            //save it to a property in case we need to re-send the message
            //if the user submits the new cutter with validation errors
            NewCutterContainer = new NewCutterContainerMessage("LaunchAddNewCutterWindow", OnAddNewCutterWindowCallback) { _cutter = NewCutter, _cutterInstance = NewCutterInstance };
            
            Messenger.Default.Send(NewCutterContainer);
        }

        private void OnAddNewCutterWindowCallback()
        {
            if (NewCutterDoesNotHaveValidationErrors())
            {
                //verify cutterId doesn't already exist
                CutterDataService.CheckIfCutterIdExists(NewCutter.CutterID, OnValidateCutterIdCallback);
            }
            else //otherwise re-send the message to open the cutter window
                Messenger.Default.Send(NewCutterContainer);
        }

        private bool NewCutterDoesNotHaveValidationErrors()
        {
            return !(NewCutter.HasValidationErrors || NewCutterInstance.HasValidationErrors);         
        }
     
        private void OnValidateCutterIdCallback(bool idIsAvailable)
        {
            if (!idIsAvailable)
            {
                var dialogMessage = new VerificationMessage("This cutter Id is already in use.\nTo change it click OK.\nTo discard changes click Cancel.",
                                                            "Invalid Cutter Id", 
                                                            OnCutterIdExistsUserReponseCallback);
                Messenger.Default.Send(dialogMessage);                
            }
            else
            {                       
                /*
                 * The order that parents & children are added makes a difference.
                 * When adding a parent & child object to the database, 
                 * you need to add the child/children to the parent's collection
                 * of children objects, BEFORE you add the parent object to the
                 * database. 
                 * 
                 * This will make Entity Framework aware of the relationship.
                 * 
                 * Parent Object: Cutter (NewCutter)
                 * Child Object: CutterInstance (NewCutterInstance)
                 * Parent's Collection of child objects: CutterInstances
                 */
                NewCutter.CutterInstances.Add(NewCutterInstance);

                CutterDataService.Add(NewCutter, NewCutterInstance, AddCutterCallback, null);
            }            
        }

        private void OnCutterIdExistsUserReponseCallback(MessageBoxResult userAnswer)
        {
            if(userAnswer == MessageBoxResult.OK)
                Messenger.Default.Send(NewCutterContainer);
        }

        private void AddCutterCallback(SubmitOperation op)
        {
            string msg = op.HasError ? "Add was unsuccessful\n\n" : "Add was successful";
            bool errorsFound = HandleErrors("Add New Cutter", msg, op);

            if(errorsFound) //send the info back to the user
                Messenger.Default.Send(NewCutterContainer);
            
            else //save was successful
                UpdateUIAfterSubmit();            
        }
        
        //update the mill & cutter collections        
        private void UpdateUIAfterSubmit()
        {
            EnableSubmitButton = false;

            /*
             * This is just for the sake of a nice UX,
             * if the user created a cutter that would
             * go in the list of cutters they are already
             * looking at, or it would add a new mill to the
             * dropdown, update the appropriate collection
             * so it is reflected immediately instead of
             * the next time the data is loaded.
             */
            if (NewCutter.MachineNumber == SelectedMill)
                CutterCollection.Refresh();                
            
            if (!MachineNumbers.Contains(NewCutter.MachineNumber.ToString()))
                MachineNumbers.Add(NewCutter.MachineNumber.ToString());
        }

        #endregion

        #region Mill Selection Changed

        private void HandleSelectedMillChanged(object parameter)
        {
            LoadCutters();
        }

        #endregion

        #region Cutter Selection Changed

        private void HandleSelectedCutterChanged(object parameter)
        {
            SetSelectedCutterInstance();
        }

        //we are able to do this because I included the collection with the
        //[Include] attribute in ToolsheetDomainService.metadata.cs
        private void SetSelectedCutterInstance()
        {
            if (SelectedCutter != null)
                SelectedCutterInstance = SelectedCutter.CutterInstances.Where(i => i.DateRetired == null).FirstOrDefault();
        }

        #endregion

        #region Submit Button Clicked

        private void HandleSubmitButtonClicked(object parameter)
        {
            IsBusy = true;

            if(SelectedCutterInstance.LifeRemaining <= SelectedCutterInstance.LifeExpectancy)
                SelectedCutter.LifeExpectancy = SelectedCutterInstance.LifeExpectancy;

            CutterDataService.Save(SaveCutterCallback, null);
            
        }

        private void SaveCutterCallback(SubmitOperation op)
        {
            IsBusy = false;

            string msg = op.HasError ? "Save was unsuccessful\n\n" : "Save was successful";
            bool errorsFound = HandleErrors("Save", msg, op);

            //save was successful
            if (!errorsFound)
                EnableSubmitButton = false;
        }

        private bool HandleErrors(string title, string msg, SubmitOperation op)
        {
            bool errorsFound = false;
            
            StringBuilder message = new StringBuilder(msg);

            if (op.EntitiesInError.Any())
            {
                errorsFound = true;
                Entity entityInError = op.EntitiesInError.First();

                if (entityInError.EntityConflict != null)
                {
                    EntityConflict conflict = entityInError.EntityConflict;
                    foreach (string s in conflict.PropertyNames)
                        message.Append(string.Format("\r\nMember '{0}' in conflict", s));
                }
                else if (entityInError.ValidationErrors.Any())
                {
                    //build validation error message
                    foreach (var entity in op.EntitiesInError)
                        foreach (ValidationResult operationError in entity.ValidationErrors)
                        {
                            if (operationError.MemberNames.Count() > 0)
                            {
                                message.Append(operationError.MemberNames.First() + ":\t");

                                if (operationError.MemberNames.First().Length < 10)
                                    message.Append("\t");
                            }
                            message.Append(operationError.ErrorMessage + Environment.NewLine);
                        }
                }
                //mark errors as handled, so we don't get an exception.
                op.MarkErrorAsHandled();
            }

            var dialogMessage = new SavedNewCutterDialogMessage(message.ToString(), title);
            Messenger.Default.Send(dialogMessage);

            return errorsFound;
        }

        // Used by the SubmitButtonCommand to determine if the SubmitButton is enabled.          
        private bool SubmitButtonIsEnabled(object parameter)
        {
            return EnableSubmitButton;
        }

        #endregion

        #endregion
    }
}
