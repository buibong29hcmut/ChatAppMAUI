using Client.MaUI.Validations.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MaUI.Validations.Rules
{
    public class IsNotNullOrEmptyRule<T>:IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = $"{value}";
            return !string.IsNullOrWhiteSpace(str);
        }
    }
}
