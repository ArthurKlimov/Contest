﻿var IndexApp = new Vue({
    el: '#IndexApp',
    data: {
        hasContests: false,
        hasNextPage: true,
        search: "",
        sort: "Popular",
        city: "",
        contests: []
    },
    created: function () {
        this.getContests();
    },
    methods: {

        getContests() {
            var request = $.ajax({
                url: "/contests/all",
                method: "GET",
                data: {
                    pageNumber: 1,
                    pageSize: 15,
                    isPublished: true,
                    search: this.search,
                    sort: this.sort,
                    city: this.city
                },
                dataType: "json",
                contentType: "application/json"
            });

            var self = this;

            request.done(function (response) {

                if (self.contests.length === 0)
                    self.contests = response.items;
                else
                    self.contests = self.contests.concat(response.items);

                self.hasNextPage = response.hasNextPage;

                if (response.hasNextPage)
                    self.pageNumber++

                if (response.items.length !== 0)
                    self.hasContests = true;
            });
        },

        filterContests() {
            this.contests.length = 0;
            this.pageNumber = 1;
            this.hasContests = false;
            this.getContests();
        },

        sortContests(sort) {
            this.contests.length = 0;
            this.pageNumber = 1;
            this.hasContests = false;
            if (sort) {
                this.sort = sort;
            }
            this.getContests();
        }
    }
});

$("#city").suggestions({
    token: "8883f56da5a81a127cba6f4102dbcd72d9ba9078",
    type: "ADDRESS",
    hint: false,
    bounds: "city",
    constraints: {
        locations: { city_type_full: "город" }
    },
    onSelectNothing: function () {
        $(this).val("");
    },
    onSelect: function (suggestion) {
        $(this).val(suggestion.data.city);
        IndexApp._data.city = suggestion.data.city;
    }
});

$("#searchButton").click(function () {
    $('html,body').animate({ scrollTop: $("#searchBlock").offset().top - 110 }, 'slow');
});