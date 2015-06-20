﻿#region

using GTR.Windows.Model;

#endregion

namespace GTR.Windows.ViewModel
{
    public class MainViewModel
    {
        /// <summary>
        ///     The <see cref="WelcomeTitle" /> property's name.
        /// </summary>
        public const string WelcomeTitlePropertyName = "WelcomeTitle";

        private readonly IDataService _dataService;
        private string _welcomeTitle = string.Empty;

        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }

                    WelcomeTitle = item.Title;
                });
        }

        /// <summary>
        ///     Gets the WelcomeTitle property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string WelcomeTitle
        {
            get { return _welcomeTitle; }

            set
            {
                if (_welcomeTitle == value)
                {
                    return;
                }

                _welcomeTitle = value;
            }
        }
    }
}