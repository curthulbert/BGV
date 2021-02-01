using System;
using System.Windows;
using CutterLifeTracker.Messages;
using CutterLifeTracker.Views;
using GalaSoft.MvvmLight.Messaging;

namespace CutterLifeTracker.Services
{
    public class MediatorService
    {
        private NewCutterContainerMessage CutterContainer { get; set; }

        /* Implement the mediator as a singleton */
        #region Constructor

        private static MediatorService _instance;
        public static MediatorService Instance
        {
            // return the instance if it isn't null
            // otherwise return a new instance of the mediator
            get { return _instance ?? CreateInstance(); }
        }

        static MediatorService CreateInstance()
        {                        
            return _instance = new MediatorService();
        }

        private MediatorService()
        {
            //register listeners
            RegisterMessages();
        }

        #endregion

        #region Message Registration

        private void RegisterMessages()
        {            
            Messenger.Default.Register<NewCutterContainerMessage>(this, OnReceivedNewCutterContainerMessage);      
            Messenger.Default.Register<SavedNewCutterDialogMessage>(this, OnSavedCutterMessageReceived);
            Messenger.Default.Register<VerificationMessage>(this, OnVerificationMessageReceived);            
        }

        #endregion

        #region Handle received messages      

        private void OnVerificationMessageReceived(VerificationMessage msg)
        {   
            //display the window and save the user response
            MessageBoxResult result = MessageBox.Show(msg.Content, msg.Caption, msg.Button);

            //fire off the callback
            msg.ProcessCallback(result);                                 
        }

        private void OnSavedCutterMessageReceived(SavedNewCutterDialogMessage msg)
        {
            MessageBox.Show(msg.Content, msg.Caption, msg.Button);
        }
       
        private void OnReceivedNewCutterContainerMessage(NewCutterContainerMessage msg)                
        {
            if (msg.Notification == "LaunchAddNewCutterWindow")
            {
                CutterContainer = msg;

                LaunchNewCutterWindow();                
            }
        }
        #endregion

        #region ChildWindow methods

        private void LaunchNewCutterWindow()
        {
            AddNewCutterWindow dialog = new AddNewCutterWindow();
            dialog.Closed += OnAddNewCutterWindowClosed;
            dialog.Show();
        }

        private void OnAddNewCutterWindowClosed(object sender, EventArgs e)
        {
            AddNewCutterWindow dialog = (AddNewCutterWindow)sender;

            //check to see if OK button was pushed
            if (dialog.OKButton.IsPressed)
            {
                //check to see if we have results
                bool? result = dialog.DialogResult;

                if (result.HasValue && result.Value)
                {
                    CutterContainer.Execute();
                }
            }
            else
            {
                CutterContainer._cutter = null;
                CutterContainer._cutterInstance = null;
                CutterContainer = null;
            }
        }

        #endregion
    }
}
