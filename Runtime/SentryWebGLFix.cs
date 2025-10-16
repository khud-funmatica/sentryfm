using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Funmatica.SentryFM
{
    /// <summary>
    /// Filters Sentry ANR errors when browser tab is inactive
    /// </summary>
    public class SentryWebGLFix : MonoBehaviour
    {
        public static bool IsVisible { get; private set; } = true;
        public static DateTime LastVisibleAt { get; private set; } = DateTime.UtcNow;
        public static int GracePeriodMs = 3000;

#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void InitPageVisibility();
#endif

        void Start()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            InitPageVisibility();
#endif
        }

        public void OnVisibilityChange(string visibleStr)
        {
            IsVisible = visibleStr == "1";
            if (IsVisible)
                LastVisibleAt = DateTime.UtcNow;
        }
    }
}
