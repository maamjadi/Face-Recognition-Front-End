using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FaceRecognitionFrontEnd;
using Xamarin.Cognitive.Face;
using Microsoft.ProjectOxford.Face;
using Plugin.Media.Abstractions;
using Plugin.Media;
using System.Linq;

namespace FaceRecognitionFrontEnd.utilities
{
    public class RecMan
    {
        public static RecMan Instance { get; } = new RecMan();

        RecMan()
        {
        }
        public static  async Task RegisterStudents(List<Student> students, string personGroupId)
        {
            if (students.Count > 0)
            {
                var faceServiceClient = new FaceServiceClient(Constants.FaceApiKey, Constants.FaceEndpoint);
                //faceServiceClient.UpdatePersonGroupAsync()

                // Step 1 - Create Person Group
                var personGroup = Guid.NewGuid().ToString();
                await faceServiceClient.CreatePersonGroupAsync(personGroupId, "Xamarin Employees");

                // Step 2 - Add persons (and faces) to person group.
                foreach (var student in students)
                {
                    // Step 2a - Create a new person, identified by their name.
                    var p = await faceServiceClient.CreatePersonAsync(personGroupId, student.UserName);
                    // Step 3a - Add a face for that person.
                    await faceServiceClient.AddPersonFaceAsync(personGroupId, p.PersonId, student.PhotoURL);
                }

                // Step 3 - Train facial recognition model.
                await faceServiceClient.TrainPersonGroupAsync(personGroupId);

            }
        }
        public static async Task AddStudents(List<Student> students, string personGroupId)
        {
            if (students.Count > 0)
            {
                var faceServiceClient = new FaceServiceClient(Constants.FaceApiKey, Constants.FaceEndpoint);
                //faceServiceClient.UpdatePersonGroupAsync()

                // Step 2 - Add persons (and faces) to person group.
                foreach (var student in students)
                {
                    // Step 2a - Create a new person, identified by their name.
                    var p = await faceServiceClient.CreatePersonAsync(personGroupId, student.UserName);
                    // Step 3a - Add a face for that person.
                    await faceServiceClient.AddPersonFaceAsync(personGroupId, p.PersonId, student.PhotoURL);
                }

                // Step 3 - Train facial recognition model.
                await faceServiceClient.TrainPersonGroupAsync(personGroupId);
            }
        }

        public static async Task DeleteGroup( string personGroupId)
        {
          
                var faceServiceClient = new FaceServiceClient(Constants.FaceApiKey, Constants.FaceEndpoint);
                //faceServiceClient.UpdatePersonGroupAsync()
                await faceServiceClient.DeletePersonGroupAsync(personGroupId);

        }
        public static async Task<string> ExecuteFindSimilarFaceCommandAsync(string personGroupId, MediaFile photo)
        {
            var stream = photo.GetStream();


            var faceServiceClient = new FaceServiceClient(Constants.FaceApiKey, Constants.FaceEndpoint);
                // Step 4a - Detect the faces in this photo.
                var faces = await faceServiceClient.DetectAsync(stream);
                var faceIds = faces.Select(face => face.FaceId).ToArray();

                if (faceIds.Length == 0)
                return null;
                            
                // Step 4b - Identify the person in the photo, based on the face.
                var results = await faceServiceClient.IdentifyAsync(personGroupId, faceIds);
                var result = results[0].Candidates[0].PersonId;

                // Step 4c - Fetch the person from the PersonId and display their name.
                var person = await faceServiceClient.GetPersonAsync(personGroupId, result);
                return person.Name;
          
            
        }
    }
}
