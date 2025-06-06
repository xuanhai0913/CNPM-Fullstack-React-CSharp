/**
 * Layout and z-index manager for header, sidebar, and footer
 * Ensures proper layout and prevents overlap issues
 */

document.addEventListener('DOMContentLoaded', function() {
    // Adjust sidebar top padding based on header height
    adjustSidebarPadding();
    
    // Adjust footer position based on sidebar state
    adjustFooterPosition();
    
    // Add resize listener to readjust when window size changes
    window.addEventListener('resize', function() {
        adjustSidebarPadding();
        adjustFooterPosition();
    });
    
    // Add mutation observer to detect sidebar state changes
    observeSidebarChanges();
});

function adjustSidebarPadding() {
    const header = document.querySelector('.main-header');
    const sidebar = document.querySelector('.sidebar');
    
    if (!header || !sidebar) return;
    
    // Get header height
    const headerHeight = header.offsetHeight;
    sidebar.style.paddingTop = `${headerHeight}px`;
    
    // Also update CSS variable for consistency
    document.documentElement.style.setProperty('--header-height', `${headerHeight}px`);
    
    // Also adjust sidebar content max height
    const sidebarHeader = document.querySelector('.sidebar-header');
    const sidebarContent = document.querySelector('.sidebar-content');
    
    if (sidebarHeader && sidebarContent) {
        const sidebarHeaderHeight = sidebarHeader.offsetHeight;
        sidebarContent.style.maxHeight = `calc(100vh - ${headerHeight + sidebarHeaderHeight}px)`;
    }
}

function adjustFooterPosition() {
    const body = document.body;
    const footer = document.querySelector('footer');
    const sidebar = document.querySelector('.sidebar');
    
    if (!footer || !sidebar) return;
    
    const hasSidebar = body.classList.contains('has-sidebar');
    const isSidebarCollapsed = body.classList.contains('sidebar-collapsed');
    const isMobile = window.innerWidth <= 991.98;
    
    if (hasSidebar && !isMobile) {
        if (isSidebarCollapsed) {
            footer.style.paddingLeft = '80px';
        } else {
            footer.style.paddingLeft = '280px';
        }
    } else {
        footer.style.paddingLeft = '0';
    }
}

function observeSidebarChanges() {
    // Watch for sidebar state changes
    const body = document.body;
    
    const observer = new MutationObserver(function(mutations) {
        mutations.forEach(function(mutation) {
            if (mutation.attributeName === 'class') {
                // Body class changed, might be sidebar state change
                adjustFooterPosition();
            }
        });
    });
    
    // Start observing body for class changes
    observer.observe(body, { attributes: true });
}
