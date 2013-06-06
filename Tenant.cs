using System;
using System.Collections.Generic;
using System.Text;

namespace DXWindowsApplication2
{
    public class Tenant
    {
        private string name;
        private string surname;
        private long idno;
        private int tenantid;
        private DateTime create_date;
        private DateTime modified_date;
        private string carno;
        private string status;
        private string paymentstatus;

        public int Tenantid
        {
            get
            {
                return tenantid;
            }
            set
            {
                tenantid = value;
            }
        }
        
        
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
            }
        }

        public long Idno
        {
            get
            {
                return idno;
            }
            set
            {
                idno = value;
            }
        }

       

        public DateTime Modified_date
        {
            get
            {
                return modified_date;
            }
            set
            {
                modified_date = value;
            }
        }

        public DateTime Created_date
        {
            get
            {
                return create_date;
            }
            set
            {
                create_date = value;
            }
        }
        public string Carno
        {
            get
            {
                return carno;
            }
            set
            {
                carno = value;
            }
        }
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }
        public string PaymentStatus
        {
            get
            {
                return paymentstatus;
            }
            set
            {
                paymentstatus = value;
            }
        }
    }
    
    
}
