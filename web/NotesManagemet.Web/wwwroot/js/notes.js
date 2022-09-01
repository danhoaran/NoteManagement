$(function () {
    $("#create-note-form").on('submit', function (e) {
        e.preventDefault();
        let formData = $("#create-note-form");
        let serialized = formData.serialize();
        $.ajax(
            {
                type: "POST",
                data: serialized,
                url: '/Notes/AddNewNote/',
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            }).done(function (result) {
                if (result) {
                    console.log("note added");
                }
            });
    })
});