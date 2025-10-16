using System;
using Sentry;
using Sentry.Extensibility;
using Sentry.Unity;
using Sentry.Unity.Editor;
using UnityEngine;

namespace Funmatica.SentryFM
{
    /// <summary>
    /// Sentry configuration for filtering ANR errors on WebGL
    /// </summary>
    public class SentryWebGLOptionsConfiguration : SentryOptionsConfiguration
    {
        public override void Configure(SentryUnityOptions options)
        {
            options.AddEventProcessor(new WebGLAnrEventProcessor());
        }
    }

    /// <summary>
    /// Event processor for filtering ANR errors when browser tab is inactive
    /// </summary>
    public class WebGLAnrEventProcessor : ISentryEventProcessor
    {
        public SentryEvent Process(SentryEvent @event)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            if (@event.Exception is Sentry.Unity.Integrations.ApplicationNotRespondingException)
            {
                if (
                    !SentryWebGLFix.IsVisible
                    || (DateTime.UtcNow - SentryWebGLFix.LastVisibleAt).TotalMilliseconds
                        < SentryWebGLFix.GracePeriodMs
                )
                {
                    return null; // Filter ANR
                }
            }
#endif
            return @event;
        }
    }
}
