using System;
namespace FaceRecognitionFrontEnd
{
    public class Student
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string FaceId { get; set; }
        public string[] SubjectsId { get; set; }
        public int[] SubjectAttendance { get; set; }

        public Student()
        {
        }
    }
}
