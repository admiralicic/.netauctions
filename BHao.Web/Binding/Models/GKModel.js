(function (bh) {
    var GKModel = function () {
        var self = this;

        self.Id = ko.observable();
        self.Naziv = ko.observable().extend({
            required: { message: "Naziv je obavezan!" }
        }
        );
    }
    bh.GKModel = GKModel;
}(window.BHao));