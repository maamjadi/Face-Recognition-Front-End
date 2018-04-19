using System;
using System.Net;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;

namespace FaceRecognitionFrontEnd
{
    public partial class AddStudent : ContentPage
    {
        public const string path = "/students";
        public string userName { get; set; }

        public AddStudent()
        {
            InitializeComponent();
            BindingContext = this;
        }
        async void AddNewStudent(object sender, EventArgs e)
        {

            Student student = new Student();
            student.UserName = userName;
            try
            {
                var response = await RestClient.Post(path + "/", student);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    DisplayErrorAlert("User Name already exist!");
                }
                else if (response.StatusCode == HttpStatusCode.Created)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var responseId = JObject.Parse(data);
                    var studentId = responseId["studentId"].ToString();
                    //TODO go to recognize page with the studentId
                }
                else
                {
                    DisplayErrorAlert("Something went wrong!");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                DisplayErrorAlert("Something went wrong!");
            }

        }
        private void DisplayErrorAlert(string message)
        {
            DisplayAlert("Alert", message, "OK");
        }
    }
}
