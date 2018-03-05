namespace BaselineSolution.WebApp.Components.Models.SelectLists
{
    /// <summary>
    ///     The options for a select element. Note that this is not necessarily a HTML 'select' tag, when using remote data
    ///     it is necessary to use an 'input' tag of the type 'hidden'.
    /// </summary>
    public class SelectOptions
    {
        /// <summary>
        ///     Gets or sets the optional label that should be used for an extra option tag without value
        /// </summary>
        public string Placeholder { get; set; }
    }
}
