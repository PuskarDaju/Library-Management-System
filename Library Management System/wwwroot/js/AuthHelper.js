// authHelpers.js

/**
 * Request the backend CSRF endpoint to refresh the CSRF cookie and return its value.
 * @returns {string|null} The `XSRF-TOKEN` cookie value if available, `null` otherwise.
 */
 async function getCsrfToken() {
    try {
        const res = await fetch('/api/security/csrf-token', {
            credentials: 'include'  // include cookies
        });
        return await getCookie('XSRF-TOKEN');
    } catch (err) {
        console.error("Failed to fetch CSRF token", err);
        return null;
    }
}

/**
 * Retrieve the value of a cookie by name.
 * @param {string} name - The cookie name to look up.
 * @returns {string|null} The cookie value if found, `null` otherwise.
 */
 function getCookie(name) {
    const match = document.cookie.match(new RegExp('(^| )' + name + '=([^;]+)'));
    return match ? match[2] : null;
}

/**
 * Send an HTTP request using fetch while adding an X-CSRF-TOKEN header and including credentials.
 *
 * Merges any user-supplied headers with an `X-CSRF-TOKEN` header obtained from getCsrfToken and forces `credentials: 'include'`.
 * @param {string} url - Request URL.
 * @param {object} [options] - Fetch options (e.g., method, body, headers); provided headers will be merged with the CSRF header.
 * @returns {Promise<Response>} The Response returned by fetch.
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