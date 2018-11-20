angular.module('authServiceApp', [])
    .controller('NewApiResourceController', ['$http', function ($http) {
        const defaultScope = {
            name: "",
            displayName: "",
            description: "",
            required: true,
            emphasize: true
        };
        var api = this;
        api.name = "";
        api.displayName = "";
        api.description = "";
        api.secret = "";
        api.enabled = true;
        api.scopes = [angular.copy(defaultScope)];
        api.claims = [];

        api.addScope = function () {
            api.scopes.push(angular.copy(defaultScope));
        };

        api.removeScope = function (index) {
            api.scopes.splice(index, 1);
        }

        api.create = function () {
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

            api.claims = api.defaultClaims.filter(function (c) {
                return c.checked;
            });

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

        validateData = function (data) {
            if (!data)
                return false;
            if (!data.name || !data.displayName || !data.description || !data.secret)
                return false;
            return true;
        }

        api.generateSecret = function () {
            const secretString = sha256(Math.random().toString(36).replace(/[^a-z]+/g, '').substr(0, 5));
            api.secret = secretString;
        }

        api.clipboardText = function (str) {
            const el = document.createElement('textarea');
            el.value = str;
            document.body.appendChild(el);
            el.select();
            document.execCommand('copy');
            document.body.removeChild(el);
        }

        function getClaims() {
            $http.get('/api/claims').then(function (results) {
                api.defaultClaims = results.data.sort(function (a, b) {
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
    }]);