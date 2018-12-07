using System;
using System.Collections.Generic;
using System.Linq;
using BaselineSolution.Framework.Infrastructure.Contracts;
using FluentValidation;

namespace BaselineSolution.Framework.Infrastructure
{
    [Serializable]
    public abstract class BaseBo : IIdentifiable
    {
        protected IValidator Validator;
        private List<ValidationMessage> _validationMessages;

        public int Id { get; set; }

        public List<ValidationMessage> ValidationMessages
        {
            get { return _validationMessages ?? (_validationMessages = new List<ValidationMessage>()); }
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
                
                ValidationMessages.AddRange(result.Errors.Select(x => new ValidationMessage
                {
                    FieldName = x.PropertyName,
                    Message = x.ErrorMessage,
                    AttemptedValue = $"{x.AttemptedValue}"
                }));
            }

            return result.IsValid;
        }
    }
}
