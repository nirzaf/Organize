using System;
using System.Collections.Generic;
using System.Text;

namespace Organize.Shared.Enitites
{
    public class TextItem : BaseItem
    {
        public string SubTitle
        {
            get => _subTitle;
            set => SetProperty(ref _subTitle, value);

        }
        private string _subTitle;

        public string Detail
        {
            get => _detail;
            set => SetProperty(ref _detail, value);
        }
        private string _detail;
    }
}
