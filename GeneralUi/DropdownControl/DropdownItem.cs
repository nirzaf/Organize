using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralUi.DropdownControl
{
    public class DropdownItem<T>
    {
        public string DisplayText { get; set; }

        public T ItemObject { get; set; }
    }
}
