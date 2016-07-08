(function (bh) {
    var StatistikaViewModel = function () {
        var self = this;
        self.viewModelHelper = new BHao.viewModelHelper();
        self.odabranaKategorijaId = ko.observable();
        self.prikazOd = ko.observable(null);
        self.prikazDo = ko.observable(null);
        self.statistika = ko.observable();
        self.kategorije = ko.observableArray([]);

        self.brojAukcija = ko.observable();
        self.prosjecnaVrijednostAukcija = ko.observable();
        self.najcesceProdavaniArtikli = ko.observableArray();
        self.listaAukcija = ko.observableArray();

        self.isLoaded = ko.observable(false);

        self.Initialize = function () {
            self.GetData();
            self.GetKategorije();
            self.isLoaded(true);
        }

        self.GetData = function () {
            if ((self.prikazOd() == null && self.prikazDo() != null) || (self.prikazOd() != null && self.prikazDo() == null)) {
                toastr.error('Provjerite datume, jedno polje nedostaje.', 'Greška');
                return;
            }

            if (self.prikazOd() > self.prikazDo()) {
                toastr.error('Provjerite datume, početni datum ne smije biti veći od krajnjeg.', 'Greška');
                return;
            }
            var dateOd = moment(self.prikazOd()).format("MM/DD/YYYY");
            var dateDo = moment(self.prikazDo()).format("MM/DD/YYYY");

            self.viewModelHelper.apiPost('api/statistika', { KategorijaId: self.odabranaKategorijaId, PrikazOd: dateOd, PrikazDo: dateDo },
                function (result) {
                    self.brojAukcija(result.BrojAukcija);
                    self.prosjecnaVrijednostAukcija(result.ProsjecnaVrijednostAukcija.toFixed(2));
                    self.najcesceProdavaniArtikli(result.NajcesceProdavaniArtikli);
                    self.listaAukcija(result.ListaAukcija);
                });
        }

        self.GetKategorije = function () {
            self.viewModelHelper.apiGet('api/lookup/kategorije', null,
                        function (result) {
                            ko.mapping.fromJS(result, {}, self.kategorije);
                        });
        }

        self.Initialize();
    }
    bh.StatistikaViewModel = StatistikaViewModel;
}(window.BHao));
