using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Toolsheet_Library.Web.EntityModel;

namespace CutterLifeTracker.DesignModel
{
    public class DesignCutter : ObservableCollection<Cutter>
    {        
        public DesignCutter()
        {
            List<int> cutterId = new List<int>();

            for (int i = 0; i < 30; i++)
            {
                //create random cutter info
                Random r = new Random();
                Cutter cutter = new Cutter();

                cutter.CutterID = r.Next(9115, 9215);                
                cutter.LifeExpectancy = r.Next(250, 350);
                cutter.MachineNumber = 6;                

                //make sure we don't have duplicate values
                //before we add them to the list of Cutter
                //objects
                cutterId.Add(cutter.CutterID);

                if (!cutterId.Contains(cutter.CutterID))
                    Add(cutter);
            }
        }        
    }
}
