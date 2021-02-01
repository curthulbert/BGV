using System;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace CutterLifeTracker.Messages
{
    internal class VerificationMessage : DialogMessage
    {                
        public VerificationMessage(string content, string title, Action<MessageBoxResult> callback)
            :base(content, callback)
        {
            Button = MessageBoxButton.OKCancel;
            Caption = title;
        }
    }
}
