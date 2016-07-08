(function (bh) {
    var InkrementModel = function () {
        var self = this;

        self.Id = ko.observable();
        self.Cijena = ko.observable().extend({
            required: { message: "Polje 'cijena' je obavezno!" },
            number: { message: "Polje 'cijena' mora biti broj" }
        });
        self.IznosInkrementa = ko.observable().extend({
            required: { message: "Polje 'iznos inkrementa' je obavezno!" },
            number: { message: "Polje 'iznos inkrementa' mora biti broj!" }
        });;

    }
    bh.InkrementModel = InkrementModel;
}(window.BHao));