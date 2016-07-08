(function (bh) {
    var AukcijeIndexViewModel = function () {

        var self = this;

        self.viewModelHelper = new BHao.viewModelHelper();
        self.aukcijeLista = ko.observableArray([]);

        self.odabranaKategorijaId = ko.observable(0);
        self.stringZaPretragu = ko.observable("");
        self.kategorije = ko.observableArray([]);
        self.isLoaded = ko.observable();
        self.brojAktivnihAukcija = ko.observable(0);
        

        self.Initialize = function () {
            self.GetAukcije();
            self.GetKategorije();
            self.isLoaded(true);
        }

        self.GetAukcije = function () {
            self.viewModelHelper.apiPost('api/aukcija/allaktivne', { KategorijaId: self.odabranaKategorijaId(), StringZaPretragu: self.stringZaPretragu() },
                function (result) {
                    if (result.length > 0) {
                        self.brojAktivnihAukcija(result.length);
                        ko.mapping.fromJS(result, {}, self.aukcijeLista);
                    } else {
                        self.viewModelHelper.apiPost('api/aukcija/zadnjeuspjesne', { KategorijaId: self.odabranaKategorijaId(), StringZaPretragu: self.stringZaPretragu() },
                            function (result) {
                                ko.mapping.fromJS(result, {}, self.aukcijeLista);
                            });
                    }
                    
                });

        }

        self.GetDetalji = function (data) {
            window.location.href = 'aukcija/' + data.Aukcija.Id() + '/detalji';
        }

        self.GetKategorije = function () {
            self.viewModelHelper.apiGet('api/lookup/kategorije', null,
                        function (result) {
                            ko.mapping.fromJS(result, {}, self.kategorije);
                        });
        }

        self.Initialize();
    }
    bh.AukcijeIndexViewModel = AukcijeIndexViewModel;
}(window.BHao));