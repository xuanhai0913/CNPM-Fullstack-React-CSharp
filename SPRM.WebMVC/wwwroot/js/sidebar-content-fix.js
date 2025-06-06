/**
 * Sidebar content visibility fix
 * Ensures content doesn't get pushed off-screen when sidebar is toggled
 */

(function() {
    // Apply fixes when DOM is ready
    document.addEventListener('DOMContentLoaded', function() {
        // Fix for content overflow on sidebar toggle
        applyOverflowFixes();
        
        // Watch for sidebar changes
        watchSidebarChanges();
        
        // Handle window resize events
        window.addEventListener('resize', applyOverflowFixes);
    });
    
    // Apply overflow fixes to prevent content from being pushed off-screen
    function applyOverflowFixes() {
        const mainContent = document.querySelector('.main-content');
        if (!mainContent) return;
        
        // Ensure content width is calculated properly
        recalculateContentWidth();
        
        // Apply overflow hidden to container to prevent horizontal scrolling
        const contentContainers = mainContent.querySelectorAll('.container, .container-fluid');
        contentContainers.forEach(container => {
            container.style.overflowX = 'hidden';
            container.style.maxWidth = '100%';
        });
    }
    
    // Watch for sidebar state changes to fix layout issues
    function watchSidebarChanges() {
        // Use MutationObserver to detect sidebar toggle
        const body = document.body;
        
        const observer = new MutationObserver(function(mutations) {
            mutations.forEach(function(mutation) {
                if (mutation.attributeName === 'class') {
                    // Body class changed, check if sidebar state changed
                    const hasSidebarOpen = body.classList.contains('sidebar-open');
                    const hasSidebarCollapsed = body.classList.contains('sidebar-collapsed');
                    
                    // Apply fixes with slight delay to allow transitions to complete
                    setTimeout(recalculateContentWidth, 50);
                    setTimeout(recalculateContentWidth, 300);
                }
            });
        });
        
        // Start observing body for class changes
        observer.observe(body, { attributes: true });
    }
    
    // Recalculate content width based on sidebar state
    function recalculateContentWidth() {
        const mainContent = document.querySelector('.main-content');
        if (!mainContent) return;
        
        const body = document.body;
        const hasSidebar = body.classList.contains('has-sidebar');
        
        if (!hasSidebar) return;
        
        // Get window width
        const windowWidth = window.innerWidth;
        
        // Check sidebar state
        const isSidebarCollapsed = body.classList.contains('sidebar-collapsed');
        const isMobile = windowWidth <= 991.98;
        
        if (isMobile) {
            // Mobile - full width
            mainContent.style.width = '100%';
            mainContent.style.maxWidth = '100%';
            mainContent.style.marginLeft = '0';
        } else if (isSidebarCollapsed) {
            // Collapsed sidebar - subtract collapsed width
            mainContent.style.width = 'calc(100% - 80px)';
            mainContent.style.maxWidth = 'calc(100% - 80px)';
            mainContent.style.marginLeft = '80px';
        } else {
            // Expanded sidebar - subtract full width
            mainContent.style.width = 'calc(100% - 280px)';
            mainContent.style.maxWidth = 'calc(100% - 280px)';
            mainContent.style.marginLeft = '280px';
        }
    }
})();
