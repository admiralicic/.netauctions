window.BHao = {};

(function (bh) {
    var rootPath;
    bh.rootPath = rootPath;
}(window.BHao));

(function (bh) {
    var viewModelHelper = function () {
        var self = this;

        self.modelIsValid = ko.observable(true);
        self.modelErrors = ko.observableArray();

        self.apiPost = function (uri, data, success, failure, always) {
            self.modelIsValid(true);
            $.post(BHao.rootPath + uri, data)
            .done(success)
            .fail(function (result) {
                if (failure == null) {
                    if (result.status != 400) {
                        self.modelErrors([result.responseText]);
                    }
                    else {
                        self.modelErrors(JSON.parse(result.responseText));
                    }
                    self.modelIsValid(false);
                }
                else {
                    failure(result);
                }
            })
            .always(function () {
                if (always != null) {
                    always();
                }
            });

        };

        self.apiGet = function (uri, data, success, failure, always) {
            self.modelIsValid(true);
            $.get(BHao.rootPath + uri, data)
            .done(success)
            .fail(function (result) {
                if (failure == null) {
                    if (result.status != 400) {
                        self.modelErrors([result.status + ':' + result.statusText + '-' + result.responseText]);
                    }
                    else {
                        self.modelErrors(JSON.parse(result.responseText));
                    }
                    self.modelIsValid(false);
                }
                else {
                    failure(result);
                }
            })
            .always(function () {
                if (always != null) {
                    always();
                }
            });

        }

        self.getUrlVars = function () {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        }

        self.posaljiPoruku = function (posiljaljacId, primalacId, aukcijaId, textPoruke, success) {
            self.apiPost('api/poruka/posalji', { TextPoruke: textPoruke, PosiljalacId: posiljaljacId, PrimalacId: primalacId, AukcijaId: aukcijaId }, success);
        }

    }
    bh.viewModelHelper = viewModelHelper;
}(window.BHao));

ko.bindingHandlers.date = {
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var value = valueAccessor();
        var allBindings = allBindingsAccessor();
        var valueUnwrapped = ko.utils.unwrapObservable(value);

        var pattern = allBindings.format || 'DD/MM/YYYY';

        var output = "";
        if (valueUnwrapped !== null && valueUnwrapped !== undefined && valueUnwrapped.length > 0) {
            output = moment(valueUnwrapped).format(pattern);
        }

        if ($(element).is("input") === true) {
            $(element).val(output);
        } else {
            $(element).text(output);
        }
    }
};

ko.bindingHandlers.enterkey = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var allBindings = allBindingsAccessor();
        $(element).keypress(function (event) {
            var keyCode = (event.which ? event.which : event.keyCode);
            if (keyCode === 13) {
                allBindings.enterkey.call(viewModel);
                return false;
            }
            return true;
        });
    }
};

ko.bindingHandlers.escapekey = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var allBindings = allBindingsAccessor();
        $(element).keypress(function (event) {
            var keyCode = (event.which ? event.which : event.keyCode);
            if (keyCode === 27) {
                allBindings.escapekey.call(viewModel);
                return false;
            }
            return true;
        });
    }
};

//ko.validation.rules['dateAndFormat'] = {
//    validator: function (val, format) {
//        return ko.validation.utils.isEmptyVal(val); //|| moment(val, format).isValid();
//    },
//    message: 'Unesite ispravan datum.'
//};

ko.validation.rules['dateGreaterThan'] = {
    validator: function (val, otherVal) {
        var datum1 = moment(val).format();
        var datum2 = moment(otherVal).format();

        return datum1 >= datum2;
    },
    message: function (otherVal) {
        var date = moment(otherVal).format('DD-MMM-YYYY')
        return 'Datum plaćanja ne može biti prije ' + date
    }
};

ko.validation.rules['dateLessThan'] = {
    validator: function (val, otherVal) {
        var datum1 = moment(val).format();
        var datum2 = moment(otherVal).format();

        return datum1 <= datum2;
    },
    message: function (val) {
        var date = moment(val).format('DD-MMM-YYYY')
        return 'Datum plaćanja ne može biti poslije ' + date
    }
};

ko.validation.rules['uloge'] = {
    validator: function (val) {
        return (val.indexOf('Admin') > 0 || val.indexOf('Uposlenik') > 0) && val.indexOf('Klijent') > 0 ? false : true;
    },
    message: function () {
        return 'Korisnik ne može biti i uposlenik i klijent istovremeno.';
    }
};

ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        var $el = $(element);

        //initialize datepicker with some optional options
        var options = allBindingsAccessor().datepickerOptions || {};
        $el.datepicker(options);

        //handle the field changing
        ko.utils.registerEventHandler(element, "change", function () {
            var observable = valueAccessor();
            observable($el.datepicker("getDate"));
        });

        //handle disposal (if KO removes by the template binding)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $el.datepicker("destroy");
        });

    },
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor()),
            $el = $(element),
            current = $el.datepicker("getDate");

        if (value - current !== 0) {
            $el.datepicker("setDate", value);
        }
    }
};


