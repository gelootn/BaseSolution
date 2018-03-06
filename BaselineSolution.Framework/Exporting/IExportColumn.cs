using System;

namespace BaselineSolution.Framework.Exporting
{
    public interface IExportColumn<TModel> where TModel : class
    {
        string Header { get; }
        Func<TModel, Object> Display { get; }
    }
}
