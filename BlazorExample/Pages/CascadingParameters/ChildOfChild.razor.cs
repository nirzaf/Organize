using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorExample.Pages.CascadingParameters
{
    public partial class ChildOfChild : ComponentBase
    {
        [CascadingParameter]
        public int Number { get; set; }

        [CascadingParameter(Name = "H1")]
        //[CascadingParameter]
        public string Text1 { get; set; }

        [CascadingParameter(Name = "H2")]
        //[CascadingParameter]
        public string Text2 { get; set; }
    }
}
