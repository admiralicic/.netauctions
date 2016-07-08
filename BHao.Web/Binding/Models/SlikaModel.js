(function (bh) {
    var SlikaModel = function (data) {
        var self = this;
        self.SlikaIme = ko.observable(data.SlikaIme);
        self.SlikaThumbIme = ko.observable();
        self.Slika = ko.observable(data.Slika);
        self.status;

        this.startUpload = function () {
            var fd = new FormData();
            fd.append("fileToUpload", self.Slika());
            var request = new XMLHttpRequest();

            request.onreadystatechange = function () {
                if (request.readyState === 4) {
                    if (request.status === 201) {
                        var s = JSON.parse(request.responseText);

                        self.SlikaIme(s.SlikaIme);
                        self.SlikaThumbIme(s.SlikaThumbIme);
                    }
                    self.status = request.status;
                    return self.status;
                }
                
            };

            request.open("POST", "/api/slike/upload", false);
            request.setRequestHeader("Accept", "application/json");
            request.send(fd);
            return self.status;

        };


    }
    window.BHao.SlikaModel = SlikaModel;
}(window.BHao));