using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.MaUI.Validations.Interfaces
{
    public interface IValidatable<T>
    {
        List<IValidationRule<T>> Validations { get; }

        List<string> Errors { get; set; }

        bool Validate();

        bool IsValid { get; set; }
    }
}
