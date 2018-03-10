using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;
namespace FaceRecognitionFrontEnd
{
    public class ValidationService
    {
        static Color validationColor = Color.Red;
        static Color correctionColor = Color.Black;
        public ValidationService()
        {
        }
        public static bool CheckEntry(Entry entry, string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                entry.PlaceholderColor = validationColor;
                return false;
            }
            else
            {
                entry.PlaceholderColor = correctionColor;
                return true;
            }
        }
        public static bool CheckEmailSyntax(string email, Entry EmailEntry)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (!match.Success)
            {
                EmailEntry.TextColor = validationColor;
            }
            else
            {
                EmailEntry.TextColor = correctionColor;
            }
            return match.Success;
        }
        public static bool VerfiyPassword(string password, string verifyPassword, Entry verifyPasswordEntry)
        {
            if (password != verifyPassword)
            {
                verifyPasswordEntry.TextColor = validationColor;
                return false;
            }
            else
            {
                verifyPasswordEntry.TextColor = correctionColor;
                return true;
            }
        }

    }
}
