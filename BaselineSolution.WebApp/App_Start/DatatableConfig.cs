using System;
using BaselineSolution.WebApp.Components.Datatables.Config;

namespace BaselineSolution.WebApp
{
    public class DatatableConfig
    {
        public static void Register()
        {

            DatatableConfiguration.Components.SearchComponents.Register(typeof(bool), DatatableDefaults.Html.SearchComponents.Boolean);
            DatatableConfiguration.Components.SearchComponents.Register(typeof(bool?), DatatableDefaults.Html.SearchComponents.Boolean);
            DatatableConfiguration.Components.SearchComponents.Register(typeof(DateTime?), DatatableDefaults.Html.SearchComponents.DateTime);
            DatatableConfiguration.Components.SearchComponents.Register(typeof(DateTime), DatatableDefaults.Html.SearchComponents.DateTime);
            DatatableConfiguration.Components.SearchComponents.Default = DatatableDefaults.Html.SearchComponents.Text;

            //DatatableConfiguration.Components.DisplayComponents.Register(DatatableDefaults.Html.DisplayComponents.GlyphiconsBoolean);

            DatatableConfiguration.TableRenderers.LocalDatatableRenderer = DatatableDefaults.Html.TableRenderers.TwitterBootstrapLocal;
            DatatableConfiguration.TableRenderers.RemoteDatatableRenderer = DatatableDefaults.Html.TableRenderers.TwitterBootstrapRemote;
        }
    }
}