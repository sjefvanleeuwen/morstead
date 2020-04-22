using Bunit;
using Bunit.Mocking.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal.Tests
{
    public class BlazorTestBase : ComponentTestFixture
    {
        public BlazorTestBase()
        {
            var jsMock = Services.AddMockJsRuntime();
            //solution for this error message:
            //System.InvalidOperationException : Cannot provide a value for property 'UriHelper' on type 'MatBlazor.MatIconButton'. There is no registered service of type 'Microsoft.AspNetCore.Components.NavigationManager'.
            Services.AddScoped(typeof(NavigationManager), typeof(MockNavigationManager));
        }
    }

    /// <summary>
    /// https://github.com/egil/bunit/issues/73
    /// </summary>
    public class MockNavigationManager : NavigationManager, IDisposable
    {
        public string NavigateToLocation { get; private set; }

        /// <summary>
        /// Navigates to the specified URI.
        /// </summary>
        /// <param name="uri">The destination URI. This can be absolute, or relative to the base URI
        /// (as returned by <see cref="NavigationManager.BaseUri"/>).</param>
        /// <param name="forceLoad">If true, bypasses client-side routing and forces the browser to load the new page from the server, whether or not the URI would normally be handled by the client-side router.</param>
        public new virtual void NavigateTo(string uri, bool forceLoad = false)
        {
            //HERE WE ARE INTENTIONALLY OVERRIDING THE IMPLEMENTATION.
            NavigateToCore(uri, forceLoad);
        }
        protected override void NavigateToCore(string uri, bool forceLoad)
        {
            NavigateToLocation = uri;
            Uri = $"{this.BaseUri}{uri}";
        }

        protected sealed override void EnsureInitialized()
        {
            Initialize("http://localhost:5000/", "http://localhost:5000/dashboard/");
        }

        public MockNavigationManager()
        {
            EnsureInitialized();
        }

        public void Dispose()
        {
        }
    }

}
