using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Redbus;
using Redbus.Events;
using Redbus.Interfaces;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace Vs.VoorzieningenEnRegelingen.Site.Shared
{
    public partial class MainLayout
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider  { get; set; }

        [Inject]
        public IEventBus Bus  { get; set; }

        SubscriptionToken token;

        private static Timer aTimer;
        private static long count;

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            var authState = AuthenticationStateProvider.GetAuthenticationStateAsync().Result;
            var user = authState.User;
            if (token == null && user.Identity.IsAuthenticated)
            {
                JSRuntime.InvokeAsync<object>("console.log", new object[] { $"Oninitalized" });
                token = Bus.Subscribe<PayloadEvent<string>>(s =>
                {
                    JSRuntime.InvokeAsync<object>("console.log", new object[] { $"Timer event #{s.Payload}" });
                    JSRuntime.InvokeAsync<object>("notify", new object[] { $"Timer event #{s.Payload}" });
                });
                // SetTimer();

            }
            return base.OnAfterRenderAsync(firstRender);
        }

        private void SetTimer()
        {

            if (aTimer != null)
                return;
            // Create a timer with a two second interval.
            aTimer = new Timer(10000);
            // Hook up the Elapsed event for the timer.
            aTimer.Elapsed += OnTimedEvent;
            // aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        // TODO: Other way to manage the lifecycle than on componentlevel?
        public void Dispose()
        {
            if (token == null)
                return;
            Bus.Unsubscribe(token);
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            count++;
            Bus.Publish(new PayloadEvent<string>($"Timed Event {count}"));
        }
    }
}
