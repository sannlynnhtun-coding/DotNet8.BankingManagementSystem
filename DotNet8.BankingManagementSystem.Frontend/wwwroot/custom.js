(() => {
    if (typeof Notiflix === 'undefined') return;

    const getCssVar = (name, fallback) => {
        try {
            const value = getComputedStyle(document.documentElement).getPropertyValue(name);
            return (value || '').trim() || fallback;
        } catch {
            return fallback;
        }
    };

    const normalizeHex = (value, fallback) => {
        const v = (value || '').trim();
        if (/^#[0-9a-fA-F]{6}$/.test(v)) return v.toLowerCase();
        if (/^#[0-9a-fA-F]{3}$/.test(v)) {
            const r = v[1], g = v[2], b = v[3];
            return `#${r}${r}${g}${g}${b}${b}`.toLowerCase();
        }
        return fallback;
    };

    const hexToRgb = (hex) => {
        const h = hex.replace('#', '');
        const n = parseInt(h, 16);
        return {
            r: (n >> 16) & 255,
            g: (n >> 8) & 255,
            b: n & 255
        };
    };

    const rgba = (hex, alpha) => {
        const { r, g, b } = hexToRgb(hex);
        return `rgba(${r}, ${g}, ${b}, ${alpha})`;
    };

    const primary = normalizeHex(getCssVar('--color-primary-600', '#9333EA'), '#9333EA');

    Notiflix.Loading.init({
        backgroundColor: rgba(primary, 0.15),
        svgColor: primary,
        messageColor: '#0f172a',
        fontFamily: 'Inter, ui-sans-serif, system-ui, sans-serif'
    });

    Notiflix.Confirm.init({
        backgroundColor: '#ffffff',
        titleColor: '#0f172a',
        messageColor: '#475569',
        borderRadius: '16px',
        okButtonBackground: primary,
        okButtonColor: '#ffffff',
        cancelButtonBackground: '#ffffff',
        cancelButtonColor: '#334155',
        backOverlayColor: 'rgba(15, 23, 42, 0.35)',
        fontFamily: 'Inter, ui-sans-serif, system-ui, sans-serif'
    });

    Notiflix.Notify.init({
        borderRadius: '12px',
        fontFamily: 'Inter, ui-sans-serif, system-ui, sans-serif',
        success: {
            background: primary,
            textColor: '#ffffff',
            notiflixIconColor: '#ffffff'
        },
        info: {
            background: primary,
            textColor: '#ffffff',
            notiflixIconColor: '#ffffff'
        },
        failure: {
            background: '#e11d48',
            textColor: '#ffffff',
            notiflixIconColor: '#ffffff'
        }
    });
})();

window.enableLoading = function (start) {
    if (start) {
        Notiflix.Loading.dots('Loading...');
    } else {
        Notiflix.Loading.remove();
    }
}

window.intervalLoading = function (start) {
    if (start) {
        Notiflix.Loading.dots('Loading...');
        setTimeout(function () {
            Notiflix.Loading.remove();
        }, 1000);
    }
}

window.errorMessage = function (message) {
    Notiflix.Notify.failure(message);
}

window.successMessage = function (message) {
    Notiflix.Notify.success(message);
}

window.confirmMessage = function (message) {
    return new Promise((resolve) => {
        Notiflix.Confirm.show(
            'Confirmation',
            message,
            'Yes',
            'No',
            () => resolve(true),
            () => resolve(false),
            {}
        );
    });
}
