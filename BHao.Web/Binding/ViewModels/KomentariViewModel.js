(function (bh) {
    var KomentariViewModel = function () {
        var self = this;

        self.isLoaded = ko.observable(false);
        self.viewModelHelper = new BHao.viewModelHelper();
        self.komentariKorisnika = ko.observableArray([]);
        self.komentariArtikala = ko.observableArray([]);
        self.viewMode = ko.observable("artikli");
        self.isPregledan = ko.observable(false);
        self.kategorije = ko.observableArray([{ Naziv: 'Nepregledani', Vrijednost: false }, { Naziv: 'Pregledani', Vrijednost: true }]);
        self.brojKomentaraArtikla = ko.observable(0);
        self.brojKomentaraKorisnika = ko.observable(0);

        self.pageArtikli = ko.observable(1);
        self.pageKorisnici = ko.observable(1);
        self.pageSize = ko.observable(10);
        self.narednaArtikliEnabled = ko.observable(true);
        self.narednaKorisniciEnabled = ko.observable(true);
        self.prethodnaArtikliEnabled = ko.observable(false);
        self.prethodnaKorisniciEnabled = ko.observable(false);

        self.pagesKorisnika = ko.computed(function () {
            return Math.ceil(self.brojKomentaraKorisnika() / self.pageSize());
        });

        self.pagesArtikla = ko.computed(function () {
            return Math.ceil(self.brojKomentaraArtikla() / self.pageSize());
        });

        self.Initialize = function () {
            self.GetData();
            self.isLoaded(true);
        }

        self.GetData = function () {
            self.viewModelHelper.apiGet("api/komentariKorisnika", { isPregledan: self.isPregledan, page: self.pageKorisnici, pageSize: self.pageSize }, function (result) {
                ko.mapping.fromJS(result.KomentariKorisnika, {}, self.komentariKorisnika);
                self.brojKomentaraKorisnika(result.BrojKomentara);
                if (self.pagesKorisnika() > self.pageKorisnici()) {
                    self.narednaKorisniciEnabled(true);
                } else {
                    self.narednaKorisniciEnabled(false);
                }

                if (self.pageKorisnici() == 1) {
                    self.prethodnaKorisniciEnabled(false);
                } else {
                    self.prethodnaKorisniciEnabled(true);
                }
            });

            self.viewModelHelper.apiGet("api/komentariArtikala", { isPregledan: self.isPregledan, page: self.pageArtikli, pageSize: self.pageSize }, function (result) {
                ko.mapping.fromJS(result.KomentariArtikla, {}, self.komentariArtikala);
                self.brojKomentaraArtikla(result.BrojKomentara);

                if (self.pagesArtikla() > self.pageArtikli()) {
                    self.narednaArtikliEnabled(true);
                } else {
                    self.narednaArtikliEnabled(false);
                }

                if (self.pageArtikli() == 1) {
                    self.prethodnaArtikliEnabled(false);
                } else {
                    self.prethodnaArtikliEnabled(true);
                }
            });
        }


        self.SetViewMode = function (mode) {
            self.viewMode(mode);
        }

        self.PrikaziAukciju = function (data) {
            window.location.href = '/aukcija/' + data.AukcijaId() + '/detalji';
        }

        self.KomentarKorisnikaPregledan = function (data) {
            self.viewModelHelper.apiGet("api/komentariKorisnika/pregledan", { komentarId: data.Id() }, function () {
                toastr.success('Komentar pregledan.', 'Komentari');
                self.komentariKorisnika.remove(data);
            });
        }

        self.UkloniKomentarKorisnika = function (data) {
            self.viewModelHelper.apiGet("api/komentariKorisnika/ukloni", { komentarId: data.Id() }, function () {
                toastr.success('Komentar uspješno uklonjen!', 'Komentari');
                self.komentariKorisnika.remove(data);
            });
        }

        self.KomentarArtiklaPregledan = function (data) {
            self.viewModelHelper.apiGet("api/komentariArtikala/pregledan", { komentarId: data.Id() }, function () {
                toastr.success('Komentar pregledan.', 'Komentari');
                self.komentariArtikala.remove(data);
            });
        }

        self.UkloniKomentarArtikla = function (data) {
            self.viewModelHelper.apiGet("api/komentariArtikala/ukloni", { komentarId: data.Id() }, function () {
                toastr.success('Komentar uspješno uklonjen!', 'Komentari');
                self.komentariArtikala.remove(data);
            });
        }

        self.PrethodnaKorisniciClick = function () {
            if (self.pageKorisnici() > 1) {
                self.pageKorisnici(self.pageKorisnici() - 1);
                self.GetData();
            }
        }

        self.PrethodnaArtikliClick = function () {
            if (self.pageArtikli() > 1) {
                self.pageArtikli(self.pageArtikli() + 1);
                self.GetData();
            }
        }

        self.NarednaKorisniciClick = function () {
            if (self.narednaKorisniciEnabled() == true) {
                if (self.pageKorisnici() < self.pagesKorisnika()) {
                    self.pageKorisnici(self.pageKorisnici() + 1);

                    self.GetData();
                }
            }

        }

        self.NarednaArtikliClick = function () {
            if (self.narednaArtikliEnabled() == true) {
                if (self.pageArtikli() < self.pagesArtikla()) {
                    self.pageArtikli(self.pageArtikli() + 1);

                    self.GetData();
                }
            }

        }


        self.GotoPage = function (page) {
            if (self.viewMode() == 'korisnici') {
                self.pageKorisnici(page);
            }

            if (self.viewMode() == 'artikli') {
                self.pageArtikli(page);
            }
           
            self.GetData();
        }

        self.PregledaniNepregledaniSwitch = function () {
            if (self.viewMode() == 'korisnici') {
                self.pageKorisnici(1);
            }

            if (self.viewMode() =='artikli') {
                self.pageArtikli(1);
            }

            self.GetData();
        }


        self.Initialize();
    }
    bh.KomentariViewModel = KomentariViewModel;
}(window.BHao));