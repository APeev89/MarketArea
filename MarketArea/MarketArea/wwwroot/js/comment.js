$(document).ready(function () {

    var btnSend = document.getElementById("btnSend");

    btnSend.addEventListener("click", sendButtonFunc)
});

//window.onload = function () {



//}

function sendButtonFunc(event) {

    var sendId = event.target.getAttribute("name");
    var sendText = document.getElementById("textAreaExample").value;

    $.ajax({
        type: "post",
        url: "https://localhost:7134/Comment/Add",
        dataType: "json",
        data: {
            id: sendId,
            text: sendText

        },
        success: function () {
            window.location.reload();
        },
        error: function (request, status, error) {
            console.log(request.responseText);
            console.log(error);
        },
    });
}