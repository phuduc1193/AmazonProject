angular.module('authServiceApp', [])
    .controller('NewApiResourceController', ['$http', function ($http) {
        const defaultScope = {
            Name: "",
            DisplayName: "",
            Description: "",
            Required: true,
            Emphasize: true
        };
        var api = this;
        api.Name = "";
        api.DisplayName = "";
        api.Description = "";
        api.Scopes = [angular.copy(defaultScope)];

        api.addScope = function () {
            api.Scopes.push(angular.copy(defaultScope));
        };

        api.removeScope = function (index) {
            api.Scopes.splice(index, 1);
        }

        api.create = function () {
            $http.post('New', api, {
                headers: {
                    'X-XSRF-TOKEN': $('#RequestVerificationToken').val()
                }
            }).then(function () {
                $("#cancel").get(0).click();
            }, function () {
                bootbox.alert({
                    message: '<div class="py-3">An unexpected error has occurred. Please try again!</div>',
                    callback: function () {
                        location.reload();
                    },
                    buttons: {
                        ok: {
                            label: 'Try again',
                            className: 'btn btn-outline-danger'
                        }
                    }
                });
            });
        };
    }]);