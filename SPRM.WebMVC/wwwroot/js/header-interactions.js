/**
 * Header interactions for the SPRM system
 * Handles animations and interactions for the minimalist header
 */

document.addEventListener('DOMContentLoaded', function() {
    // Notification bell animation
    const notificationBell = document.querySelector('.notification-dropdown .nav-link i');
    const notificationBadge = document.querySelector('.notification-dropdown .badge');
    
    if (notificationBell && notificationBadge) {
        // If there are unread notifications, animate the bell
        if (notificationBadge.innerText && parseInt(notificationBadge.innerText) > 0) {
            setTimeout(() => {
                notificationBell.classList.add('notification-bell-animate');
                
                // Remove the animation class after it completes
                setTimeout(() => {
                    notificationBell.classList.remove('notification-bell-animate');
                }, 1000);
            }, 2000);
        }
    }
    
    // Handle dropdown click for mobile
    const dropdowns = document.querySelectorAll('.dropdown-toggle');
    dropdowns.forEach(dropdown => {
        dropdown.addEventListener('click', function(e) {
            if (window.innerWidth < 992) {
                if (this.getAttribute('aria-expanded') === 'true') {
                    // Already open, do nothing
                    return;
                }
                
                // Close any other open dropdowns
                dropdowns.forEach(otherDropdown => {
                    if (otherDropdown !== this && otherDropdown.getAttribute('aria-expanded') === 'true') {
                        otherDropdown.click();
                    }
                });
            }
        });
    });
    
    // Handle window resize for responsive header
    window.addEventListener('resize', function() {
        adjustHeaderForScreenSize();
    });
    
    // Initial adjustment
    adjustHeaderForScreenSize();
});

function adjustHeaderForScreenSize() {
    const breadcrumbWrapper = document.querySelector('.breadcrumb-wrapper');
    const breadcrumb = document.querySelector('.breadcrumb');
    
    if (!breadcrumbWrapper || !breadcrumb) return;
    
    // For very small screens, simplify the breadcrumb
    if (window.innerWidth < 576) {
        // Keep only first and last item if there are more than 2 items
        const items = breadcrumb.querySelectorAll('.breadcrumb-item');
        if (items.length > 2) {
            for (let i = 1; i < items.length - 1; i++) {
                items[i].style.display = 'none';
            }
        }
    } else {
        // Show all items
        const items = breadcrumb.querySelectorAll('.breadcrumb-item');
        items.forEach(item => {
            item.style.display = '';
        });
    }
}
