using System;
using System.ServiceModel.DomainServices.Client;
using Toolsheet_Library.Web.Service;

namespace CutterLifeTracker.Services
{
    internal class DataService : IDataService
    {
        protected ToolsheetDomainContext _context { get; set; }
        public event EventHandler<HasChangesEventArgs> NotifyHasChanges;

        public DataService()
        {
            _context = new ToolsheetDomainContext();
            _context.PropertyChanged += ContextPropertyChanged;
        }

        private void ContextPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (NotifyHasChanges != null)
                NotifyHasChanges(this, new HasChangesEventArgs() { HasChanges = _context.HasChanges });
        }        
    }
}
