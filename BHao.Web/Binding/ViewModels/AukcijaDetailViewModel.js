(function (bh) {
    var AukcijaDetailViewModel = function (userId) {
        var self = this;
        self.prijavljeniKorisnikId = ko.observable(userId);
        self.viewModelHelper = new BHao.viewModelHelper();
        self.Aukcija = ko.observable();
        self.odabranaSlika = ko.observable();
        self.ocjenaKupca = ko.observable(0);
        self.ocjenaProdavca = ko.observable(0);
        self.ocjenaArtikla = ko.observable(0);
        self.ocjene = ko.observableArray([1, 2, 3, 4, 5]);
        self.ocjenaKupcaEnabled = ko.observable(false);
        self.ocjenaProdavcaEnabled = ko.observable(false);
        self.ocjenaArtiklaEnabled = ko.observable(false);
        self.textKomentara = ko.observable();
        self.textKomentaraArtikla = ko.observable();
        self.isKomentarOmogucen = ko.observable(false);
        self.isKomentarArtiklaOmogucen = ko.observable(false);
        self.iznosPonude = ko.observable().extend(
            {
                number: { message: "Iznos ponude mora biti broj." }
            });
        self.isPrijavljeniKorisnikPobjednik = ko.observable(false);
        self.omoguciKupiOdmah = ko.observable(true);
        self.unosPonudeEnabled = ko.observable(false);
        self.errors = ko.validation.group(self, { deep: true, observable: false });

        self.ocjenaProdavcaButtonVisible = ko.observable(true);
        self.ocjenaKupcaButtonVisible = ko.observable(true);
        self.ocjenaArtiklaButtonVisible = ko.observable(true);
        self.isPorukaMode = ko.observable(false);
        self.textPoruke = ko.observable();
        self.preporuke = ko.observableArray([]);

        self.isUcesnik = ko.computed(function () {
            if (self.Aukcija()) {
                var ponude = self.Aukcija().Ponude();

                var ucesnik = ko.utils.arrayFirst(ponude, function (item) {
                    return item.KorisnikId == self.prijavljeniKorisnikId();
                });

                if (ucesnik) {
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }

        });


        self.isLoaded = ko.observable(false);


        self.Initialize = function () {
            var url = window.location.pathname;
            var prvi = url.indexOf('/', 1);
            var drugi = url.indexOf('/', prvi + 1);

            self.aukcijaId = parseInt(url.substring(prvi + 1, drugi));
            self.GetDetalji();
            setInterval(function () { self.GetDetalji();  }, 10000);

            self.isLoaded(true);
            var isNovaAukcija = self.viewModelHelper.getUrlVars()["isNovaAukcija"];
            if (isNovaAukcija == "true") {

                toastr.success('Uspješno ste kreirali aukciju!', 'Nova aukcija');
            }
        }


        self.GetDetalji = function () {
            if (self.viewModelHelper.modelIsValid()) {
                self.viewModelHelper.apiPost('api/aukcija/detalji', { '': self.aukcijaId },
                    function (result) {
                        if (!self.Aukcija()) {
                            self.Aukcija(new BHao.AukcijaDetailModel(result));
                            self.odabranaSlika(self.Aukcija().Slike()[0].SlikaIme);
                        } else {
                            self.Aukcija().NajvecaPonuda(result.NajvecaPonuda);
                            self.Aukcija().NajveciPonudjacId(result.NajveciPonudjacId);
                            self.Aukcija().NajveciPonudjac(result.NajveciPonudjac);
                            self.Aukcija().Ponude(result.Ponude);
                            self.Aukcija().Zavrsena(result.Zavrsena);
                            self.Aukcija().KomentarKorisnika(result.KomentarKorisnika);
                            self.Aukcija().KomentarArtikla(result.KomentarArtikla);
                            self.Aukcija().Zavrsetak(result.Zavrsetak);
                            self.Aukcija().Artikal(result.Artikal);
                        }

                        self.GetPreporuke();
                        if (self.prijavljeniKorisnikId() == self.Aukcija().NajveciPonudjacId()) {
                            self.isPrijavljeniKorisnikPobjednik(true);
                        } else {
                            self.isPrijavljeniKorisnikPobjednik(false);
                        }

                        self.KupiOdmahOnemoguceno();
                        if (self.Aukcija().NajvecaPonuda()) {
                            if (self.isPrijavljeniKorisnikPobjednik() == false || self.Aukcija().NajvecaPonuda().KorisnikId != self.prijavljeniKorisnikId()) {
                                self.unosPonudeEnabled(true);
                            }
                            else {
                                self.unosPonudeEnabled(false);
                            }
                        }
                        else {
                            self.unosPonudeEnabled(true);
                        }

                        if (self.Aukcija().OcjeneKorisnika().length > 0) {



                            var ocKupca = ko.utils.arrayFilter(self.Aukcija().OcjeneKorisnika(), function (ocjena) {
                                return ocjena.OcijenjeniKorisnikId == self.Aukcija().NajveciPonudjacId();
                            });

                            var ocProdavca = ko.utils.arrayFilter(self.Aukcija().OcjeneKorisnika(), function (ocjena) {
                                return ocjena.OcijenjeniKorisnikId == self.Aukcija().ProdavacId();

                            });

                            

                            self.ocjenaKupca(ocKupca[0] ? ocKupca[0].Ocjena : 0);
                            self.ocjenaKupcaButtonVisible(!(self.ocjenaKupca() > 0));

                            self.ocjenaProdavca(ocProdavca[0] ? ocProdavca[0].Ocjena : 0);
                            self.ocjenaProdavcaButtonVisible(!(self.ocjenaProdavca() > 0));
                           


                        }

                        if (self.Aukcija().OcjeneArtikla().length > 0) {
                            var ocArtikla = ko.utils.arrayFilter(self.Aukcija().OcjeneArtikla(), function (ocjena) {
                                return ocjena.OcijenioId == self.Aukcija().NajveciPonudjacId() && ocjena.AukcijaId == self.Aukcija().Id();
                            });
                            self.ocjenaArtikla(ocArtikla[0] ? ocArtikla[0].Ocjena : 0);
                            self.ocjenaArtiklaButtonVisible(!(self.ocjenaArtikla() > 0));
                        }

                        if (self.Aukcija().KomentarKorisnika()) {

                            self.textKomentara(self.Aukcija().KomentarKorisnika().TextKomentara);
                        }

                        if (self.Aukcija().KomentarArtikla()) {
                            self.textKomentaraArtikla(self.Aukcija().KomentarArtikla().TextKomentara);

                        }

                        if (self.Aukcija().Zavrsena() == true && self.Aukcija().NajvecaPonuda()) {
                            if (self.prijavljeniKorisnikId() == self.Aukcija().ProdavacId()) {
                                self.ocjenaKupcaEnabled(true);
                                self.ocjenaProdavcaEnabled(false);
                            }

                            if (self.prijavljeniKorisnikId() == self.Aukcija().NajveciPonudjacId()) {
                                self.ocjenaProdavcaEnabled(true);
                                self.ocjenaKupcaEnabled(false);
                                self.ocjenaArtiklaEnabled(true);
                            }

                            self.OmoguciKomentar();
                            self.OmoguciKomentarArtikla();

                        } else {
                            self.ocjenaKupcaEnabled(false);
                            self.ocjenaProdavcaEnabled(false);
                            self.ocjenaArtiklaEnabled(false);
                        }

                    });
            }
        }
        self.PrikaziSliku = function (data) {
            self.odabranaSlika(data.SlikaIme);
        }

        self.KreirajPonudu = function () {
            self.viewModelHelper.modelIsValid(self.isValid());
            if (self.errors().length == 0) {
                self.viewModelHelper.apiPost('api/aukcija/ponudi', { AukcijaId: self.aukcijaId, IznosPonude: self.iznosPonude() },
                function (result) {
                    self.GetDetalji();
                    self.KupiOdmahOnemoguceno();
                });
            }
            else {
                self.viewModelHelper.modelIsValid(false);
                self.viewModelHelper.modelErrors(self.errors());
            }
        }

        self.KupiOdmah = function () {
            if (self.omoguciKupiOdmah) {
                self.viewModelHelper.apiPost('api/aukcija/kupiodmah', { '': self.aukcijaId },
                                function () {
                                    self.GetDetalji();
                                });
            }
        }

        self.KupiOdmahOnemoguceno = function () {
            if (self.Aukcija().NajvecaPonuda() != null) {
                if (self.Aukcija().KupiOdmahCijena() < self.Aukcija().TrenutnaCijena()) {
                    self.omoguciKupiOdmah(false);
                }
            }

            if (self.Aukcija().KupiOdmahCijena() == 0) {
                self.omoguciKupiOdmah(false);
            }

            if (self.Aukcija().Zavrsena() == true) {
                self.omoguciKupiOdmah(false);
            }
        }

        self.OcijeniKorisnika = function () {
            //Ocjena kupca
            if (self.prijavljeniKorisnikId() == self.Aukcija().ProdavacId() && self.Aukcija().Zavrsena()) {
                self.viewModelHelper.apiPost('api/aukcija/ocijeni', {
                    OcijenjeniKorisnikId: self.Aukcija().NajveciPonudjacId(),
                    OcjenjivacId: self.prijavljeniKorisnikId(),
                    Ocjena: self.ocjenaKupca,
                    AukcijaId: self.Aukcija().Id()
                },
                function () {
                    self.ocjenaKupcaButtonVisible(false);
                });
            }

            if (self.prijavljeniKorisnikId() == self.Aukcija().NajveciPonudjacId() && self.Aukcija().Zavrsena()) {
                //Ocjena prodavca
                self.viewModelHelper.apiPost('api/aukcija/ocijeni', {
                    OcijenjeniKorisnikId: self.Aukcija().ProdavacId,
                    OcjenjivacId: self.Aukcija().NajveciPonudjacId(),
                    Ocjena: self.ocjenaProdavca,
                    AukcijaId: self.Aukcija().Id()
                },
                function () {
                    self.ocjenaProdavcaButtonVisible(false);
                });
            }
        }

        self.OcijeniArtikal = function () {
            if (self.prijavljeniKorisnikId() == self.Aukcija().NajveciPonudjacId() && self.Aukcija().Zavrsena()) {
                self.viewModelHelper.apiPost('api/aukcija/artikal/ocijeni', {
                    AukcijaId: self.Aukcija().Id(),
                    ArtikalId: self.Aukcija().ArtikalId(),
                    Ocjena: self.ocjenaArtikla,
                    OcijenioId: self.Aukcija().NajveciPonudjacId()
                },
                function () {
                    self.ocjenaArtiklaButtonVisible(false);
                });
            }
        }

        self.OmoguciKomentar = function () {
            if (self.Aukcija().Zavrsena()) {
                if (self.prijavljeniKorisnikId() == self.Aukcija().NajveciPonudjacId() || self.prijavljeniKorisnikId() == self.Aukcija().ProdavacId()) {
                    self.isKomentarOmogucen(!self.textKomentara());
                }
                else {
                    self.isKomentarOmogucen(false);
                }
            }
        }

        self.OmoguciKomentarArtikla = function () {
            if (self.Aukcija().Zavrsena()) {
                if (self.prijavljeniKorisnikId() == self.Aukcija().NajveciPonudjacId()) {
                    self.isKomentarArtiklaOmogucen(!self.textKomentaraArtikla());
                }
                else {
                    self.isKomentarArtiklaOmogucen(false);
                }
            }
        }

        self.DodajKomentar = function () {
            //komentar kupca
            if (self.prijavljeniKorisnikId() == self.Aukcija().ProdavacId() && self.Aukcija().Zavrsena()) {
                var komentar = {
                    TextKomentara: self.textKomentara,
                    KomentatorId: self.prijavljeniKorisnikId(),
                    KomentiraniKorisnikId: self.Aukcija().NajveciPonudjacId(),
                    AukcijaId: self.Aukcija().Id()
                };
            }

            //komentar prodavca
            if (self.prijavljeniKorisnikId() == self.Aukcija().NajveciPonudjacId() && self.Aukcija().Zavrsena()) {
                var komentar = {
                    TextKomentara: self.textKomentara,
                    KomentatorId: self.prijavljeniKorisnikId(),
                    KomentiraniKorisnikId: self.Aukcija().ProdavacId(),
                    AukcijaId: self.Aukcija().Id()
                };
            }

            self.viewModelHelper.apiPost('api/aukcija/komentar/dodaj', ko.toJS(komentar), function () {
                self.OmoguciKomentar();
            });
        }

        self.DodajKomentarArtikla = function () {
            if (self.prijavljeniKorisnikId() == self.Aukcija().NajveciPonudjacId() && self.Aukcija().Zavrsena()) {
                var komentar = {
                    TextKomentara: self.textKomentaraArtikla,
                    KomentatorId: self.prijavljeniKorisnikId(),
                    ArtikalId: self.Aukcija().ArtikalId(),
                    AukcijaId: self.Aukcija().Id()
                };

                self.viewModelHelper.apiPost('api/aukcija/komentarArtikla/dodaj', ko.toJS(komentar), function () {
                    self.OmoguciKomentarArtikla();
                });
            }
        }

        self.TogglePorukaMode = function () {
            self.isPorukaMode(!self.isPorukaMode());
        }

        self.PosaljiPoruku = function () {
            if (!self.textPoruke()) {
                self.viewModelHelper.modelIsValid(false);
                self.viewModelHelper.modelErrors.push("Ne možete poslati praznu poruku!");
            }
            else {
                self.viewModelHelper.posaljiPoruku(self.prijavljeniKorisnikId, self.Aukcija().ProdavacId(), self.aukcijaId, self.textPoruke, function () {
                    self.textPoruke("");
                    self.TogglePorukaMode();
                    toastr.success('Poruka poslana!', 'Poruka');
                })
            }
        

        }

        self.OtkaziPoruku = function () {
            self.textPoruke("");
            self.isPorukaMode(false);
        }

        self.GetPreporuke = function () {
            if (self.Aukcija()) {
                self.viewModelHelper.apiPost('api/aukcija/getpreporuke', { '': self.Aukcija().ArtikalId() }, function (result) {
                    ko.mapping.fromJS(result, {}, self.preporuke);
                });
            }
        }

        self.PrikaziAukciju = function (data, event) {
            var e = event || window.event;
            e.stopPropagation();

            window.location.href = BHao.rootPath + 'aukcija/' + data.Id() + '/detalji';
        }

        self.Initialize();

    }
    bh.AukcijaDetailViewModel = AukcijaDetailViewModel;
}(window.BHao));