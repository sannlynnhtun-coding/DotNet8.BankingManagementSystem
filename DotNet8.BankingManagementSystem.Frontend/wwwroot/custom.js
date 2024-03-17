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

// function confirmMessage(message) {
//     return new Promise((resolve, reject) => {
//         Swal.fire({
//             title: "Confirm",
//             text: message,
//             icon: "warning",
//             showCancelButton: true,
//         }).then((result) => {
//             // return result.isConfirmed;
//             resolve(result.isConfirmed)
//         });
//     });
// }
window.isConfirmed = function (stateCode, dotNetReference) {
    Notiflix.Confirm.show(
        'Do you want to delete?',
        'Are you sure?',
        'Yes',
        'No',
        async function okCb() {
            // await dotNetReference.invokeMethodAsync('Delete', stateCode);
        },
        function cancelCb() {
            return;
        },
        {},
    );
}

// window.isConfirmed = function (stateCode,dotNetReference) {
//     Notiflix.Confirm.show(
//         'Do you want to delete?',
//         'Are you sure?',
//         'Yes',
//         'No',
//         function okCb() {
//             await dotNetReference.invokeMethodAsync('DeleteState', stateCode);
//         },
//         function cancelCb() {
//             return;
//         },
//         {},
//     );
// }