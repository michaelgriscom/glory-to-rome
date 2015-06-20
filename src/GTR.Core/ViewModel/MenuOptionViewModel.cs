namespace GTR.Core.ViewModel
{
    public class MenuOptionViewModel
    {
        public string OptionName { get; protected set; }
    }

    public class SelectableMenuOptionViewModel : MenuOptionViewModel
    {
        public SelectableMenuOptionViewModel(string optionName)
        {
            OptionName = string.Format("> {0}", optionName);
        }
    }
}