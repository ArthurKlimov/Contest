$(document).ready(function () {
    var contestsBlock = $('#contests');
    if (contestsBlock) {

        var page = 1;

        var search = $("input[name='search']").val();
        var sort = 1;

        var sortString = $("input[name='sort']").val();
        if (sortString === "New") {
            sort = 2;
        }
        if (sortString === "AlmostClosed") {
            sort = 3;
        }

        $('#loadMoreButton').click(function () {
            page++;

            var data = {
                pageNumber: page,
                pageSize: 12,
                sort: sort,
                search: search
            };

            $.ajax({
                url: '/contests',
                type: 'GET',
                data: data,
                cache: false,
                contentType: false,
            })
                .done(function (response) {
                    contestsBlock.append(response);
                    //from a server model
                    if (!hasNextPage) {
                        $('#loadMoreButton').remove();
                    }
                })
                .fail(function () {
                })
        });
    }
});