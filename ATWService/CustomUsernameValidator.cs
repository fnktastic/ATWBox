using System;
using System.Diagnostics;
using System.IdentityModel.Selectors;
using System.ServiceModel;

namespace ATWService
{
    public class CustomUsernameValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            try
            {
                if (userName == "test" && password == "test123")
                {
                    Debug.WriteLine("Authentic User");
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(string.Format("Unknown Username or Incorrect Password: {0}", ex.Message));
            }
        }
    }
}