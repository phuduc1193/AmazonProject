angular.module('authServiceApp', [])
    .controller('EditIdentityResourceController', ['$http', '$scope', function ($http, $scope) {
        const data = JSON.parse($("#Data").val());

        if (data) {
            $scope.identity = {
                Id: data.Id,
                Enabled: data.Enabled,
                Name: data.Name,
                DisplayName: data.DisplayName,
                Discription: data.Discription,
                Required: data.Required,
                Emphasize: data.Emphasize,
                ShowInDiscoveryDocument: data.ShowInDiscoveryDocument,
                UserClaims: data.UserClaims || []
            };
        }

        $scope.edit = function () {
            $http.put('Edit', $scope.identity, {
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

        $scope.addOrRemoveClaim = function (claim) {
            const index = $scope.identity.UserClaims.findIndex(function (c) {
                return c.Type == claim.type;
            });
            if (index > -1)
                $scope.identity.UserClaims.splice(index, 1);
            else $scope.identity.UserClaims.push({ Type: claim.type });
        }

        function getClaims() {
            $http.get('/api/claims').then(function (results) {
                $scope.claims = results.data.sort(function (a, b) {
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

        $scope.shouldCheck = function (claim) {
            if (!$scope.identity.UserClaims || $scope.identity.UserClaims.length == 0)
                return false;
            return $scope.identity.UserClaims.filter(function (c) {
                return c.Type == claim.type;
            }).length > 0;
        }
    }]);