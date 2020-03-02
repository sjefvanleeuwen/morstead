﻿using Microsoft.Extensions.DependencyInjection;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Controllers;
using Vs.VoorzieningenEnRegelingen.BurgerPortaal.Objects;

namespace Vs.VoorzieningenEnRegelingen.BurgerPortaal
{
    public static class Initializer
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<ISequenceController, SequenceController>();
            services.AddScoped<ISequence, Sequence>();
        }
    }
}