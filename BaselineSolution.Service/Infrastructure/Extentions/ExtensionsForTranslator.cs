using BaselineSolution.Bo.Internal;
using BaselineSolution.DAL.Infrastructure.Bases;
using BaselineSolution.Service.Translators.Internal;


namespace BaselineSolution.Service.Infrastructure.Extentions
{
    internal static class ExtensionsForTranslator
    {
        public static TBo ToBo<TBo, TModel>(this TModel model,ITranslator<TBo, TModel> translator)
            where TBo : BaseBo, new()
            where TModel : Entity

        {
            var bo = new TBo();

            if (!Equals(model, default(TModel)) && !model.Deleted)
            {
                bo.Id = model.Id;
                bo = translator.FromModel(model);
            }

            return bo;
        }

        public static TModel UpdateModel<TBo, TModel>(this TBo bo, TModel model, ITranslator<TBo, TModel> translator)
            where TBo : BaseBo
            where TModel : Entity
        {
            return translator.UpdateModel(bo, model);
        }
    }
}
