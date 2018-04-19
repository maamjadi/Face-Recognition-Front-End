using System;
using System.Net;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;


namespace FaceRecognitionFrontEnd
{
    public partial class StudentPage : ContentPage
    {
        public const string path = "/students";
        public string userName { get; set; }

        public StudentPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
        async void AddStudent(object sender, EventArgs e)
        {
            try
            {
                var response = await RestClient.Get(path + "/"+userName);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    DisplayErrorAlert("Wrong User Name!");
                }
                else if (response.StatusCode == HttpStatusCode.OK)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var responseId = JObject.Parse(data);
                    var studentId = responseId["studentId"].ToString();
                    //TODO go back and give the student id to the array on the subjects
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
