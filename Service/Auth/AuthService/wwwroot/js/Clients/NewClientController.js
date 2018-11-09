angular.module('authServiceApp', [])
    .controller('NewClientController', ['$http', '$scope', function ($http, $scope) {
        $scope.activeTabIndex = 0;
        $scope.progression = 0;

        $scope.client = {
            Name: "",
            DisplayName: "",
            Description: "",
            AllowedGrantTypes: null,
            Secrets: [],
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
            if (!$scope.client.Name || !$scope.client.DisplayName || !$scope.client.Description)
                return;
            if ($scope.client.AllowedGrantTypes != 'Implicit' && $scope.client.Secrets.length == 0)
                return;
            if ($scope.client.AllowedGrantTypes != 'ClientCredentials' && !$scope.client.RedirectUris)
                return;

            if ($scope.progression < 2)
                $scope.progression = 2;
            $('#scope-tab').get(0).click();
        }

        $scope.getBreadcrumFieldClass = function (index) {
            return { 'active': $scope.activeTabIndex == index, 'completed': $scope.progression > index && $scope.activeTabIndex != index, 'muted not-allowed': $scope.progression < index };
        }

        $scope.setAllowedGrantTypes = function (type) {
            $scope.client.AllowedGrantTypes = type;
            if ($scope.progression < 1)
                $scope.progression = 1;
        }

        $scope.generateSecret = function (e) {
            e.preventDefault();
            $scope.client.Secrets = [];
            const secretString = sha256(Math.random().toString(36).replace(/[^a-z]+/g, '').substr(0, 5));
            const secretData = {
                Value: secretString,
                Description: `Secret for ${$scope.client.Name}`
            };
            $scope.client.Secrets.push(secretData);
        }

        $scope.clipboardText = function (str) {
            const el = document.createElement('textarea');
            el.value = str;
            document.body.appendChild(el);
            el.select();
            document.execCommand('copy');
            document.body.removeChild(el);
        }
    }]);