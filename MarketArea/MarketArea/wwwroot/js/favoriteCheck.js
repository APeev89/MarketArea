window.onload = function () {

    var check_boxes = document.getElementsByClassName("favoriteCheck");
    
    for (var i = 0; i < check_boxes.length; i++) {

        check_boxes[i].addEventListener('click', checkedButtonFunc)

    }

    function checkedButtonFunc(event) {
        
        let checkBoxId = event.target.getAttribute("id");
        let getCheckBoxById = document.getElementsByClassName("favoriteCheck");
        let request;
        let currentUrl;
        
        if (event.target.checked) {
            getCheckBoxById.checked = true;
            request = "post";
            currentUrl = "https://localhost:7134/Favorite/AddToFavorite";
        }else {
            
            getCheckBoxById.checked = false;
            request = "delete";
            currentUrl = "https://localhost:7134/Favorite/RemoveFromFavorite";
        }
        $.ajax({
            type: request,
            url: currentUrl,
            dataType: "json",
            data: {
                id: checkBoxId
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
    
}