angular.module('authServiceApp', [])
    .controller('NewClientController', ['$http', '$scope', function ($http, $scope) {
        $scope.activeTabIndex = 0;
        $scope.progression = 0;

        $scope.client = {
            ClientId: "",
            ClientName: "",
            Description: "",
            AllowedScopes: [],
            Enabled: true,
            RequireConsent: true
        };

        $scope.activeTab = function (event, index) {
            if (index > $scope.progression) {
                event.preventDefault();
                event.stopPropagation();
                return;
            }
            $scope.activeTabIndex = index;
        }

        $scope.shouldAdvanceInfo = function (e) {
            e.preventDefault();
            if ($scope.client.AllowedGrantTypes)
                $('#info-tab').get(0).click();
        }

        $scope.shouldAdvanceScope = function (e) {
            e.preventDefault();
            if (!$scope.client.ClientId || !$scope.client.ClientName || !$scope.client.Description)
                return;
            if ($scope.grantType != 'Implicit' && $scope.client.ClientSecrets.length == 0)
                return;
            if ($scope.grantType != 'ClientCredentials' && !$scope.client.RedirectUris)
                return;

            if ($scope.progression < 2)
                $scope.progression = 2;
            $('#scope-tab').get(0).click();
        }

        $scope.getBreadcrumFieldClass = function (index) {
            return { 'active': $scope.activeTabIndex == index, 'completed': $scope.progression > index && $scope.activeTabIndex != index, 'muted not-allowed': $scope.progression < index };
        }

        $scope.setAllowedGrantTypes = function (type) {
            $scope.grantType = type;
            switch (type) {
                case 'Implicit':
                    $scope.client.AllowedGrantTypes = [{ GrantType: 'implicit' }];
                    delete $scope.client.ClientSecrets;
                    $scope.client.RequireClientSecret = false;
                    $scope.client.AllowedCorsOrigins = [{ Origin: "" }];
                    $scope.client.RedirectUris = [{ RedirectUri: "" }];
                    $scope.client.PostLogoutRedirectUris = [{ PostLogoutRedirectUri: "" }];
                    $scope.client.AllowAccessTokensViaBrowser = true;
                    break;
                case 'Hybrid':
                    $scope.client.AllowedGrantTypes = [{ GrantType: 'hybrid' }];
                    $scope.client.ClientSecrets = [];
                    $scope.client.RequireClientSecret = true;
                    delete $scope.client.AllowedCorsOrigins;
                    $scope.client.RedirectUris = [{ RedirectUri: "" }];
                    $scope.client.PostLogoutRedirectUris = [{ PostLogoutRedirectUri: "" }];
                    delete $scope.client.AllowAccessTokensViaBrowser;
                    break;
                default:
                    $scope.client.AllowedGrantTypes = [{ GrantType: 'client_credentials' }];
                    $scope.client.ClientSecrets = [];
                    $scope.client.RequireClientSecret = true;
                    delete $scope.client.AllowedCorsOrigins;
                    delete $scope.client.RedirectUris;
                    delete $scope.client.PostLogoutRedirectUris;
                    delete $scope.client.AllowAccessTokensViaBrowser;
            }
            if ($scope.progression < 1)
                $scope.progression = 1;
        }

        $scope.generateSecret = function (e) {
            e.preventDefault();
            $scope.client.ClientSecrets = [];
            const secretString = sha256(Math.random().toString(36).replace(/[^a-z]+/g, '').substr(0, 5));
            const secretData = {
                Value: secretString,
                Description: `Secret for ${$scope.client.ClientId}`
            };
            $scope.client.ClientSecrets.push(secretData);
        }

        $scope.clipboardText = function (str) {
            const el = document.createElement('textarea');
            el.value = str;
            document.body.appendChild(el);
            el.select();
            document.execCommand('copy');
            document.body.removeChild(el);
        }

        $scope.addOrRemoveScope = function (scope) {
            const indexScope = $scope.client.AllowedScopes.findIndex(function (s) {
                return s.Scope == scope.Scope;
            });
            if (indexScope > -1)
                $scope.client.AllowedScopes.splice(indexScope, 1);
            else $scope.client.AllowedScopes.push(scope);
        }

        $scope.shouldAdvanceFinal = function (e) {
            e.preventDefault();
            if ($scope.client.AllowedScopes.length == 0)
                return;

            if ($scope.progression < 3)
                $scope.progression = 3;
            $('#review-tab').get(0).click();
        }

        $scope.create = function (e) {
            e.preventDefault();
            $http.post('New', $scope.client, {
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
        }

        function getScopes() {
            $http.get('/api/scopes').then(function (results) {
                $scope.resources = results.data;
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

        getScopes();
    }]);