(function (bh) {
    var AdministracijaViewModel = function () {

        var self = this;

        self.viewModelHelper = new BHao.viewModelHelper();
        self.isLoaded = ko.observable(false);
        self.dataMode = ko.observable("grad");
        self.editMode = ko.observable(false);
        self.editRow = ko.observable(0);
        self.podaci = ko.observableArray([]);
        self.podaciInkrementi = ko.observableArray([]);
        self.podaciNaciniPlacanja = ko.observableArray([]);
        self.dodavanjeOmoguceno = ko.observable(false);
        self.gkNaziv = ko.observable();
        self.inkrementCijena = ko.observable();
        self.inkrementIznos = ko.observable();
        self.nacinPlacanjaOpis = ko.observable();

        self.Initialize = function () {


            self.dataMode("grad");
            self.GetData("grad");
            self.isLoaded(true);
        }

        self.selected = ko.observable();

        self.EditMode = function (mode, data) {
            if (mode == 'izmijeni') {
                self.editMode(true);
                self.editRow(data.Id);
                self.selected(data);
            } else if (mode == 'obrisi') {
                self.selected(data);
                self.Obrisi(self.selected());
            } else if (mode == 'sacuvaj') {
                self.Sacuvaj(self.selected());
            }
            else {
                self.editMode(false);
                self.editRow(0);
                self.viewModelHelper.modelErrors([]);
                self.viewModelHelper.modelIsValid(true);
            }
        }

        self.GetData = function (mode) {
            var url = "api/lookup/gradovi";
            switch (mode) {
                case "grad":
                    url = "api/lookup/gradovi";
                    self.viewModelHelper.apiGet(url, null, function (result) {
                        self.podaci.removeAll();
                        ko.mapping.fromJS(result, {}, self.podaci);
                        self.dataMode(mode);
                    });
                    break;
                case "inkrement":
                    url = "api/lookup/inkrementi";
                    self.viewModelHelper.apiGet(url, null, function (result) {
                        self.podaci.removeAll();
                        ko.mapping.fromJS(result, {}, self.podaciInkrementi);
                        self.dataMode(mode);
                    });
                    break;
                case "kategorija":
                    url = "api/lookup/kategorije"
                    self.viewModelHelper.apiGet(url, null, function (result) {
                        self.podaci.removeAll();
                        ko.mapping.fromJS(result, {}, self.podaci);
                        self.dataMode(mode);
                    });
                    break;
                case "nacinplacanja":
                    url = "api/lookup/naciniplacanja"
                    self.viewModelHelper.apiGet(url, null, function (result) {
                        self.podaci.removeAll();
                        ko.mapping.fromJS(result, {}, self.podaciNaciniPlacanja);
                        self.dataMode(mode);
                    });
                    break;
                default:
            }
        }

        self.OmoguciDodavanje = function () {
            self.dodavanjeOmoguceno(true);
        }

        self.OnemoguciDodavanje = function () {
            self.dodavanjeOmoguceno(false);
            self.viewModelHelper.modelIsValid(true);
        }

        self.Sacuvaj = function (data) {
            // debugger;
            var errors;
            var gk = new BHao.GKModel();
            var inkrement = new BHao.InkrementModel();
            var nacinplacanja = new BHao.NacinPlacanjaModel();

            if (self.dataMode() == 'grad' || self.dataMode() == 'kategorija') {

                gk.Naziv(self.gkNaziv());
                if (data.Id) {
                    gk.Id(data.Id());
                    gk.Naziv(data.Naziv());
                }
                errors = ko.validation.group(gk);
                self.viewModelHelper.modelIsValid(gk.isValid());
            }

            if (self.dataMode() == 'inkrement') {

                inkrement.Cijena(self.inkrementCijena());
                inkrement.IznosInkrementa(self.inkrementIznos());
                if (data.Id) {
                    inkrement.Id(data.Id());
                    inkrement.Cijena(data.Cijena());
                    inkrement.IznosInkrementa(data.IznosInkrementa());
                }
                errors = ko.validation.group(inkrement);
                self.viewModelHelper.modelIsValid(inkrement.isValid());
            }

            if (self.dataMode() == 'nacinplacanja') {

                nacinplacanja.Opis(self.nacinPlacanjaOpis());
                if (data.Id) {
                    nacinplacanja.Id(data.Id());
                    nacinplacanja.Opis(data.Opis());
                }
                errors = ko.validation.group(nacinplacanja);
                self.viewModelHelper.modelIsValid(nacinplacanja.isValid());

            }

            if (errors().length < 1) {
                var url = "";
                var data = null;

                switch (self.dataMode()) {
                    case 'grad':
                        url = "api/postavke/grad/sacuvaj"
                        data = ko.mapping.toJS(gk);
                        break;
                    case 'kategorija':
                        url = "api/postavke/kategorija/sacuvaj"
                        data = ko.mapping.toJS(gk);
                        break;
                    case 'inkrement':
                        url = "api/postavke/inkrement/sacuvaj"
                        data = ko.mapping.toJS(inkrement);
                        break;
                    case 'nacinplacanja':
                        url = "api/postavke/nacinplacanja/sacuvaj"
                        data = ko.mapping.toJS(nacinplacanja);
                        break;
                    default:
                }

                self.viewModelHelper.apiPost(url, data, function (result) {
                    toastr.success('Podaci uspješno sačuvani!','Postavke');
                    self.dodavanjeOmoguceno(false);
                    self.editMode(false);
                    self.editRow(0);

                    self.gkNaziv("");
                    self.inkrementCijena("");
                    self.inkrementIznos("");
                    self.nacinPlacanjaOpis("");

                    self.GetData(self.dataMode());
                });



            } else {
                self.viewModelHelper.modelErrors(errors());
            }

        }

        self.Obrisi = function (data) {
            var url = "";
            switch (self.dataMode()) {
                case 'grad':
                    url = "api/postavke/grad/obrisi";
                    break;
                case 'kategorija':
                    url = "api/postavke/kategorija/obrisi";
                    break;
                case 'inkrement':
                    url = "api/postavke/inkrement/obrisi";
                    break;
                case 'nacinplacanja':
                    url = "api/postavke/nacinplacanja/obrisi";
                    break;
                default:
            }
            if (confirm("Potvrda brisanja?")) {
                self.viewModelHelper.apiPost(url, { '': data.Id() }, function (result) {
                    self.GetData(self.dataMode());
                });
            }

        }

        self.Initialize();
    }
    bh.AdministracijaViewModel = AdministracijaViewModel;
}(window.BHao));

