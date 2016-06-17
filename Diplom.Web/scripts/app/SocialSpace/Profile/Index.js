var active = $("#link_about");
jQuery(function () {

    jQuery(".mask").mask("00/00/0000");
    jQuery(".maskPhone").mask("(999) 999-9999");
    if ($("#User_Type").val() == "My") {
        var input = document.querySelector("input[type='file']");
        input.onchange = function () {
            this.form.submit();
        }
    }
    //$.ajax({
    //    url: '/SocialSpace/Profile/Friends',
    //    success: function (data) {
    //        $('#content_connected').html(data);
    //    }
    //});

});
function clickSubscription() {
    $("#content_subscriber").addClass("hidden");
    $("#content_connected").addClass("hidden");
    $("#content_subscription").removeClass("hidden");
    $("#link_subscripton").addClass("active");
    $("#link_connected").removeClass("active");
    $("#link_subscriber").removeClass("active");
}
function clickConnected() {
    $("#content_subscriber").addClass("hidden");
    $("#content_subscription").addClass("hidden");
    $("#content_connected").removeClass("hidden");
    $("#link_connected").addClass("active");
    $("#link_subscripton").removeClass("active");
    $("#link_subscriber").removeClass("active");
}
function clickSubscriber() {
    $("#content_subscriber").removeClass("hidden");
    $("#content_subscription").addClass("hidden");
    $("#content_connected").addClass("hidden");
    $("#link_connected").removeClass("active");
    $("#link_subscripton").removeClass("active");
    $("#link_subscriber").addClass("active");
}
function clickActive(id) {
    switch (active.attr("id")) {
        case "link_about":
            $("#content_about").addClass("hidden");
            break;
        case "link_connections":
            $("#content_connections").addClass("hidden");
            break;
        default: break;
    }

    active.removeClass("active");
    active = $("#" + id);
    active.addClass("active");
    switch (active.attr("id")) {
        case "link_about":
            $("#content_about").removeClass("hidden");
            break;
        case "link_connections":
            $("#content_connections").removeClass("hidden");
            break;
        default: break;
    }
}

function addFriend(id) {
    $.ajax({
        url: '/SocialSpace/Profile/AddToFriends?id=' + id,
        dataType: "json",
        success: function (result) {
            if (result == true) {
                swal("Good job!", "Friend request send", "success");
            } else {
                swal({
                    title: "Auto close alert!",
                    text: "Request has been sent yet",
                    timer: 2000,
                    showConfirmButton: false
                });
            }
        }
    });
}
function deleteFriend(id) {
    $.ajax({
        url: '/SocialSpace/Profile/DeleteFromFriends?id=' + id,
        dataType: "json",
        success: function (result) {
            if (result == true) {
                swal("Good job!", "Friend request reject", "success");
            } else {
                swal({
                    title: "Auto close alert!",
                    text: "Request rejected",
                    timer: 2000,
                    showConfirmButton: false
                });
            }
        }
    });
}
function friendsDelete(id) {
    $.ajax({
        url: '/SocialSpace/Profile/DeleteFromFriends?id=' + id,
        dataType: "json",
        success: function (result) {
            var footer = $("#" + id).find($(".c-footer"));
            footer.empty();
            footer.append("<button onclick=\"subscribersAdd('" + id + "')\" class=\"waves-effect\"><i class=\"zmdi zmdi-face-add\"></i> Add</button>");
            $("#" + id).detach().prependTo('#content_subscriber');
        }
    });
}
function subscribersAdd(id) {
    $.ajax({
        url: '/SocialSpace/Profile/AddToFriends?id=' + id,
        dataType: "json",
        success: function (result) {
            var footer = $("#" + id).find($(".c-footer"));
            footer.empty();
            footer.append("<button onclick=\"friendsDelete('" + id + "')\" class=\"waves-effect\"><i class=\"zmdi zmdi-face-add\"></i> Delete</button>");
            $("#" + id).detach().prependTo('#content_connected');
        }
    });
}
function subscriptionsReject(id) {
    $.ajax({
        url: '/SocialSpace/Profile/DeleteFromFriends?id=' + id,
        dataType: "json",
        success: function (result) {
            $("#" + id).remove();
        }
    });
}
function loadPhoto() {
    document.getElementById('files').click();
}