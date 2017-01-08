angular.module("app.services").factory("categoriesService", [
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
                return $http.get("api/categories").then(
                    handleSuccess,
                    handleError);
            },
            getById: function(id) {
                return $http.get("api/categories/" + id)
                    .then(
                        handleSuccess,
                        handleError
                    );
            },
            addCategory: function(category) {
                return $http.post("api/categories", category).then(handleSuccess, handleError);
            },
            updateCategory: function (category) {
                return $http.put("api/categories", category).then(handleSuccess, handleError);
            },
            deleteCategory: function (id) {
                return $http.delete("api/categories/" + id).then(
                    handleSuccess,
                    handleError);
            }
        }
    }
]);