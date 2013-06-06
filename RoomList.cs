using System;
using System.Collections.Generic;
using System.Text;

namespace DXWindowsApplication2
{
    public class RoomList
    {
        private string roomno;
        private string roomtype;
        private string name;
        private string surname;
        private long idno;
        private int roomid;
        private DateTime create_date;
        private DateTime modified_date;
        private string roomstatus;


        public string Roomno
        {
            get
            {
                return roomno;
            }
            set
            {
                roomno = value;
            }
        }

        public string Roomtype
        {
            get
            {
                return roomtype;
            }
            set
            {
                roomtype = value;
            }
        }

        public int Roomid
        {
            get
            {
                return roomid;
            }
            set
            {
                roomid = value;
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
        public string Roomstatus
        {
            get
            {
                return roomstatus;
            }
            set
            {
                roomstatus = value;
            }
        }

    }
}
