using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace CutterLifeTracker.Messages
{
    internal class FrameMessage : MessageBase
    {
        public Frame RootFrame { get; set; }
    }
}
