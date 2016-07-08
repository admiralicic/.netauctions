(function (bh) {
    var PorukeViewModel = function (korisnikId) {

        var self = this;
        self.viewModelHelper = new BHao.viewModelHelper();
        self.prijavljeniKorisnikId = ko.observable(korisnikId);
        self.poruke = ko.observableArray();
        self.viewMode = ko.observable("primljene");
        self.poruka = ko.observable();
        self.isLoaded = ko.observable(false);
        self.textOdgovora = ko.observable();
        self.brojNeprocitanihPoruka = ko.computed(function () {
            var neprocitane = ko.utils.arrayFilter(self.poruke(), function (item) {
                return item.PrimalacId() == self.prijavljeniKorisnikId() && item.Procitana() == false;
            });

            return neprocitane.length;
        });

        self.prethodniViewMode = "";

        self.primljenePoruke = ko.computed(function () {
            return ko.utils.arrayFilter(self.poruke(), function (item) {
                return item.PrimalacId() == self.prijavljeniKorisnikId();
            });
        });


        self.poslanePoruke = ko.computed(function () {
            return ko.utils.arrayFilter(self.poruke(), function (item) {
                return item.PosiljalacId() == self.prijavljeniKorisnikId();
            });
        });

        self.Initialize = function () {
            self.GetPoruke();
            self.viewMode("primljene");
            self.isLoaded(true);
        }


        self.GetPoruke = function () {
            self.viewModelHelper.apiPost('api/poruke', { '': korisnikId }, function (result) {
                ko.mapping.fromJS(result, {}, self.poruke);
            });

        }

        self.GetPoslane = function () {
            self.viewMode("poslane");
        }

        self.GetPrimljene = function () {
            self.viewMode("primljene");
        }

        self.GetDetalji = function (data) {
            self.poruka = new bh.PorukaModel(data);
            self.poruka.TextPoruke(self.poruka.TextPoruke().replace(/(?:\r\n|\r|\n)/g, '<br />'));
            self.prethodniViewMode = self.viewMode();

            if (self.poruka.PrimalacId() == self.prijavljeniKorisnikId()) {
                self.viewModelHelper.apiPost('api/poruke/oznaciProcitana', { '': self.poruka.PorukaId }, function () {

                    self.poruka.Procitana(true);
                    self.GetPoruke();
                });
            }

            self.viewMode("detalji");
        }

        self.PrikaziAukciju = function (data, event) {
            var e = event || window.event;
            e.stopPropagation();

            window.location.href = 'aukcija/' + data.AukcijaId() + '/detalji';
        }

        self.PrikaziListu = function (data) {
            self.viewMode(self.prethodniViewMode);
        }


        self.ObrisiPoruku = function (data, event) {

            var e = event || window.event;
            e.stopPropagation();


            if (confirm("Potvrda brisanja poruke?") == true) {
                if (data.PorukaId) {
                    self.poruka(new BHao.PorukaModel(data));
                }

                var unmappedPoruka = ko.mapping.toJS(self.poruka);

                self.viewModelHelper.apiPost('api/poruke/obrisi', unmappedPoruka, function () {
                    self.GetPoruke();
                    if (self.prethodniViewMode != "") {
                        self.viewMode(self.prethodniViewMode);
                    }
                    
                });
            }

        }

        self.Odgovori = function () {

            self.viewMode("odgovor");
            self.textOdgovora('\n\n\n' + '----------------------------------------' + '\n' +
                                self.poruka.PosiljalacUserName() + '\n' +
                                moment(self.poruka.Datum()).format('DD MMM YYYY, HH:mm:ss') + '\n\n' +
                                self.poruka.TextPoruke().replace(/(<br \/>)+/g, "\n"));
        }

        self.Posalji = function (data) {
            self.odgovor = new BHao.PorukaModel(data.poruka);
            self.odgovor.TextPoruke(self.textOdgovora());
            self.odgovor.PosiljalacId(data.poruka.PrimalacId());
            self.odgovor.PrimalacId(data.poruka.PosiljalacId());
            self.odgovor.Procitana(false);

            var unmappedOdgovor = ko.mapping.toJS(self.odgovor);

            self.viewModelHelper.apiPost('api/poruka/posalji', unmappedOdgovor, function () {
                self.GetPoruke();
                self.viewMode(self.prethodniViewMode);
            })
        }

        self.Odustani = function () {
            self.viewMode("detalji");
        }

        self.Initialize();
    }
    bh.PorukeViewModel = PorukeViewModel;
}(window.BHao))