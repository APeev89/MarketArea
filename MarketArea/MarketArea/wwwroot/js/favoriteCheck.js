window.onload = function () {

    var check_boxes = document.getElementsByClassName("favoriteCheck");
    
    for (var i = 0; i < check_boxes.length; i++) {

        check_boxes[i].addEventListener('click', checkedButtonFunc)

    }


    function checkedButtonFunc(event) {
        event.preventDefault();

        let checkBoxId = event.target.getAttribute("id");
        let getCheckBoxById = document.querySelector('.');
        let request;
        
        if (event.target.checked) {
            getCheckBoxById.checked = true;
            request = "post";
        }
        else {
            
            getCheckBoxById.checked = false;
            request = "delete";
        }
        $.ajax({
            type: request,
            url: "https://localhost:7134/Favorite/AddToFavorite",
            dataType: "jsonp",
            data: {
                id: checkBoxId,
                setFavourite: event.target.checked,

            },
            success: function (response) {
                console.log("true");
            }
        });

    }

}