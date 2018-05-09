using System;
namespace FaceRecognitionFrontEnd
{
    public class Subject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastDate { get; set; }
        public string[] Students { get; set; }
        public int NumberOfSessions { get; set; }
        public int AttendedSessions { get; set; }

        public Subject()
        {
        }
    }
}
