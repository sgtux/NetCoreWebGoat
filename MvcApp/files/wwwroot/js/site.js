// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function removePost(id) {
    if (confirm('Remove this post ?'))
        window.location = '/Post/Delete/' + id;
}

function logout() {
    window.location = '/Account/Logout';
}

function searchPosts() {
    const elem = document.getElementById('txtSearch')
    window.location = '/Post?search=' + elem.value
}

eval("function checkReadiness() { window.location = '/CheckReadiness?ip=' + document.getElementById('ip').value }")