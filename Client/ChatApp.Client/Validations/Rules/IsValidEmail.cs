using Client.MaUI.Validations.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Client.MaUI.Validations.Rules
{
    internal class IsEmailValidRule<T>:IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            try
            {
                MailAddress m = new MailAddress(value as string);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
