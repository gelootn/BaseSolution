using BaselineSolution.Service.Translators.Internal;
using FluentValidation;

namespace BaselineSolution.Service.Tests.TestData
{
    public class TestObjectTranslator : ITranslator<TestObjectBo, TestObject>
    {
       
        public TestObjectBo FromModel(TestObject model)
        {
            return new TestObjectBo() { Name = model.Name };
        }

        public TestObject UpdateModel(TestObjectBo bo, TestObject model)
        {
            model.Name = bo.Name;
            return model;
        }
    }
}