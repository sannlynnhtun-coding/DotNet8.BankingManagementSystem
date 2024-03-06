window.enableLoading = function (start) {
    if (start) {
        Notiflix.Loading.dots('Loading...', {
            backgroundColor: 'rgba(207, 193, 237, 0.5)',
        });
    } else {
        Notiflix.Loading.remove();
    }
}

window.intervalLoading = function (start) {
    if (start) {
        Notiflix.Loading.dots('Loading...', {
            backgroundColor: 'rgba(207, 193, 237, 0.5)',
        });
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