using System;

namespace CutterLifeTracker.Services
{
    public class HasChangesEventArgs : EventArgs
    {
        public bool HasChanges { get; set; }
    }
}
