using System;
using GalaSoft.MvvmLight.Messaging;
using Toolsheet_Library.Web.EntityModel;

namespace CutterLifeTracker.Messages
{
    internal class NewCutterContainerMessage : NotificationMessageAction
    {
        public Cutter _cutter { get; set; }
        public CutterInstance _cutterInstance { get; set; }

        public NewCutterContainerMessage(string notification, Action callback)
            :base(notification, callback)
        {
        }
    }
}
