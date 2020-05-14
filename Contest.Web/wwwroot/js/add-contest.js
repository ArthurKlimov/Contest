$(document).ready(function () {
    $('#cover').on('change', function () {
        var fileName = $(this).val().replace('C:\\fakepath\\', " ")
        $(this).next('.custom-file-label').html(fileName);
    })

    $('#fullDescription').summernote({
        height: 300,
        toolbar: [
            ['style', ['bold', 'italic', 'underline', 'clear']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['fontname', ['Segoe UI']]
        ]
    });



    $('#addContestButton').click(function () {

        var startDate = $('#startDate').val();

        var endDate = $('#endDate').val();

        var smallDescription = $('#smallDescription').val();
        var link = $('#link').val(); 
        var fullDescription = $('#fullDescription').val();

        var data = {
            startDate: startDate,
            endDate: endDate,
            smallDescription: smallDescription,
            link: link,
            fullDescription: fullDescription
        }

        $.ajax({
            type: "POST",
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            url: "/api/contest/add",
            data: JSON.stringify(data),
            success: function (response) {
                console.log(response);
            },
            error: function (response) {
                console.log(response);
            }
        })
    });
});