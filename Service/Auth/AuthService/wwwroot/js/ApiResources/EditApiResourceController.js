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
            api.Secrets = data.Secrets;
            api.Scopes = data.Scopes;
            api.UserClaims = data.UserClaims;
        }

        api.addScope = function () {
            api.Scopes.push(angular.copy(defaultScope));
        };

        api.removeScope = function (index) {
            api.Scopes.splice(index, 1);
        }

        api.edit = function () {
            if (!validateData(api)) {
                bootbox.alert({
                    message: '<div class="py-3">Invalid request. Please fill all required fields!</div>',
                    buttons: {
                        ok: {
                            label: 'Try again',
                            className: 'btn btn-outline-danger'
                        }
                    }
                });
                return;
            }

            $http.put('Edit', api, {
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

        validateData = function (data) {
            console.log(data);
            if (!data)
                return false;
            if (!data.Name || !data.DisplayName || !data.Description)
                return false;
            if (!data.Secrets || !data.Secrets[0].Value)
                return false;
            if (!data.Scopes)
                return false;
            return true;
        }

        api.generateSecret = function () {
            api.Secrets = [];
            const secretString = sha256(Math.random().toString(36).replace(/[^a-z]+/g, '').substr(0, 5));
            const secretData = {
                Value: secretString,
                Description: `Secret for ${api.Name}`
            };
            api.Secrets.push(secretData);
        }

        api.clipboardText = function (str) {
            const el = document.createElement('textarea');
            el.value = str;
            document.body.appendChild(el);
            el.select();
            document.execCommand('copy');
            document.body.removeChild(el);
        }

        api.addOrRemoveClaim = function (claim) {
            const index = api.UserClaims.findIndex(function (c) {
                return c.Type == claim.type;
            });
            if (index > -1)
                api.UserClaims.splice(index, 1);
            else api.UserClaims.push({ Type: claim.type });
        }

        function getClaims() {
            $http.get('/api/claims').then(function (results) {
                api.claims = results.data.sort(function (a, b) {
                    return a.name.length - b.name.length;
                });
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
        }

        getClaims();

        api.shouldCheck = function (claim) {
            if (!api.UserClaims || api.UserClaims.length == 0)
                return false;
            return api.UserClaims.filter(function (c) {
                return c.Type == claim.type;
            }).length > 0;
        }
    }]);