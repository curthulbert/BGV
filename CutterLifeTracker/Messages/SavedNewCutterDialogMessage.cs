using System.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace CutterLifeTracker.Messages
{
    public class SavedNewCutterDialogMessage : DialogMessage
    {
        public SavedNewCutterDialogMessage(string content, string title)
            : base(content, null)
        {
            Button = MessageBoxButton.OK;             
            Caption = title;
        }
    }
}
