using Client.MaUI.Validations.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MaUI.Validations.Rules
{
    public class IsValidSizeImage<T>: IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public int Size { get; set; } = 15;
        public bool Check(T value)
        {
            return true;
        }

    }
}
