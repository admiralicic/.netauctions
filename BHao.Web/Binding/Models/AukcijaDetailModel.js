(function (bh) {
    var AukcijaDetailModel = function (data) {
        var self = this;
        self.Id = ko.observable(data.Id);
        self.Pocetak = ko.observable(data.Pocetak);
        self.Zavrsetak = ko.observable(data.Zavrsetak);
        self.NazivArtikla = ko.observable(data.NazivArtikla);
        self.ArtikalId = ko.observable(data.ArtikalId);
        self.Proizvodjac = ko.observable(data.Proizvodjac);
        self.Model = ko.observable(data.Model);
        self.PocetnaCijena = ko.observable(data.PocetnaCijena);
        self.KupiOdmahCijena = ko.observable(data.KupiOdmahCijena);
        self.NajvecaPonuda = ko.observable(data.NajvecaPonuda);
        self.NajveciPonudjacId = ko.observable(data.NajveciPonudjacId);
        self.NajveciPonudjac = ko.observable(data.NajveciPonudjac);
        self.DetaljanOpis = ko.observable(data.DetaljanOpis);
        self.ProdavacId = ko.observable(data.ProdavacId);
        self.Prodavac = ko.observable(data.Prodavac);
        self.NacinPlacanja = ko.observable(data.NacinPlacanja);
        self.Slike = ko.observableArray([]);
        self.Ponude = ko.observableArray(data.Ponude);
        self.Napomena = ko.observable(data.Napomena);
        self.Zavrsena = ko.observable(data.Zavrsena);
        self.OcjeneKorisnika = ko.observableArray(data.OcjeneKorisnika);
        self.OcjeneArtikla = ko.observableArray(data.OcjeneArtikla);
        self.KomentarKorisnika = ko.observable(data.KomentarKorisnika);
        self.KomentarArtikla = ko.observable(data.KomentarArtikla);
        self.KomentariArtikla = ko.observableArray(data.KomentariArtikla);
        self.Artikal = ko.observable(data.Artikal);
        self.isAdmin = ko.observable(data.isAdmin);
        self.NaslovAukcije = ko.computed(function () {
            return self.NazivArtikla() + ' ' + self.Proizvodjac() + ' ' + self.Model();
        });
        self.TrenutnaCijena = ko.computed(function () {
            return self.NajvecaPonuda() != null ? self.NajvecaPonuda().Iznos : self.PocetnaCijena();
        });

        self.Slike(ko.utils.arrayMap(data.Slike, function (item) {
            return { SlikaIme: item.SlikaIme, SlikaThumbIme: item.SlikaThumbIme };
        }));
    }
    bh.AukcijaDetailModel = AukcijaDetailModel;
}(window.BHao));