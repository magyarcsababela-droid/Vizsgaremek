// Global showToast helper placed in a separate JS file to ensure it is available
// Usage: window.showToast(message, type = 'warning', duration = 5000)
window.showToast = window.showToast || function(message, type = 'warning', duration = 5000) {
    try {
        // simple sanitization
        if (message === null || message === undefined) message = '';
        const container = document.createElement('div');
        container.className = `toast-custom toast-${type}`;
        container.innerHTML = `<div class="toast-body"><div class="toast-text">${message}</div><button class="toast-close">×</button></div>`;
        document.body.appendChild(container);
        const btn = container.querySelector('.toast-close');
        if (btn) btn.addEventListener('click', () => { try { container.remove(); } catch (e) {} });
        // Keep a reference to the timeout so external code could clear it if needed
        const removeTimeout = setTimeout(() => { try { container.remove(); } catch (e) {} }, duration);
        // Provide a small accessible attribute and ensure it is visible for screen readers briefly
        container.setAttribute('role', 'status');
        container.setAttribute('aria-live', 'polite');
        // debug log
        try { console.log('showToast:', message, type, duration); } catch (e) {}
    } catch (e) {
        try { alert(message); } catch (e) {}
    }
};

// Persist a toast to sessionStorage so it can be shown after a full page reload.
window.setPendingToast = function(message, type = 'warning', duration = 5000, position = 'top-right') {
    try {
        const obj = { message: message ?? '', type: type, duration: duration, position: position };
        sessionStorage.setItem('pendingToast', JSON.stringify(obj));
    } catch (e) { }
};

// On load, check for a pending toast (from prior navigation) and show it once.
try {
    const json = sessionStorage.getItem('pendingToast');
    if (json) {
        try {
            const p = JSON.parse(json);
            // small delay to allow page to settle
            setTimeout(() => { try { window.showToast(p.message, p.type, p.duration, p.position); } catch (e) {} }, 150);
        } catch (e) { }
        sessionStorage.removeItem('pendingToast');
    }
} catch (e) { }
