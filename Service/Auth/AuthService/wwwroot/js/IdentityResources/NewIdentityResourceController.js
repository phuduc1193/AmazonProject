angular.module('authServiceApp', [])
    .controller('NewIdentityResourceController', ['$http', '$scope', function ($http, $scope) {
        $scope.identity = {
            Name: "",
            DisplayName: "",
            Description: "",
            Emphasize: false,
            Required: false,
            UserClaims: []
        };

        $scope.create = function () {
            $http.post('New', $scope.identity, {
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
    }]);