angular.module("app.controllers").controller("taskCtrl", [
    "$scope", "$rootScope", "$state", "$stateParams", "tasksService", "categoriesService", "webSocketService",
    function ($scope, $rootScope, $state, $stateParams, tasksService, categoriesService, webSocketService) {
        $scope.task = {};
        $scope.categories = [];
        $scope.isNew = false;
        $scope.catControlVisible = false;
        $scope.newCatName = null;

        $scope.init = function() {
            var id = $stateParams.id;
            $scope.isNew = id == "new";

            //список категорий и сама задача загружаются синхронно друг относительно друга,
            //чтобы к моменту загрузки задачи все категории точно были загружены
            categoriesService.getList()
                .then(function(data) {
                        $scope.categories = data;
                        if (!$scope.isNew)
                            tasksService.getById(id)
                                .then(
                                    function(data) {
                                        $scope.task = data;
                                        $scope.task.DueDate = new Date(data.DueDate);
                                        console.log($scope.task);
                                    },
                                    function(err) {
                                        alert(err
                                            .Message
                                            ? err.Message
                                            : "Ошибка при получении задачи");
                                    });
                    },
                    function(err) {
                        alert(err.Message ? err.Message : "Ошибка при получении категорий");
                    });
        };

        $scope.init();

        $scope.setCatControlVisibility = function(isVisible) {
            $scope.catControlVisible = isVisible;
            if (!$scope.catControlVisible)
                $scope.newCatName = null;
        };

        $scope.addCategory = function() {
            if ($scope.newCatName && $scope.newCatName.length > 0) {
                categoriesService.addCategory({ Name: $scope.newCatName })
                    .then(
                        function (data) {
                            categoriesService.getById(data)
                                .then(function(data) {
                                    $scope.categories.push(data);
                                    $scope.catControlVisible = false;
                                    $scope.newCatName = null;
                                }, function (err) { });
                        },
                        function (err) { alert(err.Message ? err.Message : "Не удалось сохранить категорию"); }
                    );
            }
        };

        $scope.onSave = function () {
            if ($scope.isNew) {
                tasksService.addTask($scope.task)
                    .then(
                        function(data) { $state.go("main"); },
                        function(err) { alert(err.Message ? err.Message : "Не удалось сохранить задачу"); }
                    );
            } else {
                tasksService.updateTask($scope.task)
                    .then(
                        function (data) { $state.go("main"); },
                        function (err) { alert(err.Message ? err.Message : "Не удалось отредактировать задачу"); }
                    );
            }
        };

        $rootScope.$on("taskUpdated",
            function(event, taskChangeDetails) {
                if (taskChangeDetails.ChangeType == 1 && taskChangeDetails.Task.Id == $scope.task.Id) {
                    $scope.task = taskChangeDetails.Task;
                    $scope.DueDate = new Date(taskChangeDetails.Task.DueDate);
                    $scope.$apply();
                }
            });
    }
]);