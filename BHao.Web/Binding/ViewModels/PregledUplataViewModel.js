(function (bh) {
    var PregledUplataViewModel = function () {
        var self = this;
        self.viewModelHelper = new BHao.viewModelHelper();
        self.isLoaded = ko.observable(false);
        self.aukcije = ko.observableArray();
        self.editMode = ko.observable(false);
        self.editRow = ko.observable(0);
        self.selected = ko.observable();
        self.dataMode = ko.observable("neplacene");
        self.stringZaPretragu = ko.observable("");
       

        self.page = ko.observable(1);
        self.pageSize = ko.observable(15);
        self.narednaEnabled = ko.observable(true);
        self.prethodnaEnabled = ko.observable(false);
        self.brojAukcija = ko.observable();

        self.pages = ko.computed(function () {
            return Math.ceil(self.brojAukcija() / self.pageSize());
        });
        
        self.zavrsetak = ko.computed(function () {
            return self.selected() ? self.selected().Aukcija.Zavrsetak() : null;
        });
        ko.validation.registerExtenders();
        self.unos = ko.observable().extend({
            required: { message: 'Morate unijeti datum!'},
            dateLessThan: moment(),
            dateGreaterThan: self.zavrsetak
        });

        self.Initialize = function () {

            self.GetData(self.dataMode());
            self.isLoaded(true);
        }

        self.Pretraga = function () {
            self.page(1);
            self.GetData(self.dataMode());
        }

        self.GetData = function (filter) {
            if (self.dataMode() != filter) {
                self.page(1);
            }
            self.dataMode(filter);
            self.viewModelHelper.apiGet('api/aukcija/zavrsene', { filterPlacanja : filter, stringZaPretragu: self.stringZaPretragu, page: self.page, pageSize: self.pageSize }, function (result) {
                ko.mapping.fromJS(result.aukcije, {}, self.aukcije);
                self.brojAukcija(result.brojAukcija);

                if (self.pages() >= self.page()) {
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
        self.EditMode = function (mode, data) {

            switch (mode) {
                case 'izmijeni':
                    self.editRow(data.Aukcija.Id());
                    self.editMode(true);
                    self.selected(data);
                    if (self.selected().Aukcija.DatumPlacanja()) {
                        self.unos(moment(self.selected().Aukcija.DatumPlacanja()).format("DD-MMM-YYYY"));
                    } else {
                        self.unos("");
                    }
                    break;

                case 'sacuvaj':
                    var errors = ko.validation.group(self);
                    self.viewModelHelper.modelErrors(errors());
                    if (self.isValid()) {

                        var datumPlacanja = moment(self.unos()).format();
                        self.editMode(false);
                        self.viewModelHelper.apiPost('api/aukcija/evidentirajplacanje', { AukcijaId: self.selected().Aukcija.Id(), DatumPlacanja: datumPlacanja }, function (result) {
                            toastr.success('Plaćanje uspješno evidentirano', 'Evidencija uplata');
                            self.selected().Aukcija.DatumPlacanja(datumPlacanja);

                        });

                    } else {
                        for (var i = 0; i < self.viewModelHelper.modelErrors().length; i++) {
                            toastr.error(self.viewModelHelper.modelErrors()[i],'Greška');
                        }

                    }
                    break;

                case 'otkazi':
                    toastr.info('Otkazano', 'Evidencija uplata');
                    self.editMode(false);
                    break;
                default:

            }

        }

        self.GotoPage = function (page) {
            self.page(page);
            self.GetData(self.dataMode());
        }

        self.Initialize();
    }
    bh.PregledUplataViewModel = PregledUplataViewModel;
}(window.BHao))