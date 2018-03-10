using System;
namespace FaceRecognitionFrontEnd
{
    public class Teacher
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string[] Subjects { get; set; }

        public Teacher()
        {
        }
    }
}
