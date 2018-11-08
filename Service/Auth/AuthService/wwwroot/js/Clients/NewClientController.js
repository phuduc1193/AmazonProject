angular.module('authServiceApp', [])
    .controller('NewClientController', ['$http', '$scope', function ($http, $scope) {

        $scope.activeTabIndex = 0;
        $scope.progression = 0;

        $scope.activeTab = function (index) {
            $scope.activeTabIndex = index;
            $scope.progression = index;
        }

        $scope.getBreadcrumFieldClass = function (index) {
            return { 'active': $scope.activeTabIndex == index, 'completed': $scope.progression > index && $scope.activeTabIndex != index };
        }

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
        api.Enabled = true;
        api.Scopes = [angular.copy(defaultScope)];
        api.Secrets = [];

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
    }]);