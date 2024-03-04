var interval;

function loading() {
    Notiflix.Loading.custom({
        customSvgUrl: 'https://notiflix.github.io/dir/icon.svg',
    });
}
function hideLoading() {
    Notiflix.Loading.remove();
}
jsFunctions.enableLoading = function () {
    loading();
}

jsFunctions.endInterval = function () {
    console.log("End Interval");
    $('#remaingSeconds').html(0);
    clearInterval(interval);
}