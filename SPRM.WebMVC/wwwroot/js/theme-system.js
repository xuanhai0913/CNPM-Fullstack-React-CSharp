/**
 * Theme System - Dark/Light Mode Toggle
 * Handles theme switching and persistence
 */

class ThemeManager {
    constructor() {
        this.currentTheme = this.getStoredTheme() || this.getPreferredTheme();
        this.themeToggleBtn = null;
        this.themeIcon = null;
        this.init();
    }

    init() {
        // Apply initial theme
        this.setTheme(this.currentTheme);
        
        // Wait for DOM to be ready
        if (document.readyState === 'loading') {
            document.addEventListener('DOMContentLoaded', () => this.setupUI());
        } else {
            this.setupUI();
        }
        
        // Listen for system theme changes
        window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', (e) => {
            if (!this.getStoredTheme()) {
                this.setTheme(e.matches ? 'dark' : 'light');
            }
        });
    }

    setupUI() {
        this.themeToggleBtn = document.getElementById('themeToggle');
        this.themeIcon = document.getElementById('themeIcon');
        
        if (this.themeToggleBtn) {
            this.themeToggleBtn.addEventListener('click', () => this.toggleTheme());
            this.updateToggleIcon();
        }
    }

    getStoredTheme() {
        return localStorage.getItem('sprm-theme');
    }

    setStoredTheme(theme) {
        localStorage.setItem('sprm-theme', theme);
    }

    getPreferredTheme() {
        return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
    }

    setTheme(theme) {
        this.currentTheme = theme;
        document.documentElement.setAttribute('data-theme', theme);
        this.setStoredTheme(theme);
        this.updateToggleIcon();
        
        // Dispatch custom event for other components
        window.dispatchEvent(new CustomEvent('themeChanged', { 
            detail: { theme: theme } 
        }));
        
        // Update meta theme-color for mobile browsers
        this.updateMetaThemeColor(theme);
    }

    toggleTheme() {
        const newTheme = this.currentTheme === 'dark' ? 'light' : 'dark';
        this.setTheme(newTheme);
        
        // Add animation class for smooth transition
        if (this.themeToggleBtn) {
            this.themeToggleBtn.classList.add('toggling');
            setTimeout(() => {
                this.themeToggleBtn.classList.remove('toggling');
            }, 300);
        }
    }

    updateToggleIcon() {
        if (!this.themeIcon) return;
        
        // Update icon and tooltip based on current theme
        if (this.currentTheme === 'dark') {
            this.themeIcon.className = 'fas fa-sun';
            if (this.themeToggleBtn) {
                this.themeToggleBtn.title = 'Chuyển sang giao diện sáng';
                this.themeToggleBtn.setAttribute('aria-label', 'Chuyển sang giao diện sáng');
            }
        } else {
            this.themeIcon.className = 'fas fa-moon';
            if (this.themeToggleBtn) {
                this.themeToggleBtn.title = 'Chuyển sang giao diện tối';
                this.themeToggleBtn.setAttribute('aria-label', 'Chuyển sang giao diện tối');
            }
        }
    }

    updateMetaThemeColor(theme) {
        let metaThemeColor = document.querySelector('meta[name="theme-color"]');
        if (!metaThemeColor) {
            metaThemeColor = document.createElement('meta');
            metaThemeColor.name = 'theme-color';
            document.head.appendChild(metaThemeColor);
        }
        
        metaThemeColor.content = theme === 'dark' ? '#2d2d30' : '#ffffff';
    }

    getCurrentTheme() {
        return this.currentTheme;
    }

    // Method to manually set theme (for external use)
    setThemeManually(theme) {
        if (theme === 'dark' || theme === 'light') {
            this.setTheme(theme);
        }
    }

    // Method to reset to system preference
    resetToSystemTheme() {
        localStorage.removeItem('sprm-theme');
        this.setTheme(this.getPreferredTheme());
    }
}

// Initialize theme manager
const themeManager = new ThemeManager();

// Make it globally available
window.themeManager = themeManager;

// Additional helper functions for theme-aware components
window.addEventListener('themeChanged', function(e) {
    // Update any theme-dependent charts, maps, or other components
    console.log('Theme changed to:', e.detail.theme);
    
    // Example: Update chart colors if you have any
    if (window.updateChartsTheme) {
        window.updateChartsTheme(e.detail.theme);
    }
    
    // Update any Monaco editors or code editors
    if (window.updateEditorsTheme) {
        window.updateEditorsTheme(e.detail.theme);
    }
});

// Utility function to get current theme for external scripts
window.getCurrentTheme = function() {
    return themeManager.getCurrentTheme();
};

// Keyboard shortcut for theme toggle (Ctrl/Cmd + Shift + T)
document.addEventListener('keydown', function(e) {
    if ((e.ctrlKey || e.metaKey) && e.shiftKey && e.key === 'T') {
        e.preventDefault();
        themeManager.toggleTheme();
    }
});

// Add CSS class for theme toggle animation
const style = document.createElement('style');
style.textContent = `
    .theme-toggle-btn.toggling {
        transform: scale(0.9) rotate(180deg);
    }
    
    .theme-toggle-btn.toggling #themeIcon {
        opacity: 0.7;
    }
`;
document.head.appendChild(style);
