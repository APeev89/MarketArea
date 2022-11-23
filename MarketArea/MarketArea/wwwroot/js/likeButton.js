window.onload = function () {
    var likebtn = document.getElementsByClassName("Like");

    for (var i = 0; i < likebtn.length; i++) {

        likebtn[i].addEventListener('click', likeButtonFunc)

    }
}

function likeButtonFunc(event) {

    let likeId = event.target.getAttribute("id");
    let v = "tx";
    $.ajax({
        type: "post",
        url: "https://localhost:7134/Like/LikeDislike",
        dataType: "json",
        data: {
            id: likeId
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