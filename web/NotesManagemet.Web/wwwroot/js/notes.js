$(function () {

    loadExistingNotes();

    $("#create-note-form").on('submit', function (e) {
        e.preventDefault();
        let successMessage = document.getElementById("note-success");
        let errorMessage = document.getElementById("note-error");
        successMessage.innerHTML = "";
        errorMessage.innerHTML = "";

            let formData = $("#create-note-form");
            let serialized = formData.serialize();
            $.ajax(
                {
                    type: "POST",
                    data: serialized,
                    url: '/Notes/AddNewNote/',
                    contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                }).done(function (result) {
                    switch (result) {
                        case 1:
                            successMessage.innerHTML = "Note added";
                            let container = document.getElementById("all-notes");
                            container.innerHTML = "";
                            loadExistingNotes();
                            break;
                        case -1:
                            errorMessage.innerHTML = "Please select at least one category";
                            break;
                        case -2:
                            errorMessage.innerHTML = "Something has gone wrong. Help is one the way!"
                            break;
                        case -3:
                            errorMessage.innerHTML = "Problem submitting form";
                            break;                      
                        }
            });      
    })
});

function SetupHtml(data) {
    let container = document.getElementById("all-notes");

    for (let x = 0; x < data.length; x++) {
        let noteContainer = document.createElement("div");
        noteContainer.className = "container p-3 my-3 border";
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

        noteContainer.appendChild(categoriesDiv);
        noteContainer.appendChild(bodyDiv);
        noteContainer.appendChild(dateCreatedDiv);

        container.appendChild(noteContainer);
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