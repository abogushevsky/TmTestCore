angular.module("app.controllers").controller("tasksListCtrl", [
    "$scope", "$rootScope", "$state", "tasksService", "webSocketService",
    function ($scope, $rootScope, $state, tasksService, webSocketService) {
        $scope.tasksList = [];

        $scope.init = function() {
            tasksService.getList().then(function(data) { $scope.tasksList = data });
        }

        $scope.init();

        $scope.getCatName = function(name) {
            if (!name || name == "undefined")
                return "Без категории";

            return name;
        };

        $scope.editTask = function(id) {
            $state.go("task", { id: id });
        };

        $rootScope.$on("taskUpdated",
            function(event, taskChangeDetails) {
                console.log(taskChangeDetails);
                switch (taskChangeDetails.ChangeType) {
                    case 0:
                        $scope.tasksList.push(taskChangeDetails.Task);
                        break;
                    case 1:
                        for (var i = 0; i < $scope.tasksList.length; i++) {
                            if ($scope.tasksList[i].Id == taskChangeDetails.Task.Id) {
                                $scope.tasksList[i] = taskChangeDetails.Task;
                                break;
                            }
                        }
                        break;
                    case 2:
                        //TODO: удалить из массива $scope.tasksList;
                        break;
                }
                $scope.$apply();
            });
    }
]);