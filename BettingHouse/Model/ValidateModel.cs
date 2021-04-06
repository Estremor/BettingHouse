using BettingHouse.Properties;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingHouse.Model
{
    public class ValidateModel : AbstractValidator<BettingModel>
    {
        public ValidateModel()
        {
            RuleFor(x => x.Amount).Must(ValidateMaxAmount).WithMessage(Resources.InvalidAmount);
            RuleFor(x => x.Number).Must(ValidateRange).WithMessage(Resources.InvalidNumberSelected);
        }

        private bool ValidateRange(int arg)
        {
            return !(arg < 0 || arg > 38);
        }

        private bool ValidateMaxAmount(decimal arg)
        {
            return !(arg < 0 || arg > 10000);
        }
    }
}
