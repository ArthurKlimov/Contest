$(document).ready(function () {

    $('#fullDescription').summernote({
        height: 300,
        toolbar: [
            ['style', ['bold', 'italic', 'underline', 'clear']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['fontname', ['Segoe UI']]
        ]
    });

    $('#cover').change(function () {
        var fileName = $(this).val().replace('C:\\fakepath\\', " ")
        $(this).next('.custom-file-label').html(fileName);
    })

    $("#endDate").change(function () {
        if ($(this).hasClass("is-invalid") && $(this).val() !== "") {
            $(this).removeClass("is-invalid");
        }
        else {
            $(this).addClass("is-invalid");
        }
    });

    $("#smallDescription").change(function () {
        if ($(this).hasClass("is-invalid") && $(this).val() !== "" && $(this).val().length <= 140) {
            $(this).removeClass("is-invalid");
        }
        else {
            $(this).addClass("is-invalid");
        }
    });

    $("#link").change(function () {
        if ($(this).hasClass("is-invalid") && $(this).val() !== "" && $(this).val().length <= 140) {
            $(this).removeClass("is-invalid");
        }
        else {
            $(this).addClass("is-invalid");
        }
    });

    $("#fullDescription").change(function () {
        if ($(this).hasClass("is-invalid") && $(this).val().length <= 2500) {
            $(this).removeClass("is-invalid");
        }
        else {
            $(this).addClass("is-invalid");
        }
    });

    $('#form').submit(function (e) {
        e.preventDefault();

        var isValid = true;
        var endDate = $('#endDate').val();
        var smallDescription = $('#smallDescription').val();
        var link = $('#link').val(); 
        var fullDescription = $('#fullDescription').val();

        if (endDate === "") {
            $("#endDate").addClass("is-invalid");
            isValid = false;
        }

        if (smallDescription === "" || smallDescription.length > 140) {
            $("#smallDescription").addClass("is-invalid");
            isValid = false;
        }
        if (link === "" || link.length > 140) {
            $("#link").addClass("is-invalid");
            isValid = false;
        }
        if (fullDescription === "" || fullDescription.length > 2500) {
            $("#fullDescription").addClass("is-invalid");
            isValid = false;
        }

        if (isValid === true) {
            var data = {
                endDate: endDate,
                smallDescription: smallDescription,
                link: link,
                fullDescription: fullDescription
            }

            $.ajax({
                type: $(this).attr('method'),
                contentType: "application/json;charset=utf-8",
                url: $(this).attr('action'),
                data: JSON.stringify(data),
                success: function () {
                    $('#endDate').val('');
                    $('#smallDescription').val('');
                    $('#link').val('');
                    $('#fullDescription').val('');
                    $('#successModal').modal('show');
                },
                error: function () {
                    $('#failModal').modal('show');
                }
            })
        }

    });


});