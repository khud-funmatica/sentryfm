using UnityEditor;

namespace Funmatica.SentryFM
{
    public class SentryFMCreator
    {
        [MenuItem("Funmatica/Create/Sentry WebGL Options Configuration")]
        public static void CreateSentryWebGLOptionsConfiguration()
        {
            ScriptableObjectUtility.CreateAsset<SentryWebGLOptionsConfiguration>();
        }
    }
}
