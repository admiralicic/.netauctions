(function (bh) {
    var MojeAukcijeViewModel = function (korisnikId) {
        var self = this;

        self.korisnikId = ko.observable(korisnikId);
        self.viewModelHelper = new BHao.viewModelHelper();
        self.brojAukcija = ko.observable(0);
        self.isPorukaMode = ko.observable(0);
        self.aukcijeKorisnika = ko.observableArray();
        self.isLoaded = ko.observable(false);
        self.textPoruke = ko.observable();
        self.dataMode = ko.observable("prodavac");
        
        self.page = ko.observable(1);
        self.pageSize = ko.observable(6);
        self.narednaEnabled = ko.observable(true);
        self.prethodnaEnabled = ko.observable(false);

        self.pages = ko.computed(function () {
            return Math.ceil(self.brojAukcija() / self.pageSize());
        });

        self.Initialize = function () {
            self.GetAukcije("prodavac");
            self.isLoaded(true);
        }

        self.GetAukcije = function (kriterij) {
            if (kriterij != self.dataMode()) {
                self.page(1);
            }
            self.dataMode(kriterij);
            self.viewModelHelper.apiPost('api/aukcija/getzakorisnika', { KorisnikId: korisnikId, Kriterij: kriterij, Page: self.page, PageSize: self.pageSize }, function (result) {
                ko.mapping.fromJS(result.aukcije, {}, self.aukcijeKorisnika);
                self.brojAukcija(result.brojAukcija);

                if (self.pages() > self.page()) {
                    self.narednaEnabled(true);
                } else {
                    self.narednaEnabled(false);
                }

                if (self.page() == 1) {
                    self.prethodnaEnabled(false);
                } else {
                    self.prethodnaEnabled(true);
                }
            });
            
        }

        self.GetDetalji = function (data) {
            window.location.href = data.Aukcija.Id() + '/detalji';
        }

        self.TogglePorukaMode = function (data) {
            if (self.isPorukaMode() == 0) {
                self.isPorukaMode(data.Aukcija.Id())
            } else {
                self.isPorukaMode(0);
            }
        }

        self.PosaljiPoruku = function (data) {
            if (!self.textPoruke()) {
                self.viewModelHelper.modelIsValid(false);
                self.viewModelHelper.modelErrors.push("Ne možete poslati praznu poruku!");
            } else {
                self.viewModelHelper.posaljiPoruku(self.korisnikId, data.Kupac.Id(), data.Aukcija.Id(), self.textPoruke, function () {
                    self.TogglePorukaMode(data);
                    self.textPoruke("");
                    toastr.success('Poruka poslana!', 'Poruka');
                });
            }
            
        }

        self.PrethodnaClick = function () {
            if (self.page() > 1) {
                self.page(self.page() - 1);
                self.GetAukcije(self.dataMode());
            }
        }

        self.NarednaClick = function () {
            if (self.narednaEnabled() == true) {
                self.page(self.page() + 1);
                self.narednaEnabled(false); 
                self.GetAukcije(self.dataMode());
            }

        }

        self.GotoPage = function (page) {
            self.page(page);
            self.GetAukcije(self.dataMode());
        }

        self.Initialize();
    }
    bh.MojeAukcijeViewModel = MojeAukcijeViewModel;
}(window.BHao))