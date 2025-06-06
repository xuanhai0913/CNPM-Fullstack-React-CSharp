/**
 * Non-sidebar layout handler
 * This script ensures proper layout for users without sidebar (not authenticated)
 */

document.addEventListener('DOMContentLoaded', function() {
    // Only apply for non-authenticated users (no sidebar)
    if (!document.body.classList.contains('has-sidebar')) {
        optimizeNonSidebarLayout();
        
        // Also handle window resize for responsive adjustments
        window.addEventListener('resize', function() {
            optimizeNonSidebarLayout();
        });
    }
});

function optimizeNonSidebarLayout() {
    const mainContent = document.querySelector('.main-content');
    if (!mainContent) return;
    
    // Ensure content is centered
    mainContent.style.width = '100%';
    mainContent.style.maxWidth = '100%';
    mainContent.style.marginLeft = 'auto';
    mainContent.style.marginRight = 'auto';
    
    // Apply styles to containers for better centering
    const containers = mainContent.querySelectorAll('.container, .container-fluid');
    containers.forEach(container => {
        container.style.width = '100%';
        
        // Center containers with appropriate max-width
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

// Make sure layout is optimized when images load
window.addEventListener('load', function() {
    if (!document.body.classList.contains('has-sidebar')) {
        setTimeout(optimizeNonSidebarLayout, 100);
        setTimeout(optimizeNonSidebarLayout, 500);
    }
});
