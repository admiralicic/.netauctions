(function (bh) {
    var PregledAukcijaViewModel = function () {
        var self = this;
        self.viewModelHelper = new BHao.viewModelHelper();
        self.isLoaded = ko.observable(false);
        self.aukcije = ko.observableArray([]);
        self.aukcijeSve = ko.observableArray([]);
        self.dataMode = ko.observable(false);
        self.brojAukcija = ko.observable();

        self.page = ko.observable(1);
        self.pageSize = ko.observable(6);
        self.narednaEnabled = ko.observable(true);
        self.prethodnaEnabled = ko.observable(false);

        self.pages = ko.computed(function () {
            return Math.ceil(self.brojAukcija() / self.pageSize());
        });

        self.Initialize = function () {
            self.GetData(false);
            self.isLoaded(true);
        }

        self.GetData = function (mode) {
            if (mode != self.dataMode()) {
                self.page(1);
            }
            self.dataMode(mode);
            self.viewModelHelper.apiPost("api/aukcija/all", { Zavrsena: mode, Page: self.page, PageSize: self.pageSize }, function (result) {
                ko.mapping.fromJS(result.aukcije, {}, self.aukcije);
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
            window.location.href = '/Aukcija/' + data.Aukcija.Id() + '/detalji';
        }

        self.UkloniAukciju = function (data) {
            if (confirm("Potvrda uklanjanja aukcije?")) {
                self.viewModelHelper.apiPost("api/aukcija/ukloni", { '': data.Aukcija.Id() }, function () {
                    self.aukcije.remove(data);
                    toastr.success('Aukcija uklonjena!', 'Admin');
                    
                });
            }

        }

        self.PrethodnaClick = function () {
            if (self.page() > 1) {
                self.page(self.page() - 1);
                self.GetData(self.dataMode());
            }
        }

        self.NarednaClick = function () {
            if (self.narednaEnabled() == true) {
                self.page(self.page() + 1);
                self.narednaEnabled(false); // onemogićava da prilikom 'double-click' na naredna, da preskoci dvije stranice
                self.GetData(self.dataMode());
            }

        }

        self.GotoPage = function (page) {
            self.page(page);
            self.GetData(self.dataMode());
        }

        self.Initialize();
    }
    bh.PregledAukcijaViewModel = PregledAukcijaViewModel;
}(window.BHao));