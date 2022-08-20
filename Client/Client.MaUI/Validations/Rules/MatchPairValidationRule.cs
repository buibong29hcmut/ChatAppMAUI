using Client.MaUI.Validations.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MaUI.Validations.Rules
{
    public class MatchPairValidationRule<T> : IValidationRule<ValidatablePair<T>>
    {
        public string ValidationMessage { get; set; }

        public bool Check(ValidatablePair<T> value)
        {
            return value.Item1.Value.Equals(value.Item2.Value);
        }
    }
}
