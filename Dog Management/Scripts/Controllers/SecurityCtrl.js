webApp.controller("securityCtrl", function ($rootScope, $scope, $http, $location, security, notifier) {
    
    $scope.tryLogin = function (username, password) {
        security.login(username, password);
    };
    $scope.init = function () {
        if ($rootScope.user) {
            if ($location.path() === "/logout") {
                security.logout();
            }
            $location.path('/profile');
        }
    };
    $scope.register = function (username, password, confirm, email) {
        notifier('Тест', 'success');
        //if (password !== confirm) {

        //}
        //security.Register(username, password, email);
    };

});