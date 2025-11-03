// authHelpers.js

/**
 * Fetch and store the CSRF token from backend
 * Returns the token string
 */
 async function getCsrfToken() {
    try {
        const res = await fetch('/api/security/csrf-token', {
            credentials: 'include'  // include cookies
        });
        return getCookie('XSRF-TOKEN');
    } catch (err) {
        console.error("Failed to fetch CSRF token", err);
        return null;
    }
}

/**
 * Get a cookie value by name
 */
 function getCookie(name) {
    const match = document.cookie.match(new RegExp('(^| )' + name + '=([^;]+)'));
    return match ? match[2] : null;
}

/**
 * Send a fetch request with JSON body, CSRF, and credentials
 * @param {string} url
 * @param {object} options fetch options (method, body, headers)
 */
 async function secureFetch(url, options = {}) {
    const csrfToken = await getCsrfToken();

    const defaultHeaders = {
        'X-CSRF-TOKEN': csrfToken
    };

    const fetchOptions = {
        ...options,
        headers: {
            ...defaultHeaders,
            ...(options.headers || {})
        },
        credentials: 'include' // always include cookies
    };

    return fetch(url, fetchOptions);
}
