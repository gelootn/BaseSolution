using System.CodeDom;
using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.Framework.Infrastructure;
using FluentValidation;

namespace BaselineSolution.Service.Tests.TestData
{
    public class TestObjectBo : BaseBo
    {
        public TestObjectBo()
        {
            Validator = new TestObjectBoValidator();
        }

        public string Name { get; set; }
    }

    public class TestObjectBoValidator : AbstractValidator<TestObjectBo>
    {
        public TestObjectBoValidator()
        {
            RuleFor(x => x.Id).Must(x => x < 100);
        }
    }
}