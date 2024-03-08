using LogisticsManagement.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogisticsManagement
{
    public class Validations
    {
        public static bool ValidateIntInputChoice(int choice)
        {
            if (!int.TryParse(choice.ToString(), out _))
            {
                CommonServices.ErrorMessage("Invalid input. Please enter a valid integer:");
                return false;
            }
            return true;
        }
        public static bool ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || !char.IsLetter(name[0]) || name.Length <2)
            {
                CommonServices.ErrorMessage("Invalid Name. Name must start with an alphabet and be at least 2 characters long.");
                return false;
            }
            return true;
        }
        public static bool validateEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            if (!Regex.IsMatch(email, pattern))
            {
                CommonServices.ErrorMessage("Invalid Email. Please enter a valid email.");
                return false;
            }
            return true;
        }

        public static bool validatePassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{6,}$";
            if (!Regex.IsMatch(password, pattern))
            {
                CommonServices.ErrorMessage("Invalid Password. Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
                return false;
            }
            return true;
        }
        public static bool validatePhoneNumber(string phoneNumber)
        {
            string pattern = @"^(\+91[\-\s]?)?[0]?(91)?[789]\d{9}$";
            if (!Regex.IsMatch(phoneNumber, pattern))
            {
                CommonServices.ErrorMessage("Invalid Phone Number. Please enter a valid phone number.");
                return false;
            }
            return true;
        }
        public static bool ValidateLicenseNumber(string licenseNumber)
        {
            string pattern = @"^[A-Za-z0-9]{6,12}$";

            if(!Regex.IsMatch(licenseNumber, pattern))
            {
                CommonServices.ErrorMessage("Invalid License Number. Please enter a valid license number.");
                return false;
            }
            return true;
        }
        public static bool ValidateVehicleNumber(string vehicleNumber)
        {
            // Define a regular expression pattern for vehicle number validation
            string pattern = @"^[A-Z]{2}\s?[0-9]{2}\s?[A-Z]{2}\s?[0-9]{4}$";

            if(! Regex.IsMatch(vehicleNumber, pattern))
            {
                CommonServices.ErrorMessage("Invalid Vehicle Number. Please enter a valid vehicle number.");
                return false;
            }

            return true;
        }

    }


}

