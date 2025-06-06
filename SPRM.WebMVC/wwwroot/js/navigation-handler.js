/**
 * Navigation handler for SPRM system
 * This script ensures sidebar state is maintained across page navigations
 */

document.addEventListener('DOMContentLoaded', function() {
    // Attach event listeners to all internal links
    const internalLinks = document.querySelectorAll('a[href^="/"], a[href^="./"], a[href^="../"], a[asp-controller]');
    
    internalLinks.forEach(link => {
        link.addEventListener('click', function(e) {
            // Don't interfere with links that open in new tabs or have special behavior
            if (e.ctrlKey || e.metaKey || e.shiftKey || link.target === '_blank') {
                return;
            }
            
            // Save sidebar state before navigation
            if (window.sidebarManager) {
                const sidebarState = {
                    isCollapsed: window.sidebarManager.isCollapsed,
                    isMobile: window.sidebarManager.isMobile,
                    isMobileOpen: window.sidebarManager.isMobileOpen
                };
                
                // Store state in sessionStorage for retrieval after page load
                sessionStorage.setItem('sidebarState', JSON.stringify(sidebarState));
            }
        });
    });
    
    // Restore sidebar state after navigation
    const savedState = sessionStorage.getItem('sidebarState');
    if (savedState && window.sidebarManager) {
        try {
            const state = JSON.parse(savedState);
            
            // Only apply non-mobile states (mobile states should be determined by screen size)
            if (!window.sidebarManager.isMobile && state.isCollapsed !== undefined) {
                if (state.isCollapsed) {
                    window.sidebarManager.isCollapsed = true;
                    window.sidebarManager.sidebar?.classList.add('collapsed');
                    document.body.classList.add('sidebar-collapsed');
                    document.body.classList.remove('sidebar-open');
                } else {
                    window.sidebarManager.isCollapsed = false;
                    window.sidebarManager.sidebar?.classList.remove('collapsed');
                    document.body.classList.remove('sidebar-collapsed');
                    document.body.classList.add('sidebar-open');
                }
                
                // Re-optimize main content with a slight delay to ensure DOM is ready
                setTimeout(() => {
                    window.sidebarManager.optimizeMainContent();
                    // Run it again after a short delay to make sure it sticks
                    setTimeout(() => window.sidebarManager.optimizeMainContent(), 200);
                }, 50);
            }
        } catch (e) {
            console.error('Error restoring sidebar state:', e);
        }
    }
});

// Add event handler to fix layout when AJAX content is loaded
document.addEventListener('ajaxComplete', function() {
    if (window.sidebarUtils && typeof window.sidebarUtils.refreshLayout === 'function') {
        window.sidebarUtils.refreshLayout();
    }
});

// Force layout refresh when images load (which can change layout)
window.addEventListener('load', function() {
    // Apply for both authenticated and non-authenticated users
    if (window.sidebarUtils && typeof window.sidebarUtils.refreshLayout === 'function') {
        // Delay slightly to allow images to affect layout
        setTimeout(window.sidebarUtils.refreshLayout, 100);
        // Apply multiple times to ensure consistency
        setTimeout(window.sidebarUtils.refreshLayout, 500);
        setTimeout(window.sidebarUtils.refreshLayout, 1000);
    } else {
        // If sidebarUtils doesn't exist (no sidebar/not authenticated), still optimize layout
        setTimeout(optimizeNonSidebarLayout, 100);
        setTimeout(optimizeNonSidebarLayout, 500);
    }
});

// Helper function to optimize layout for non-authenticated users
function optimizeNonSidebarLayout() {
    const mainContent = document.querySelector('.main-content');
    if (!mainContent) return;
    
    // For non-authenticated users, ensure content is centered
    if (!document.body.classList.contains('has-sidebar')) {
        mainContent.style.width = '100%';
        mainContent.style.maxWidth = '100%';
        mainContent.style.marginLeft = 'auto';
        mainContent.style.marginRight = 'auto';
        
        // Apply styles to containers
        const containers = mainContent.querySelectorAll('.container, .container-fluid');
        containers.forEach(container => {
            container.style.width = '100%';
            
            // Center containers with a max-width
            if (container.classList.contains('container')) {
                container.style.maxWidth = '1200px';
            } else if (container.classList.contains('container-fluid') && !container.classList.contains('full-width')) {
                container.style.maxWidth = '1400px';
            }
            container.style.marginLeft = 'auto';
            container.style.marginRight = 'auto';
            container.style.paddingLeft = window.innerWidth <= 991.98 ? '1rem' : '1.5rem';
            container.style.paddingRight = window.innerWidth <= 991.98 ? '1rem' : '1.5rem';
        });
    }
}

// Listen for popstate events (browser back/forward)
window.addEventListener('popstate', function() {
    if (window.sidebarUtils && typeof window.sidebarUtils.refreshLayout === 'function') {
        // Delay to allow DOM to update
        setTimeout(window.sidebarUtils.refreshLayout, 100);
    }
});
