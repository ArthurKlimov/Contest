$(document).ready(function () {
    $('#fullDescription').summernote({
        height: 300,
        toolbar: [
            ['style', ['bold', 'italic', 'underline', 'clear']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['fontname', ['Segoe UI']]
        ]
    });

    $('#coverImage').change(function () {
        var fileName = $(this).val().replace('C:\\fakepath\\', " ")
        $(this).next('.custom-file-label').html(fileName);
    })

    $('#form').submit(function (e) {
        e.preventDefault();

        var endDateInput = $("input[name='endDate']");
        var smallDescriptionInput = $("input[name='smallDescription']");
        var linkInput = $("input[name='link']");
        var fullDescriptionInput = $("textarea[name='fullDescription']");
        var coverImageInput = $("input[name='coverImage']");

        var isFormValid = true;

        if (endDateInput.val() === "") {
            isFormValid = false;
            endDateInput.addClass('is-invalid');
        }
        if (smallDescriptionInput.val() === "" || smallDescriptionInput.val().length > 140) {
            isFormValid = false;
            smallDescriptionInput.addClass('is-invalid');
        }
        if (linkInput.val() === "" || smallDescriptionInput.val().length > 140) {
            isFormValid = false;
            linkInput.addClass('is-invalid');
        }
        if (fullDescriptionInput.val().length > 2500) {
            isFormValid = false;
            $('.note-editor').addClass('is-invalid');
        }
        if (coverImageInput[0].files[0] && coverImageInput[0].files[0].size > 5242880) {
            isFormValid = false;
            coverImageInput.addClass('is-invalid');
        }

        if (isFormValid) {

            var formData = new FormData();
            formData.append("endDate", endDateInput.val());
            formData.append("smallDescription", smallDescriptionInput.val());
            formData.append("link", linkInput.val());
            formData.append("fullDescription", fullDescriptionInput.val());
            formData.append("coverImage", coverImageInput[0].files[0]);

            $.ajax({
                url: '/addContest',
                type: 'POST',
                data: formData,
                cache: false,
                contentType: false,
                processData: false,
            })
                .done(function () {
                    $('#successModal').modal();
                 })
                .fail(function () {
                    $('#failModal').modal();
                })
        }

        debugger
    });
});