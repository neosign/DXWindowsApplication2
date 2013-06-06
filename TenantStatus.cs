using System;
using System.Collections.Generic;
using System.Text;

namespace DXWindowsApplication2
{
    public class TenantStatus
    {
        private int _tenantID;
        private int _tenantText;

        public int TenantID
        {
            get
            {
                return _tenantID;
            }
            set
            {
                _tenantID = value;
            }
        }

        public int TenantText
        {
            get
            {
                return _tenantText;
            }
            set
            {
                _tenantText = value;
            }
        }
    }
}
