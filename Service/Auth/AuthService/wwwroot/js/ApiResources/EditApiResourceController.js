angular.module('authServiceApp', [])
    .controller('EditApiResourceController', ['$http', function ($http) {
        const defaultScope = {
            Name: "",
            DisplayName: "",
            Description: "",
            Required: true,
            Emphasize: true
        };
        var api = this;
        const data = JSON.parse($("#Data").val());

        if (data) {
            api.Id = data.Id;
            api.Name = data.Name;
            api.DisplayName = data.DisplayName;
            api.Description = data.Description;
            api.Enabled = data.Enabled;
            api.Scopes = data.Scopes;
        }

        api.addScope = function () {
            api.Scopes.push(angular.copy(defaultScope));
        };

        api.removeScope = function (index) {
            api.Scopes.splice(index, 1);
        }

        api.edit = function () {
            $http.post('Edit', api, {
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