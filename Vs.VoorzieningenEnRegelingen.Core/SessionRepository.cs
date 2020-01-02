using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Vs.VoorzieningenEnRegelingen.Core
{
    public class SessionRepository
    {
        private readonly IMemoryCache _cache;

        public TimeSpan SessionTimeOut { get; }
        public ServiceProvider _provider { get; }

        public SessionRepository(TimeSpan sessionTimeOut)
        {
            // make sure backend cache is still valid before sliding expiration on clients.
            SessionTimeOut = sessionTimeOut.Add(new TimeSpan(0,1,0));
            _provider = new ServiceCollection()
                .AddMemoryCache()
                .BuildServiceProvider();

            _cache = _provider.GetService<IMemoryCache>();
        }

        public void Persist(Guid sessionId, ParametersCollection parameters)
        {
            using (var entry = _cache.CreateEntry(sessionId))
            {
                entry.Value = parameters;
                entry.SlidingExpiration = SessionTimeOut;
            }
        }

        public ParametersCollection Retrieve(Guid sessionId)
        {
            ParametersCollection result;
            _cache.TryGetValue(sessionId, out result);
            return result;
        }
    }
}
