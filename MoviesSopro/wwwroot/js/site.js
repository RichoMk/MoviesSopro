function AddToWishlist(id) {
    $.ajax({
        type: "POST",
        url: "/Home/AddToWishlist/" + id,
        success: function (data) {
            //console.log(data);
            if (data.data != "") {
                new Noty({
                    type: 'alert',
                    layout: 'bottomLeft',
                    timeout: 3000,
                    text: 'Successfully Added To Watch later',
                    theme: 'sunset'
                }).show();
            } else {
                new Noty({
                    type: 'error',
                    layout: 'bottomLeft',
                    timeout: 3000,
                    progressBar: true,
                    text: 'Movie Already Exists In The Watch later',
                    theme: 'sunset'
                }).show();
            }
        },
        error: function () {
            alert("error");
        }
    });
};