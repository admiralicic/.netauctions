(function (bh) {
    var AukcijaViewModel = function () {

        var self = this;

        self.aukcijaModel = new BHao.AukcijaModel();
        self.viewModelHelper = new BHao.viewModelHelper();
        self.naciniPlacanja = ko.observableArray([]);
        self.kategorije = ko.observableArray([]);
        self.slike = ko.observableArray([]);
        self.slikaGreska = ko.observable(false);
        self.isLoaded = ko.observable(false);
        self.artikli = ko.observableArray();
        self.nazivOptions = ko.computed(function () {
            var nazivi = ko.utils.arrayMap(self.artikli(), function (item) {
                return item.Naziv;
            });
            return ko.utils.arrayGetDistinctValues(nazivi).sort();
        });

        self.proizvodjacOptions = ko.computed(function () {
            var proizvodjaci = ko.utils.arrayMap(self.artikli(), function (item) {
                return { Proizvodjac: item.Proizvodjac, Naziv: item.Naziv };
            });

            var filteredProizvodjaci = ko.utils.arrayFilter(proizvodjaci, function (item) {
                return self.aukcijaModel.Naziv() == item.Naziv;
            });

            filteredProizvodjaci = ko.utils.arrayMap(filteredProizvodjaci, function (item) {
                return item.Proizvodjac;
            });
            return ko.utils.arrayGetDistinctValues(filteredProizvodjaci).sort();
        });

        self.modelOptions = ko.computed(function () {
            var modeli = ko.utils.arrayMap(self.artikli(), function (item) {
                return { Model: item.Model, Proizvodjac: item.Proizvodjac, Naziv: item.Naziv };
            });

            var filteredModeli = ko.utils.arrayFilter(modeli, function (item) {
                return self.aukcijaModel.Naziv() == item.Naziv && self.aukcijaModel.Proizvodjac() == item.Proizvodjac;
            });

            filteredModeli = ko.utils.arrayMap(filteredModeli, function (item) {
                return item.Model;
            });

            return ko.utils.arrayGetDistinctValues(filteredModeli).sort();
        });

        self.Initialize = function () {
            self.isLoaded(true);
            self.GetArtikli();

        }

        self.kreiraj = function (model) {
            var errors = ko.validation.group(model);
            self.viewModelHelper.modelIsValid(model.isValid());
            if (errors().length == 0) {
                for (var i = 0; i < self.slike().length; i++) {
                    if (self.slike()[i].startUpload() == 201) {
                        var unmappedSlika = ko.mapping.toJS(self.slike()[i]);
                        self.aukcijaModel.Slike.push({ SlikaIme: unmappedSlika.SlikaIme, SlikaThumbIme: unmappedSlika.SlikaThumbIme });

                    } else {
                        errors().push("Greška pri dodavanju slike (" + (i + 1) + "), provjerite da li slika zadovoljava kriterij. Dozvoljeni format je '.jpeg'.");
                        self.slikaGreska(true);

                    }
                }
            }

            if (errors().length == 0) {
                var unmappedModel = ko.mapping.toJS(model);
                var uri = 'api/aukcija/kreiraj';
                self.viewModelHelper.apiPost(uri, unmappedModel,
                    function (result) {
                        window.location.href = BHao.rootPath + 'aukcija/' + result + '/detalji?isNovaAukcija=true';
                    });
            }
            else {
                self.viewModelHelper.modelIsValid(false);
                self.viewModelHelper.modelErrors(errors());
                if (self.slikaGreska() == true) {
                    self.slike.removeAll();
                    self.slikaGreska(false);
                }
                self.aukcijaModel.Slike.removeAll();
            }
        };


        self.viewModelHelper.apiGet('api/lookup/naciniplacanja', null,
                  function (result) {
                      ko.mapping.fromJS(result, {}, self.naciniPlacanja);
                  });

        self.viewModelHelper.apiGet('api/lookup/kategorije', null,
                    function (result) {
                        ko.mapping.fromJS(result, {}, self.kategorije);
                    });

        self.GetArtikli = function () {
            self.viewModelHelper.apiGet('api/lookup/artikli', null,
                function (result) {

                    self.artikli(ko.utils.arrayMap(result, function (item) {
                        return { Naziv: item.Naziv, Proizvodjac: item.Proizvodjac, Model: item.Model };
                    }))
                });
        }


        self.slikaSelected = function (files) {
            if (files.length < 4 && files.length > 0) {
                self.slike.removeAll();
                for (var i = 0; i < files.length; i++) {
                    if (files[i]) {
                        self.slike.push(new BHao.SlikaModel({ SlikaIme: files[i].name, Slika: files[i] }));
                        var x = self.slike().length;
                        toastr.success('Odabrali ste ' + files[i].name, 'Slike');
                    }
                }
            }
            else {
                toastr.error('Odabrali ste vise od 3 slike, pokusajte ponovo!', 'Greška');
            }

        };

        //self.upload = function () {
        //    for (var i = 0; i < self.slike().length; i++) {
        //        self.slike()[i].startUpload();
        //        self.aukcijaModel.Slike.push(self.slike()[i].SlikaIme);
        //    }
        //};

        self.Initialize();
    };
    bh.AukcijaViewModel = AukcijaViewModel;
}(window.BHao));