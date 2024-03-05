window.enableLoading = function (start) {
    if (start) {
        Notiflix.Loading.standard('Loading...', {
            backgroundColor: 'rgba(0,0,0,0.8)',
        });
    }
    else {
        Notiflix.Loading.remove();
    }
}
