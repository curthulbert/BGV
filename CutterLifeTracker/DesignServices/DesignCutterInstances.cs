using System;
using System.Collections.ObjectModel;
using CutterLifeTracker.DesignModel;
using Toolsheet_Library.Web.EntityModel;

namespace CutterLifeTracker.DesignServices
{
    class DesignCutterInstances : ObservableCollection<CutterInstance>
    {
        private Cutter _designCutter;

        public DesignCutterInstances()
        {

            _designCutter = new DesignCutter()[0];
            
            CreateCutterInstances();
        }

        private void CreateCutterInstances()
        {
            Random r = new Random();
            var cutterInstance = new CutterInstance()
            {
                CutterInstanceID = r.Next(),
                DateActivated = DateTime.Now.Date,
                Cutter = _designCutter,
                LifeRemaining = r.Next(250, 350),
                ChangeNumber = r.Next(1,30)                
            };
        }
    }
}
