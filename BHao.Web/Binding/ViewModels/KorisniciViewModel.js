(function (bh) {
    var KorisniciViewModel = function () {
        var self = this;
        self.viewModelHelper = new BHao.viewModelHelper();
        self.selectedKorisnik = ko.observable();
        self.korisnici = ko.observableArray();
        self.isLoaded = ko.observableArray(false);
        self.viewMode = ko.observable("lista");
        self.gradovi = ko.observableArray();
        self.uloge = ko.observableArray([]);
        self.odabraneUloge = ko.observableArray([]);
        self.filter = ko.observable("");
        self.dataMode = ko.observable("");

        self.Initialize = function () {
            self.viewModelHelper.apiGet('api/lookup/gradovi', null, function (result) {
                self.gradovi(result);
            });
            self.viewModelHelper.apiGet('api/administracija/uloge', null, function (result) {
                self.uloge(result);
            });
            self.GetData('klijenti');
            self.isLoaded(true);
        }

        self.GetData = function (mode) {
            self.dataMode(mode);
            self.viewModelHelper.apiGet('api/administracija/korisnici', { filter: self.filter(), ulogeFilter: self.dataMode() }, function (result) {
                ko.mapping.fromJS(result, {}, self.korisnici);
            });
        }

        self.Sacuvaj = function () {
            var errors = ko.validation.group(self.selectedKorisnik());
            self.viewModelHelper.modelErrors(errors());
            if (self.selectedKorisnik().isValid()) {
                
                self.ViewMode("lista");
               
                var unmappedKorisnik = ko.mapping.toJS(self.selectedKorisnik);
                if (self.selectedKorisnik().Id() > 0) {
                    self.viewModelHelper.apiPost("api/administracija/korisnici/azuriraj", unmappedKorisnik, function (successResult) {
                        self.GetData(self.dataMode());
                        toastr.success('Promjene uspješno sačuvane!', 'Korisnici');
                    });
                } else {

                    self.viewModelHelper.apiPost("api/administracija/korisnici/kreiraj", unmappedKorisnik, function (successResult) {
                        self.GetData(self.dataMode());
                    });
                }
            }
            else {
                for (var i = 0; i < self.viewModelHelper.modelErrors().length; i++) {
                        toastr.error(self.viewModelHelper.modelErrors()[i], 'Greška');
                }
                
            }
        }

        self.ViewMode = function (mode, data) {
            self.viewMode(mode);
            switch (mode) {
                case 'izmijeni':
                    self.selectedKorisnik(new BHao.KorisnikModel(data));
                    break;
                case 'dodaj':
                    self.selectedKorisnik(new BHao.KorisnikModel());
                    break;
                case 'lista':
                    break;
                default:
            }


        }

        self.Pretraga = function () {
            self.GetData(self.dataMode());
        }



        self.Initialize();
    }
    bh.KorisniciViewModel = KorisniciViewModel;
}(window.BHao));