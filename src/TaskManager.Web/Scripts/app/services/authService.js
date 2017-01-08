angular.module("app.services").factory("authService", [
    "$http", "$q", "$timeout",
    function ($http, $q, $timeout) {
        return {
            login: function (userName, password) {
                var request = "grant_type=password&UserName=" + userName + "&Password=" + password;
                return $http.post("Token", request);
            },
            logout: function() {
                return $http.post("api/Account/Logout");
            },
            register: function(regModel) {
                return $http.post("api/Account/Register", regModel);
            },
            getCurrentUser: function() {
                return $http.get("api/Account/UserInfo")
                    .then(
                        function(resp) { return resp.data; },
                        function(err) { return $q.reject(err.data); }
                    );
            }
        }
    }
]);