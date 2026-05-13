/**
 * Global application utility for handling AJAX requests and UI states.
 */
window.wms = {
    /**
     * Generic fetch wrapper with async/await and robust error handling.
     */
    async request(url, options = {}) {
        const defaultHeaders = {
            'X-Requested-With': 'XMLHttpRequest'
        };

        const config = {
            ...options,
            headers: {
                ...defaultHeaders,
                ...options.headers
            }
        };

        try {
            const response = await fetch(url, config);
            
            if (!response.ok) {
                // Try to get error message from response body
                let errorMessage = 'An unexpected error occurred';
                try {
                    const errorData = await response.json();
                    errorMessage = errorData.message || errorMessage;
                } catch {
                    const text = await response.text();
                    if (text && text.length < 200) errorMessage = text;
                }
                throw new Error(errorMessage);
            }

            // Check content type to decide how to parse
            const contentType = response.headers.get('content-type');
            if (contentType && contentType.includes('application/json')) {
                return await response.json();
            }
            return await response.text();
        } catch (error) {
            console.error(`[AJAX Error] ${url}:`, error);
            throw error;
        }
    },

    /**
     * UI helper to show a loader in a container.
     */
    showLoader(elementId, message = 'Loading details...') {
        const element = document.getElementById(elementId);
        if (!element) return;
        element.innerHTML = `
            <div class="flex flex-col items-center py-10">
                <span class="loading loading-spinner loading-lg text-primary"></span>
                <p class="mt-4 text-sm opacity-50">${message}</p>
            </div>
        `;
    },

    /**
     * UI helper to show an error message in a container.
     */
    showError(elementId, message = 'Error loading content. Please try again.') {
        const element = document.getElementById(elementId);
        if (!element) return;
        element.innerHTML = `
            <div class="alert alert-error shadow-lg">
                <svg xmlns="http://www.w3.org/2000/svg" class="stroke-current shrink-0 h-6 w-6" fill="none" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M10 14l2-2m0 0l2-2m-2 2l-2-2m2 2l2 2m7-2a9 9 0 11-18 0 9 9 0 0118 0z" />
                </svg>
                <span>${message}</span>
            </div>
        `;
    }
};
