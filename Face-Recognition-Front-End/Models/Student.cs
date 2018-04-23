using System;
namespace FaceRecognitionFrontEnd
{
    public class Student
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PhotoURL { get; set; }
        public SubjectAttendance[] SubjectAttendance { get; set; }

        public Student()
        {
        }
    }
    public class SubjectAttendance{
        public string SubjectId;
        public int Attendance;
        public SubjectAttendance()
        {
        }
    }
}
