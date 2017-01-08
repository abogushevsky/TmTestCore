angular.module("app.controllers").controller("authCtrl", [
    "$scope", "$rootScope", "$state", "authService",
    function ($scope, $rootScope, $state, authService) {
        $scope.userName = "";
        $scope.password = "";
        $scope.showError = false;
        $scope.errorText = "Не удалось войти в систему";

        authService.logout();

        $scope.login = function () {
            $scope.showError = false;
            authService.login($scope.userName, $scope.password).then(function () {
                $state.go("main");
            }, function(err) {
                $scope.errorText = err.data.error_description;
                $scope.showError = true;
            });
        }
    }
]);