// JavaScript library for tracking browser tab visibility
// Used by SentryWebGLFix to detect when tab becomes visible/invisible
mergeInto(LibraryManager.library, {
    InitPageVisibility: function() {
        // Listen for browser tab visibility changes
        document.addEventListener('visibilitychange', function() {
            var isVisible = document.visibilityState === 'visible' ? '1' : '0';
            var go = 'SentryWebGLFix'; // GameObject name to send message to
            
            // Try different Unity instance names for compatibility
            if (typeof gameInstance !== 'undefined' && gameInstance.SendMessage) {
                gameInstance.SendMessage(go, 'OnVisibilityChange', isVisible);
            } else if (typeof unityInstance !== 'undefined' && unityInstance.SendMessage) {
                unityInstance.SendMessage(go, 'OnVisibilityChange', isVisible);
            }
        });
    }
});

