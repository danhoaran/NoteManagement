$(function () {

    loadExistingNotes();

    $("#create-note-form").on('submit', function (e) {
        let successMessage = document.getElementById("note-success");
        successMessage.innerHTML = "";
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
                    successMessage.innerHTML = "Note added";
                    let container = document.getElementById("all-notes");
                    container.innerHTML = "";
                    loadExistingNotes();
                }
            });
    })
});

function SetupHtml(data) {
    let container = document.getElementById("all-notes");

    for (let x = 0; x < data.length; x++) {
        let categoriesDiv = document.createElement("div");
        let categoriesHtml = document.createElement("label");
        categoriesHtml.innerHTML = data[x].categories;
        categoriesDiv.appendChild(categoriesHtml);
        let bodyDiv = document.createElement("div");
        let bodyHtml = document.createElement("label");
        bodyHtml.innerHTML = data[x].body;
        bodyDiv.appendChild(bodyHtml);
        let dateCreatedDiv = document.createElement("div");
        let dateCreatedHtml = document.createElement("label");
        dateCreatedHtml.innerHTML = data[x].creationDate;
        dateCreatedDiv.appendChild(dateCreatedHtml);

        container.appendChild(categoriesDiv);
        container.appendChild(bodyDiv);
        container.appendChild(dateCreatedDiv);
    }
}


function loadExistingNotes() {
    $.ajax({
        type: "POST",
        url: '/Notes/LoadAllNotes/',
        contentType: false,
        processData: false,
        success: function (data) {
            if (data.length > 0) {
                SetupHtml(data);
            }
        },
        error: function () {
            alert("problem loading data");
        }
    });
}