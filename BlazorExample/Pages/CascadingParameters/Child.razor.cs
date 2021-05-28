using Microsoft.AspNetCore.Components;

namespace BlazorExample.Pages.CascadingParameters
{
    public partial class Child : ComponentBase
    {
        [CascadingParameter] public int Count { get; set; }

        //[CascadingParameter]
        [CascadingParameter(Name = "H2")] public string Text { get; set; }
    }
}