(function ($) {
    viewModel = {
        computerGameCollection: ko.observableArray(),
        requestVerificationToken: ko.observable(),
        loaded: ko.observable(false)
    };

    function ComputerGame(Id, Name, Description, ReleaseDate, Rating, Editable = false) {
        var self = this;
        self.Id = ko.observable(Id);
        self.Name = ko.observable(Name).extend({
            required: { message: 'Name is required.' }
        })
        self.Description = ko.observable(Description);
        var releaseDateString = '';
        if (ReleaseDate) {
            var ticks = ReleaseDate.match(/(\d)+/g);
            var releaseDateJs = new Date(parseInt(ticks[0]));
            releaseDateString = releaseDateJs.ddmmyyyy();
        }
        self.ReleaseDate = ko.observable(releaseDateString).extend({
            required: true,
            pattern: {
                message: 'Release Date must be of the form dd/MM/yyyy',
                params: /^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$/
            }
        });
        self.Rating = ko.observable(Rating).extend({ digit: true }).extend({ min:1, max: 10 });
        self.Errors = ko.validation.group(self);
        self.Editable = ko.observable(Editable);
        self.Deleted = ko.observable(false);
        self.Update = function () {
            saveData(
                self,
                function (data) { if (data !== 0) { self.Editable(false); self.Id(data); } else { console.log('error'); console.log('data not saved'); } },
                function (data) { console.log('error'); console.log(data); })
        };
        self.AllowEdit = function () {
            self.Editable(true);
        };
        self.StopEdit = function () {
            getData();
        };
        self.Delete = function () {
            deleteData(
                self,
                function (data) {
                    self.Deleted(true);
                    console.log('deleted');
                },
                function (data) { console.log('error'); console.log(data); })
        };
    };

    Date.prototype.ddmmyyyy = function () {
        var mm = this.getMonth() + 1; // getMonth() is zero-based
        var dd = this.getDate();

        return [(dd > 9 ? '' : '0') + dd,
            (mm > 9 ? '' : '0') + mm,
            this.getFullYear()        
        ].join('/');
    };

    function ajaxCall(type, postUrl, submitData, successCallback, errorCallback) {
        $.ajax({
            type: type,
            url: postUrl,
            data: submitData
        }).done(function (data) {
            successCallback(data);
        }).fail(function (ex) {
            errorCallback(ex);
        })
    }

    function getData() {
        var successCallback = function (data) {
            var mappedData = ko.utils.arrayMap($(data), function (element) {
                return new ComputerGame(element.Id, element.Name, element.Description, element.ReleaseDate, element.Rating);
            });
            viewModel.computerGameCollection(mappedData);
            viewModel.requestVerificationToken($("input[name=__RequestVerificationToken]").val());
            ko.applyBindings(viewModel, $("body")[0]);
            viewModel.loaded(true);
            $(".getData").show();
            $(".datepicker").datepicker();
        }
        var errorCallback = function (error) {
            console.log("Error:");
            console.log(error);
        };
        ajaxCall("GET", "/ComputerGames/IndexJson", null, successCallback, errorCallback);
    }

    function saveData(currentData, successCallback, errorCallback) {
        var postUrl = "";
        var submitData = {
            __RequestVerificationToken: viewModel.requestVerificationToken(),
            Id: currentData.Id(),
            Name: currentData.Name(),
            Description: currentData.Description(),
            ReleaseDate: currentData.ReleaseDate(),
            Rating: currentData.Rating()
        };
        if (currentData.Id() && currentData.Id() > 0) {
            postUrl = "/ComputerGames/EditJson"
        }
        else {
            postUrl = "/ComputerGames/CreateJson"
        }
        ajaxCall("POST", postUrl, submitData, successCallback, errorCallback);
    }

    function deleteData(currentData, successCallback, errorCallback) {
        var postUrl = "/ComputerGames/DeleteJson";
        var submitData = {
            __RequestVerificationToken: viewModel.requestVerificationToken(),
            Id: currentData.Id(),

        };
        ajaxCall("POST", postUrl, submitData, successCallback, errorCallback);
    }

    $(document).ready(function () {
        getData();
    });

    $(document).on("click", ".create", function () {
        var newComputerGame = ComputerGame(0, '', '', '', '', true);
        viewModel.computerGameCollection.push(newComputerGame);
    });
})(jQuery);