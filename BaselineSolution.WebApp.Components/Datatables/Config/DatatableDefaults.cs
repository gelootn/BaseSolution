using BaselineSolution.WebApp.Components.Datatables.Html.Components.DisplayComponents;
using BaselineSolution.WebApp.Components.Datatables.Html.Components.SearchComponents;
using BaselineSolution.WebApp.Components.Datatables.Html.TableRenderers;

namespace BaselineSolution.WebApp.Components.Datatables.Config
{
    public static class DatatableDefaults
    {
        public static class Html
        {
            public static class SearchComponents
            {
                private static readonly BooleanSearchComponent BooleanSearchComponent = new BooleanSearchComponent();

                private static readonly DateTimeSearchComponent DateTimeSearchComponent =
                    new DateTimeSearchComponent();

                private static readonly TextSearchComponent TextSearchComponent = new TextSearchComponent();

                public static BooleanSearchComponent Boolean
                {
                    get { return BooleanSearchComponent; }
                }

                public static DateTimeSearchComponent DateTime
                {
                    get { return DateTimeSearchComponent; }
                }

                public static TextSearchComponent Text
                {
                    get { return TextSearchComponent; }
                }
            }

            public static class DisplayComponents
            {
                private static readonly GlyphiconsBooleanDisplayComponent GlyphiconsBooleanDisplayComponent = new GlyphiconsBooleanDisplayComponent();

                public static GlyphiconsBooleanDisplayComponent GlyphiconsBoolean { get { return GlyphiconsBooleanDisplayComponent; } }
            }

            public static class TableRenderers
            {
                private static readonly TwitterBootstrapRemoteDatatableRenderer
                    TwitterBootstrapRemoteDatatableRenderer = new TwitterBootstrapRemoteDatatableRenderer();

                private static readonly TwitterBootstrapLocalDatatableRenderer
                    TwitterBootstrapLocalDatatableRenderer = new TwitterBootstrapLocalDatatableRenderer();

                public static TwitterBootstrapRemoteDatatableRenderer TwitterBootstrapRemote
                {
                    get { return TwitterBootstrapRemoteDatatableRenderer; }
                }

                public static TwitterBootstrapLocalDatatableRenderer TwitterBootstrapLocal
                {
                    get { return TwitterBootstrapLocalDatatableRenderer; }
                }
            }
        }
    }
}