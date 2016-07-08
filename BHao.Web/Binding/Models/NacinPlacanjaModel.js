(function (bh) {
    var NacinPlacanjaModel = function () {
        var self = this;

        self.Id = ko.observable();
        self.Opis = ko.observable().extend({
            required: {message: "Opis je obavezan!"}
        });
    }
    bh.NacinPlacanjaModel = NacinPlacanjaModel;
}(window.BHao));