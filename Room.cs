using System;

namespace DXWindowsApplication2 {
    class Room {
        string roomID;
        string firstName;
        string secondName;
        string comments;
        string roomType;
        public Room(string firstName, string secondName) {
            this.firstName = firstName;
            this.secondName = secondName;
            comments = String.Empty;
        }
        public Room(string firstName, string secondName, string comments)
            : this(firstName, secondName) {
            this.comments = comments;           
        }
        public Room(string firstName, string secondName,string roomID, string roomType)
        {
            this.firstName = firstName;
            this.secondName = secondName;
            this.roomType = roomType;
            this.roomID = roomID;
        }
        public string FirstName {
            get { return firstName; }
            set { firstName = value; }
        }
        public string SecondName {
            get { return secondName; }
            set { secondName = value; }
        }
        public string Comments {
            get { return comments; }
            set { comments = value; }
        }
        public string RoomID 
        {
            get { return roomID; }
            set { roomID = value; }
        }
        public string RoomType {
            get { return roomType; }
            set { roomType = value; }
        }
        
    }
}