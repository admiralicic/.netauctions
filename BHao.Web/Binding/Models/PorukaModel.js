(function (bh) {
    var PorukaModel = function (data) {

        var self = this;
        
        self.PorukaId = ko.observable(data.PorukaId());
        self.Datum = ko.observable(data.Datum());
        self.TextPoruke = ko.observable(data.TextPoruke());
        self.Procitana = ko.observable(data.Procitana());
        self.PosiljalacId = ko.observable(data.PosiljalacId());
        self.PosiljalacUserName = ko.observable(data.PosiljalacUserName());
        self.PrimalacId = ko.observable(data.PrimalacId());
        self.PrimalacUserName = ko.observable(data.PrimalacUserName());
        self.AukcijaId = ko.observable(data.AukcijaId());
        self.AukcijaPredmet = ko.observable(data.AukcijaPredmet());

    }
    bh.PorukaModel = PorukaModel;
}(window.BHao));