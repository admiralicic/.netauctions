(function (bh) {
    var KorisnikModel = function (data) {

        var self = this;

        self.Id = ko.observable(data ? data.Id() : "");
        //self.KorisnickoIme = ko.observable(data ? data.KorisnickoIme() : "").extend({ required: { message: "Morate unijeti korisničko ime!" } });
        self.Email = ko.observable(data ? data.Email() : "").extend({ email: true, required: { message: "Morate unijeti email!" } });
        self.Ime = ko.observable(data ? data.Ime() : "").extend({ required: { message: "Morate unijeti ime!" } });
        self.Prezime = ko.observable(data ? data.Prezime() : "").extend({ required: { message: "Morate unijeti prezime!" } });
        self.Ulica = ko.observable(data ? data.Ulica() : "").extend({ required: { message: "Morate unijeti ulicu!" } });
        self.Broj = ko.observable(data ? data.Broj() : "").extend({ required: { message: "Morate unijeti broj!" } });
        self.GradId = ko.observable(data ? data.GradId() : "").extend({ required: { message: "Morate odabrati grad!" } });
        self.PostanskiBroj = ko.observable(data ? data.PostanskiBroj() : "").extend({ required: { message: "Morate unijeti poštanski broj!"} });
        self.Telefon = ko.observable(data ? data.Telefon() : "")

        ko.validation.registerExtenders();

        self.Uloge = ko.observableArray(data ? data.Uloge() : []).extend({ uloge: { message: "Korisnik ne može biti uposlenik i klijent istovremeno" } });
        self.IsOnemogucen = ko.observable(data ? data.IsOnemogucen() : false);
        self.Lozinka = ko.observable("");
    }
    bh.KorisnikModel = KorisnikModel;
}(window.BHao));