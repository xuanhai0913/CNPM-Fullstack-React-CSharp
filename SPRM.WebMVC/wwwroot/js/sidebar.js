// Sidebar functionality
class SidebarManager {
    constructor() {
        this.sidebar = document.getElementById('sidebar');
        this.sidebarToggle = document.getElementById('sidebarToggle');
        this.sidebarOverlay = document.getElementById('sidebarOverlay');
        this.mobileMenuToggle = document.querySelector('.mobile-menu-toggle');
        this.body = document.body;
        
        // Only initialize if sidebar exists (user is authenticated)
        if (!this.sidebar) {
            return;
        }
        
        this.isMobile = window.innerWidth <= 991.98; // Thay đổi breakpoint
        this.isCollapsed = localStorage.getItem('sidebar-collapsed') === 'true';
        this.isMobileOpen = false;
        
        this.init();
    }
    
    init() {
        // Only run if sidebar exists
        if (!this.sidebar) return;
        
        // Add sidebar class to body
        document.body.classList.add('has-sidebar');
        
        this.setupEventListeners();
        this.setupResponsive();
        this.initializeSidebar();
        this.setupSubmenuToggle();
        this.setupTooltips();
        this.setupAnimation(); // Thêm animation setup
    }
    
    setupEventListeners() {
        // Only setup if sidebar exists
        if (!this.sidebar) return;
        
        // Sidebar toggle button
        if (this.sidebarToggle) {
            this.sidebarToggle.addEventListener('click', () => this.toggleSidebar());
        }
        
        // Mobile menu toggle
        if (this.mobileMenuToggle) {
            this.mobileMenuToggle.addEventListener('click', () => this.toggleMobileSidebar());
        }
        
        // Overlay click to close mobile sidebar
        if (this.sidebarOverlay) {
            this.sidebarOverlay.addEventListener('click', () => this.closeMobileSidebar());
        }
        
        // Window resize handler
        window.addEventListener('resize', () => this.handleResize());
        
        // Escape key to close mobile sidebar
        document.addEventListener('keydown', (e) => {
            if (e.key === 'Escape' && this.isMobileOpen) {
                this.closeMobileSidebar();
            }
        });
    }
    
    setupResponsive() {
        this.isMobile = window.innerWidth <= 991.98; // Cập nhật breakpoint
        
        if (this.isMobile) {
            this.body.classList.add('sidebar-mobile');
            this.body.classList.remove('sidebar-open', 'sidebar-collapsed');
            this.sidebar?.classList.add('mobile-hidden');
        } else {
            this.body.classList.remove('sidebar-mobile');
            this.sidebar?.classList.remove('mobile-hidden', 'mobile-open');
            
            if (this.isCollapsed) {
                this.body.classList.add('sidebar-collapsed');
                this.body.classList.remove('sidebar-open');
                this.sidebar?.classList.add('collapsed');
            } else {
                this.body.classList.add('sidebar-open');
                this.body.classList.remove('sidebar-collapsed');
                this.sidebar?.classList.remove('collapsed');
            }
        }
        
        // Tối ưu hiển thị main content
        this.optimizeMainContent();
    }
    
    optimizeMainContent() {
        const mainContent = document.querySelector('.main-content');
        if (!mainContent) return;
        
        // Ensure main content has appropriate width and max-width
        if (this.isMobile) {
            mainContent.style.width = '100%';
            mainContent.style.maxWidth = '100%';
            mainContent.style.marginLeft = '0';
        } else if (this.isCollapsed) {
            mainContent.style.width = 'calc(100% - 80px)';
            mainContent.style.maxWidth = 'none';
            mainContent.style.marginLeft = '80px';
        } else {
            mainContent.style.width = 'calc(100% - 280px)';
            mainContent.style.maxWidth = 'none';
            mainContent.style.marginLeft = '280px';
        }
        
        // Ensure containers inside are also responsive
        const containers = mainContent.querySelectorAll('.container, .container-fluid');
        containers.forEach(container => {
            container.style.width = '100%';
            container.style.maxWidth = '100%';
            container.style.paddingLeft = '1rem';
            container.style.paddingRight = '1rem';
        });
    }
    
    // Thêm method setup animation
    setupAnimation() {
        // Thêm stagger animation cho nav items
        const navItems = this.sidebar?.querySelectorAll('.nav-item');
        navItems?.forEach((item, index) => {
            item.style.animationDelay = `${index * 0.1}s`;
        });
    }
    
    initializeSidebar() {
        if (!this.isMobile && this.isCollapsed) {
            this.sidebar?.classList.add('collapsed');
            this.body.classList.add('sidebar-collapsed');
        } else if (!this.isMobile) {
            this.body.classList.add('sidebar-open');
        }
    }
    
    toggleSidebar() {
        if (this.isMobile) {
            this.toggleMobileSidebar();
        } else {
            this.toggleDesktopSidebar();
        }
    }
    
    toggleDesktopSidebar() {
        this.isCollapsed = !this.isCollapsed;
        localStorage.setItem('sidebar-collapsed', this.isCollapsed.toString());
        
        if (this.isCollapsed) {
            this.sidebar?.classList.add('collapsed');
            this.body.classList.remove('sidebar-open');
            this.body.classList.add('sidebar-collapsed');
        } else {
            this.sidebar?.classList.remove('collapsed');
            this.body.classList.remove('sidebar-collapsed');
            this.body.classList.add('sidebar-open');
        }
        
        // Close all submenus when collapsing
        if (this.isCollapsed) {
            this.closeAllSubmenus();
        }
    }
    
    toggleMobileSidebar() {
        this.isMobileOpen = !this.isMobileOpen;
        
        if (this.isMobileOpen) {
            this.openMobileSidebar();
        } else {
            this.closeMobileSidebar();
        }
    }
    
    openMobileSidebar() {
        this.sidebar?.classList.remove('mobile-hidden');
        this.sidebar?.classList.add('mobile-open');
        this.sidebarOverlay?.classList.add('active');
        this.body.style.overflow = 'hidden';
        this.isMobileOpen = true;
    }
    
    closeMobileSidebar() {
        this.sidebar?.classList.add('mobile-hidden');
        this.sidebar?.classList.remove('mobile-open');
        this.sidebarOverlay?.classList.remove('active');
        this.body.style.overflow = '';
        this.isMobileOpen = false;
    }
    
    handleResize() {
        const oldIsMobile = this.isMobile;
        this.isMobile = window.innerWidth <= 991.98; // Cập nhật breakpoint
        
        if (oldIsMobile !== this.isMobile) {
            this.setupResponsive();
            
            // Nếu chuyển từ mobile sang desktop
            if (!this.isMobile) {
                this.closeMobileSidebar();
            }
            
            // Smooth transition khi thay đổi kích thước
            this.sidebar?.style.setProperty('transition', 'all 0.3s cubic-bezier(0.4, 0, 0.2, 1)');
            
            setTimeout(() => {
                this.optimizeMainContent();
            }, 300);
        }
    }
    
    setupSubmenuToggle() {
        const submenuToggle = document.querySelectorAll('[data-toggle="submenu"]');
        
        submenuToggle.forEach(toggle => {
            toggle.addEventListener('click', (e) => {
                e.preventDefault();
                
                // Don't toggle submenu if sidebar is collapsed on desktop
                if (!this.isMobile && this.isCollapsed) {
                    return;
                }
                
                const isActive = toggle.classList.contains('active');
                
                // Close other submenus if opening this one
                if (!isActive) {
                    this.closeAllSubmenus();
                }
                
                toggle.classList.toggle('active');
            });
        });
    }
    
    closeAllSubmenus() {
        const activeSubmenus = document.querySelectorAll('[data-toggle="submenu"].active');
        activeSubmenus.forEach(submenu => {
            submenu.classList.remove('active');
        });
    }
    
    setupTooltips() {
        const navLinks = document.querySelectorAll('.sidebar .nav-link');
        
        navLinks.forEach(link => {
            const textElement = link.querySelector('.nav-text');
            if (textElement) {
                link.setAttribute('data-tooltip', textElement.textContent.trim());
            }
        });
    }
    
    // Method to set active navigation item
    setActiveNavItem(controller, action = null) {
        // Remove all active classes
        const allNavLinks = document.querySelectorAll('.sidebar .nav-link, .sidebar .submenu-link');
        allNavLinks.forEach(link => link.classList.remove('active'));
        
        // Find and activate the correct navigation item
        let targetSelector = '';
        
        if (action) {
            targetSelector = `[href*="${controller}"][href*="${action}"]`;
        } else {
            targetSelector = `[href*="${controller}"]`;
        }
        
        const targetLink = document.querySelector(`.sidebar ${targetSelector}`);
        if (targetLink) {
            targetLink.classList.add('active');
            
            // If it's a submenu link, also activate the parent
            if (targetLink.classList.contains('submenu-link')) {
                const parentSubmenu = targetLink.closest('.nav-item').querySelector('[data-toggle="submenu"]');
                if (parentSubmenu) {
                    parentSubmenu.classList.add('active');
                }
            }
        }
    }
    
    // Method to manually open/close sidebar
    open() {
        if (this.isMobile) {
            this.openMobileSidebar();
        } else {
            this.isCollapsed = false;
            localStorage.setItem('sidebar-collapsed', 'false');
            this.sidebar?.classList.remove('collapsed');
            this.body.classList.remove('sidebar-collapsed');
            this.body.classList.add('sidebar-open');
        }
    }
    
    close() {
        if (this.isMobile) {
            this.closeMobileSidebar();
        } else {
            this.isCollapsed = true;
            localStorage.setItem('sidebar-collapsed', 'true');
            this.sidebar?.classList.add('collapsed');
            this.body.classList.remove('sidebar-open');
            this.body.classList.add('sidebar-collapsed');
            this.closeAllSubmenus();
        }
    }
    
    // Method to check if sidebar is open
    isOpen() {
        if (this.isMobile) {
            return this.isMobileOpen;
        } else {
            return !this.isCollapsed;
        }
    }
}

// Initialize sidebar when DOM is loaded
document.addEventListener('DOMContentLoaded', function() {
    // Only create mobile toggle if sidebar exists
    const sidebar = document.getElementById('sidebar');
    if (sidebar && !document.querySelector('.mobile-menu-toggle')) {
        const mobileToggle = document.createElement('button');
        mobileToggle.className = 'mobile-menu-toggle';
        mobileToggle.innerHTML = '<i class="fas fa-bars"></i>';
        mobileToggle.setAttribute('aria-label', 'Toggle mobile menu');
        document.body.appendChild(mobileToggle);
    }
    
    // Initialize sidebar manager (will only work if sidebar exists)
    window.sidebarManager = new SidebarManager();
    
    // Auto-set active navigation based on current URL (only if sidebar exists)
    if (sidebar) {
        const currentPath = window.location.pathname;
        const pathParts = currentPath.split('/').filter(part => part);
        
        if (pathParts.length >= 1) {
            const controller = pathParts[0];
            const action = pathParts.length >= 2 ? pathParts[1] : null;
            window.sidebarManager.setActiveNavItem(controller, action);
        }
    }
});

// Ensure overlay is shown/hidden with sidebar on mobile
// Patch: Listen for sidebar open/close and toggle overlay class
(function() {
    document.addEventListener('DOMContentLoaded', function() {
        const sidebarOverlay = document.getElementById('sidebarOverlay');
        if (!sidebarOverlay || !window.sidebarManager) return;

        // Patch open/close to also toggle overlay
        const origOpenMobileSidebar = window.sidebarManager.openMobileSidebar.bind(window.sidebarManager);
        const origCloseMobileSidebar = window.sidebarManager.closeMobileSidebar.bind(window.sidebarManager);

        window.sidebarManager.openMobileSidebar = function() {
            origOpenMobileSidebar();
            sidebarOverlay.classList.remove('hidden');
            sidebarOverlay.classList.add('active');
        };
        window.sidebarManager.closeMobileSidebar = function() {
            origCloseMobileSidebar();
            sidebarOverlay.classList.add('hidden');
            sidebarOverlay.classList.remove('active');
        };
    });
})();

// Global utility functions for sidebar (with safety checks)
window.sidebarUtils = {
    toggle: function() {
        if (window.sidebarManager && window.sidebarManager.sidebar) {
            window.sidebarManager.toggleSidebar();
        }
    },
    
    openMobile: function() {
        if (window.sidebarManager && window.sidebarManager.sidebar) {
            window.sidebarManager.openMobileSidebar();
        }
    },
    
    closeMobile: function() {
        if (window.sidebarManager && window.sidebarManager.sidebar) {
            window.sidebarManager.closeMobileSidebar();
        }
    },
    
    setActive: function(controller, action) {
        if (window.sidebarManager && window.sidebarManager.sidebar) {
            window.sidebarManager.setActiveNavItem(controller, action);
        }
    }
};

// Utility functions for external use
window.sidebarUtils = {
    toggle: () => window.sidebarManager?.toggleSidebar(),
    open: () => window.sidebarManager?.open(),
    close: () => window.sidebarManager?.close(),
    isOpen: () => window.sidebarManager?.isOpen(),
    setActive: (controller, action) => window.sidebarManager?.setActiveNavItem(controller, action)
};
