function showLoading() {
    $('#loading-wrapper').show();
}

function hideLoading() {
    $('#loading-wrapper').hide();
}

function displayErrorMessage() {
    displayErrorAlert("Đã có lỗi xảy ra!", 2000);
}

function displayErrorAlert(message, timer = 2000) {
    Swal.fire({
        icon: 'error',
        title: message,
        showConfirmButton: false,
        timer: timer
    })
};

function displaySuccessAlert(message, timer = 2000) {
    Swal.fire({
        icon: 'success',
        title: message,
        showConfirmButton: false,
        timer: timer
    })
};

