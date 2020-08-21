// ONLY needed for Live Unit Tests
// https://developercommunity.visualstudio.com/content/problem/853116/live-unit-testing-fails-to-build-blazor-server-pro.html
// The reason for live unit testing not working in blazor is that, code generation from .razor files is not supported with live unit testing and so the live unit testing does not recognize the classes.
//
//The solution for is to have a code behind file for all .razor pages. An empty code file will do the job. Even for App.razor and layout files.
//
// Add App.razor.cs and add the following to it

using Microsoft.AspNetCore.Components;

namespace C64.FrontEnd
{
    public partial class App : ComponentBase

    { }

    //public partial class MainLayout : LayoutComponentBase

    //{ }
}