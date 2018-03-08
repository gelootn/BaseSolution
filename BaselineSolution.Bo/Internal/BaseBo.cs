using System;
using System.Collections.Generic;
using System.Linq;
using BaselineSolution.Framework.Infrastructure.Contracts;
using FluentValidation;

namespace BaselineSolution.Bo.Internal
{
    [Serializable]
    public abstract class BaseBo : IIdentifiable
    {
        protected IValidator Validator;
        private List<string> _validationMessages;

        public int Id { get; set; }

        public List<string> ValidationMessages
        {
            get { return _validationMessages ?? (_validationMessages = new List<string>()); }
            set { _validationMessages = value; }
        }

        public bool IsNew => Id == 0;

        public bool IsValid()
        {
            if (Validator == null)
                throw new ValidationException("Validation is not availible");

            var result = Validator.Validate(this);

            if (!result.IsValid)
            {
                
                ValidationMessages.AddRange(result.Errors.Select(x => $"{x.ErrorMessage}"));
            }

            return result.IsValid;
        }
    }
}
