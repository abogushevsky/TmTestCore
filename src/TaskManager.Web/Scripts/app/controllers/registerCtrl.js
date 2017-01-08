angular.module("app.controllers").controller("registerCtrl", [
    "$scope", "$rootScope", "$state", "authService",
    function ($scope, $rootScope, $state, authService) {
        $scope.model = {
            Email: null,
            FirstName: null,
            LastName: null,
            Password: null,
            ConfirmPassword: null
        };

        $scope.showError = false;
        $scope.errorText = "Не удалось зарегистрироваться";
        $scope.errors = [];

        $scope.register = function () {
            $scope.showError = false;
            $scope.errors = [];
            authService.register($scope.model)
                .then(
                    function() { $state.go("main"); },
                    function (err) {
                        console.log(err);
                        var data = err.data;
                        if (data.error_description)
                            $scope.errorText = data.error_description;
                        if (data.ModelState) {
                            for (var propName in data.ModelState) {
                                if (data.ModelState.hasOwnProperty(propName)) {
                                    console.log(data.ModelState[propName]);
                                    var errorGroup = data.ModelState[propName];
                                    for (var i = 0; i < errorGroup.length; i++) {
                                        $scope.errors.push(errorGroup[i]);
                                    }
                                }
                            }
                        }
                        console.log($scope.errors);
                        $scope.showError = true;
                    });
        };
    }
]);