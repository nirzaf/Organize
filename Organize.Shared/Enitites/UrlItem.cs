using System;
using System.Collections.Generic;
using System.Text;

namespace Organize.Shared.Enitites
{
    public class UrlItem : BaseItem
    {
        public string Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }
        private string _url;
    }
}
