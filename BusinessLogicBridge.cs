using System;
using System.Collections.Generic;
using System.Text;

namespace DXWindowsApplication2
{
    class BusinessLogicBridge
    {
        public static DataLayer.BusinessLogic DataStore;
        public static void ConnectBusinessLogic()
        {
            DataStore = new DataLayer.BusinessLogic();
            DataStore.Connect();
            languages.loadLanguage("en");

        }
    }
}
