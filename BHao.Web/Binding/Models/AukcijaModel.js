(function (bh) {
    var AukcijaModel = function () {

        var self = this;

        self.Trajanje = ko.observable().extend({
            required: { message: "Unesite trajanje aukcije." },
            number: { message: "Unesite trajanje aukcije 2 - 7 dana." },
            min: { params: 2, message: "Unesite trajanje aukcije 2 - 7 dana."},
            max: { params: 7, message: "Unesite trajanje aukcije 2 - 7 dana." }
        });        
        self.MinimalnaCijena = ko.observable().extend({ number: { message: "Minimalna cijena mora biti broj." } });

        ko.validation.rules['moraBitiVece'] = {            
            validator: function (val, otherVal) {
                if (val == null || otherval == null) {
                    return true;
                }
                return parseFloat(val) > parseFloat(otherVal);
            },
            message: 'Kupi odmah cijena mora biti veća od {0}'
        };
        ko.validation.registerExtenders();

        self.KupiOdmahCijena = ko.observable().extend({
            number: { message: "Kupi odmah cijena mora biti broj." },
            moraBitiVece: self.MinimalnaCijena
        });

        self.Proizvodjac = ko.observable("").extend({
            required: { message: "Unesite proizvođača." }
        });
        self.Model = ko.observable("").extend({
            required: {message: "Unesite model artikla."}});
        self.Naziv = ko.observable("").extend({
            required: { message: "Unesite naziv artikla." }
        });
        self.DetaljanOpis = ko.observable("");
        self.Napomena = ko.observable(""); 
        self.NacinPlacanjaId = ko.observable();
        self.Slike = ko.observableArray([]);
        self.KategorijaId = ko.observable();
    }
    window.BHao.AukcijaModel = AukcijaModel;
}(window.BHao));
