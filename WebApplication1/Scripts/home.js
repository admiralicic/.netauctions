/// <reference path="knockout-3.2.0.debug.js" />

$(function () {
    var viewModel = {
        firstName: ko.observable("John")
    };

    ko.applyBindings(viewModel);
})
