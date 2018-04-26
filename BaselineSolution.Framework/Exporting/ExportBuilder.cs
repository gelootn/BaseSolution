using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BaselineSolution.Framework.Extensions;
using BaselineSolution.Framework.Infrastructure.Attributes;
using DoddleReport;
using DoddleReport.OpenXml;
using DoddleReport.Web;

namespace BaselineSolution.Framework.Exporting
{
    public class ExportBuilder<TModel> where TModel : class
    {
        private readonly IEnumerable<TModel> _models;
        private readonly ICollection<IExportColumn<TModel>> _columns;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        private ExportBuilder(IEnumerable<TModel> models)
        {
            _models = models;
            _columns = new List<IExportColumn<TModel>>();
        }

        public static ExportBuilder<TModel> Create(IEnumerable<TModel> models)
        {
            return new ExportBuilder<TModel>(models);
        }

        public ExportBuilder<TModel> Column<TProperty>(Expression<Func<TModel, TProperty>> display)
        {
            if (!(display.Body is MemberExpression))
                throw new ArgumentException(display + " is not a property!");

            var memberInfo = ((MemberExpression)display.Body).Member;
            if (!memberInfo.HasAttribute<DisplayNameAttribute>())
                throw new ArgumentException(display + " does not have a [Display] attribute");
            var displayAttribute = ExtensionsForMemberInfo.GetAttribute<DisplayNameAttribute>(memberInfo);

            _columns.Add(new ExportColumn<TModel, TProperty>(displayAttribute.DisplayName, display));
            return this;
        }

        public ExportBuilder<TModel> Column<TProperty>(string header, Expression<Func<TModel, TProperty>> property)
        {
            _columns.Add(new ExportColumn<TModel, TProperty>(header, property));
            return this;
        }

        public IReportSource ToReportSource()
        {
            if (_models.Any())
            {
                return DoddleExporter.ToReportSource(_models.Select(model => _columns.ToDictionary(c => c.Header, c => c.Display(model))));
            }
            var result = _columns
                    .ToDictionary(a => a.Header, a => string.Empty);
            return DoddleExporter.ToReportSource(new[] { result });
        }

        public Report ToReport([CanBeNull] IEnumerable<KeyValuePair<string, string>> headers, [CanBeNull] IReportWriter writer = null)
        {
            headers = headers ?? Enumerable.Empty<KeyValuePair<string, string>>();
            var report = new Report(ToReportSource(), writer);
            //report.TextFields.Footer = string.Format(@"Aangemaakt op: {0}", DateTime.Now.ToString(DataFormatStrings.Date));
            var headersArray = headers as KeyValuePair<string, string>[] ?? headers.ToArray();
            if (headersArray.Any())
            {
                report.TextFields.Header = headersArray.Aggregate(string.Empty,
                                             (currentHeaders, header) => string.Format("{0}{3}{1} : {2}", currentHeaders, header.Key, header.Value, Environment.NewLine));
            }

            return report;
        }

        public ReportResult ToExcelReportResult([CanBeNull] IEnumerable<KeyValuePair<string, string>> headers,string fileName = "")
        {
            var result = new ReportResult(ToReport(headers), new ExcelReportWriter(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            if(!string.IsNullOrWhiteSpace(fileName))
                result.FileName = fileName;

            return result;
        }
    }
}
