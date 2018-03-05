namespace BaselineSolution.WebApp.Components.Models.SelectLists
{
    public class RemoteSelectOptions: SelectOptions
    {
        public bool CustomInitialize { get; set; }
        public bool Multiple { get; set; }
        public bool SelectOrNew { get; set; }
        public string AddUrl { get; set; }
    }
}
