angular.module("app.services").factory("tasksService", [
    "$http", "$q",
    function ($http, $q) {
        function handleSuccess(resp) {
            return resp.data;
        }

        function handleError(err) {
            console.log(err);
            return $q.reject(err.data);
        }

        return {
            getList: function() {
                return $http.get("api/tasks").then(
                    handleSuccess,
                    handleError);
            },
            getById: function(id) {
                return $http.get("api/tasks/" + id)
                    .then(
                        handleSuccess,
                        handleError
                    );
            },
            addTask: function(task) {
                return $http.post("api/tasks", task).then(handleSuccess, handleError);
            },
            updateTask: function(task) {
                return $http.put("api/tasks", task).then(handleSuccess, handleError);
            },
            deleteTask: function(id) {
                return $http.delete("api/tasks/" + id).then(
                    handleSuccess,
                    handleError);
            }
        }
    }
]);