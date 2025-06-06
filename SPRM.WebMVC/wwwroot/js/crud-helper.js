/**
 * CRUD Operations Helper
 * Provides reusable functions for Create, Read, Update, Delete operations
 */

class CRUDHelper {
    constructor() {
        this.modals = new Map();
        this.loadingStates = new Map();
        this.init();
    }

    init() {
        this.setupEventListeners();
        this.initializeModals();
        this.setupFormValidation();
    }

    /**
     * Setup global event listeners
     */
    setupEventListeners() {
        document.addEventListener('click', (e) => {
            // Handle create buttons
            if (e.target.matches('[data-crud="create"]')) {
                e.preventDefault();
                this.handleCreate(e.target);
            }

            // Handle edit buttons
            if (e.target.matches('[data-crud="edit"]')) {
                e.preventDefault();
                this.handleEdit(e.target);
            }

            // Handle delete buttons
            if (e.target.matches('[data-crud="delete"]')) {
                e.preventDefault();
                this.handleDelete(e.target);
            }

            // Handle view buttons
            if (e.target.matches('[data-crud="view"]')) {
                e.preventDefault();
                this.handleView(e.target);
            }
        });

        // Handle form submissions
        document.addEventListener('submit', (e) => {
            if (e.target.matches('[data-crud-form]')) {
                e.preventDefault();
                this.handleFormSubmit(e.target);
            }
        });
    }

    /**
     * Initialize modals
     */
    initializeModals() {
        const modals = document.querySelectorAll('.modal[data-crud-modal]');
        modals.forEach(modal => {
            const modalId = modal.id;
            if (window.bootstrap) {
                this.modals.set(modalId, new bootstrap.Modal(modal));
            }
        });
    }

    /**
     * Handle create operation
     */
    handleCreate(button) {
        const modalId = button.getAttribute('data-target') || button.getAttribute('data-bs-target');
        const url = button.getAttribute('data-url');
        
        if (modalId) {
            // Show modal for create
            this.showModal(modalId.replace('#', ''));
        } else if (url) {
            // Redirect to create page
            window.location.href = url;
        }
    }

    /**
     * Handle edit operation
     */
    async handleEdit(button) {
        const modalId = button.getAttribute('data-target') || button.getAttribute('data-bs-target');
        const url = button.getAttribute('data-url');
        const id = button.getAttribute('data-id');
        
        if (modalId) {
            // Load data and show modal
            await this.loadDataForEdit(modalId.replace('#', ''), id, url);
        } else if (url) {
            // Redirect to edit page
            window.location.href = url;
        }
    }

    /**
     * Handle delete operation
     */
    async handleDelete(button) {
        const url = button.getAttribute('data-url');
        const id = button.getAttribute('data-id');
        const message = button.getAttribute('data-message') || 'Are you sure you want to delete this item?';
        
        const confirmed = await this.showConfirmDialog(message);
        if (confirmed) {
            await this.performDelete(url, id);
        }
    }

    /**
     * Handle view operation
     */
    async handleView(button) {
        const modalId = button.getAttribute('data-target') || button.getAttribute('data-bs-target');
        const url = button.getAttribute('data-url');
        const id = button.getAttribute('data-id');
        
        if (modalId) {
            // Load data and show modal
            await this.loadDataForView(modalId.replace('#', ''), id, url);
        } else if (url) {
            // Redirect to view page
            window.location.href = url;
        }
    }

    /**
     * Handle form submission
     */
    async handleFormSubmit(form) {
        const url = form.action;
        const method = form.method || 'POST';
        const formData = new FormData(form);
        
        // Show loading state
        this.setLoadingState(form, true);
        
        try {
            const response = await this.makeRequest(url, method, formData);
            
            if (response.success) {
                this.showToast('Operation completed successfully!', 'success');
                
                // Close modal if it's in a modal
                const modal = form.closest('.modal');
                if (modal) {
                    this.hideModal(modal.id);
                }
                
                // Reload page or update UI
                if (response.reload) {
                    window.location.reload();
                } else if (response.redirect) {
                    window.location.href = response.redirect;
                }
            } else {
                this.showToast(response.message || 'Operation failed', 'error');
            }
        } catch (error) {
            console.error('Form submission error:', error);
            this.showToast('An error occurred. Please try again.', 'error');
        } finally {
            this.setLoadingState(form, false);
        }
    }

    /**
     * Load data for edit
     */
    async loadDataForEdit(modalId, id, url) {
        try {
            this.showModal(modalId);
            const modal = document.getElementById(modalId);
            const form = modal.querySelector('form');
            
            if (form) {
                this.setLoadingState(form, true);
            }
            
            const response = await fetch(`${url}/${id}`);
            const data = await response.json();
            
            if (data.success) {
                this.populateForm(form, data.data);
            } else {
                this.showToast(data.message || 'Failed to load data', 'error');
            }
        } catch (error) {
            console.error('Load edit data error:', error);
            this.showToast('Failed to load data', 'error');
        } finally {
            const modal = document.getElementById(modalId);
            const form = modal.querySelector('form');
            if (form) {
                this.setLoadingState(form, false);
            }
        }
    }

    /**
     * Load data for view
     */
    async loadDataForView(modalId, id, url) {
        try {
            this.showModal(modalId);
            const modal = document.getElementById(modalId);
            const content = modal.querySelector('.modal-body');
            
            if (content) {
                content.innerHTML = '<div class="text-center"><i class="fas fa-spinner fa-spin"></i> Loading...</div>';
            }
            
            const response = await fetch(`${url}/${id}`);
            const data = await response.json();
            
            if (data.success) {
                this.populateViewContent(content, data.data);
            } else {
                content.innerHTML = '<div class="text-center text-danger">Failed to load data</div>';
            }
        } catch (error) {
            console.error('Load view data error:', error);
            const modal = document.getElementById(modalId);
            const content = modal.querySelector('.modal-body');
            if (content) {
                content.innerHTML = '<div class="text-center text-danger">Failed to load data</div>';
            }
        }
    }

    /**
     * Perform delete operation
     */
    async performDelete(url, id) {
        try {
            const response = await this.makeRequest(`${url}/${id}`, 'DELETE');
            
            if (response.success) {
                this.showToast('Item deleted successfully!', 'success');
                
                // Remove row from table if present
                const row = document.querySelector(`[data-id="${id}"]`);
                if (row) {
                    row.closest('tr').remove();
                }
                
                // Reload page if needed
                if (response.reload) {
                    setTimeout(() => window.location.reload(), 1000);
                }
            } else {
                this.showToast(response.message || 'Delete failed', 'error');
            }
        } catch (error) {
            console.error('Delete error:', error);
            this.showToast('An error occurred while deleting', 'error');
        }
    }

    /**
     * Make HTTP request
     */
    async makeRequest(url, method = 'GET', data = null) {
        const options = {
            method,
            headers: {
                'X-Requested-With': 'XMLHttpRequest'
            }
        };

        if (data && method !== 'GET') {
            if (data instanceof FormData) {
                options.body = data;
            } else {
                options.headers['Content-Type'] = 'application/json';
                options.body = JSON.stringify(data);
            }
        }

        // Add CSRF token if available
        const token = document.querySelector('input[name="__RequestVerificationToken"]');
        if (token && data instanceof FormData) {
            data.append('__RequestVerificationToken', token.value);
        }

        const response = await fetch(url, options);
        return await response.json();
    }

    /**
     * Show modal
     */
    showModal(modalId) {
        const modal = this.modals.get(modalId);
        if (modal) {
            modal.show();
        } else if (window.bootstrap) {
            const modalElement = document.getElementById(modalId);
            if (modalElement) {
                const modal = new bootstrap.Modal(modalElement);
                this.modals.set(modalId, modal);
                modal.show();
            }
        }
    }

    /**
     * Hide modal
     */
    hideModal(modalId) {
        const modal = this.modals.get(modalId);
        if (modal) {
            modal.hide();
        }
    }

    /**
     * Show confirm dialog
     */
    showConfirmDialog(message) {
        return new Promise((resolve) => {
            if (window.Swal) {
                window.Swal.fire({
                    title: 'Confirm',
                    text: message,
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#ef4444',
                    cancelButtonColor: '#6b7280',
                    confirmButtonText: 'Yes, delete it!'
                }).then((result) => {
                    resolve(result.isConfirmed);
                });
            } else {
                resolve(confirm(message));
            }
        });
    }

    /**
     * Show toast notification
     */
    showToast(message, type = 'info') {
        if (window.toastr) {
            window.toastr[type](message);
        } else if (window.Swal) {
            window.Swal.fire({
                text: message,
                icon: type === 'error' ? 'error' : type === 'success' ? 'success' : 'info',
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 3000,
                timerProgressBar: true
            });
        } else {
            alert(message);
        }
    }

    /**
     * Set loading state
     */
    setLoadingState(element, isLoading) {
        if (isLoading) {
            element.classList.add('loading');
            const buttons = element.querySelectorAll('button[type="submit"]');
            buttons.forEach(btn => {
                btn.disabled = true;
                btn.innerHTML = '<i class="fas fa-spinner fa-spin me-2"></i>Loading...';
            });
        } else {
            element.classList.remove('loading');
            const buttons = element.querySelectorAll('button[type="submit"]');
            buttons.forEach(btn => {
                btn.disabled = false;
                btn.innerHTML = btn.getAttribute('data-original-text') || 'Submit';
            });
        }
    }

    /**
     * Populate form with data
     */
    populateForm(form, data) {
        if (!form || !data) return;

        Object.keys(data).forEach(key => {
            const field = form.querySelector(`[name="${key}"]`);
            if (field) {
                if (field.type === 'checkbox') {
                    field.checked = !!data[key];
                } else if (field.type === 'radio') {
                    if (field.value === data[key]) {
                        field.checked = true;
                    }
                } else {
                    field.value = data[key];
                }
            }
        });
    }

    /**
     * Populate view content
     */
    populateViewContent(container, data) {
        if (!container || !data) return;

        let html = '<div class="row">';
        Object.keys(data).forEach(key => {
            const value = data[key];
            if (value !== null && value !== undefined) {
                html += `
                    <div class="col-md-6 mb-3">
                        <strong>${this.formatLabel(key)}:</strong>
                        <div>${this.formatValue(value)}</div>
                    </div>
                `;
            }
        });
        html += '</div>';

        container.innerHTML = html;
    }

    /**
     * Format label for display
     */
    formatLabel(key) {
        return key.replace(/([A-Z])/g, ' $1')
                  .replace(/^./, str => str.toUpperCase())
                  .trim();
    }

    /**
     * Format value for display
     */
    formatValue(value) {
        if (typeof value === 'boolean') {
            return value ? '<span class="badge bg-success">Yes</span>' : '<span class="badge bg-secondary">No</span>';
        }
        if (typeof value === 'object' && value instanceof Date) {
            return value.toLocaleDateString();
        }
        return value;
    }

    /**
     * Setup form validation
     */
    setupFormValidation() {
        // Add real-time validation for forms
        document.addEventListener('blur', (e) => {
            if (e.target.matches('input, select, textarea')) {
                this.validateField(e.target);
            }
        }, true);
    }

    /**
     * Validate individual field
     */
    validateField(field) {
        const value = field.value.trim();
        let isValid = true;
        let message = '';

        // Required validation
        if (field.hasAttribute('required') && !value) {
            isValid = false;
            message = 'This field is required';
        }

        // Email validation
        if (field.type === 'email' && value && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value)) {
            isValid = false;
            message = 'Please enter a valid email address';
        }

        // Minimum length validation
        const minLength = field.getAttribute('minlength');
        if (minLength && value.length < parseInt(minLength)) {
            isValid = false;
            message = `Minimum length is ${minLength} characters`;
        }

        this.showFieldValidation(field, isValid, message);
        return isValid;
    }

    /**
     * Show field validation result
     */
    showFieldValidation(field, isValid, message) {
        // Remove existing validation classes and messages
        field.classList.remove('is-valid', 'is-invalid');
        const existingFeedback = field.parentNode.querySelector('.invalid-feedback');
        if (existingFeedback) {
            existingFeedback.remove();
        }

        if (!isValid) {
            field.classList.add('is-invalid');
            if (message) {
                const feedback = document.createElement('div');
                feedback.className = 'invalid-feedback';
                feedback.textContent = message;
                field.parentNode.appendChild(feedback);
            }
        } else if (field.value.trim()) {
            field.classList.add('is-valid');
        }
    }
}

// Quick search functionality
class QuickSearch {
    constructor(selector, options = {}) {
        this.searchInput = document.querySelector(selector);
        this.options = {
            delay: 300,
            minLength: 2,
            ...options
        };
        this.timeout = null;
        
        if (this.searchInput) {
            this.init();
        }
    }

    init() {
        this.searchInput.addEventListener('input', (e) => {
            clearTimeout(this.timeout);
            this.timeout = setTimeout(() => {
                this.performSearch(e.target.value);
            }, this.options.delay);
        });
    }

    performSearch(query) {
        if (query.length < this.options.minLength) {
            this.clearResults();
            return;
        }

        // Show loading state
        this.showLoading();

        // Perform search
        if (this.options.onSearch) {
            this.options.onSearch(query);
        }
    }

    showLoading() {
        const resultsContainer = document.querySelector(this.options.resultsSelector);
        if (resultsContainer) {
            resultsContainer.innerHTML = '<div class="text-center p-3"><i class="fas fa-spinner fa-spin"></i> Searching...</div>';
        }
    }

    clearResults() {
        const resultsContainer = document.querySelector(this.options.resultsSelector);
        if (resultsContainer) {
            resultsContainer.innerHTML = '';
        }
    }
}

// Initialize on DOM ready
document.addEventListener('DOMContentLoaded', function() {
    // Initialize CRUD helper
    window.crudHelper = new CRUDHelper();
    
    // Initialize quick search if search input exists
    const searchInput = document.querySelector('.quick-search input');
    if (searchInput) {
        window.quickSearch = new QuickSearch('.quick-search input', {
            resultsSelector: '.search-results',
            onSearch: (query) => {
                // Implement search logic here
                console.log('Searching for:', query);
            }
        });
    }

    // Add smooth scrolling to anchor links
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        });
    });

    // Initialize tooltips if Bootstrap is available
    if (window.bootstrap && bootstrap.Tooltip) {
        const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
        tooltipTriggerList.map(function (tooltipTriggerEl) {
            return new bootstrap.Tooltip(tooltipTriggerEl);
        });
    }
});

// Export for use in other scripts
window.CRUDHelper = CRUDHelper;
window.QuickSearch = QuickSearch;
