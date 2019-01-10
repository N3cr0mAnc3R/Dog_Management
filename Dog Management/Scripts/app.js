var webApp = angular.module("webApp", ["ngRoute", "ngMessages"]);
webApp.config(["$routeProvider", "$locationProvider",
    function ($routeProvider, $locationProvider) {
        $routeProvider.
            when('/', {
                templateUrl: 'template/news',
                controller: 'newsCtrl'
            }).
            when('/login', {
                templateUrl: 'template/account/login',
                controller: 'securityCtrl'
            }).
            when('/logout', {
                templateUrl: 'template/account/login',
                controller: 'securityCtrl'
            }).
            when('/register', {
                templateUrl: 'template/account/registration',
                controller: 'securityCtrl'
            }).
            when('/dogList', {
                templateUrl: 'template/profile/doglist',
                controller: 'profileCtrl'
            }).
            //when('/addDog', {
            //    templateUrl: 'template/app/addDog',
            //    controller: 'AddDogCtrl'
            //}).
            //when('/user', {
            //    templateUrl: 'template/app/user',
            //    controller: 'UserCtrl'
            //}).
            //when('/userProfile', {
            //    templateUrl: 'template/User/UserProfile',
            //    controller: 'UserCtrl'
            //}).
            //when('/judge', {
            //    templateUrl: 'template/app/judge',
            //    controller: 'JudgeCtrl'
            //}).
            //when('/organization', {
            //    templateUrl: 'template/app/organization',
            //    controller: 'OrganizationCtrl'
            //}).
            //when('/request', {
            //    templateUrl: 'template/app/request',
            //    controller: 'RequestCtrl'
            //}).
            //when('/requestApproval', {
            //    templateUrl: 'template/app/requestApproval',
            //    controller: 'RequestApprovalCtrl'
            //}).
            //when('/documentCreator', {
            //    templateUrl: 'template/app/documentCreator',
            //    controller: 'DocumentCreatorCtrl',
            //    resolve: {
            //        auth: function () { }
            //    }
            //}).
            //when('/eventCreator', {
            //    templateUrl: 'template/app/eventCreator',
            //    controller: 'EventCreatorCtrl',
            //    resolve: {
            //        access: function () {
            //            return ['developer', 'admin'];
            //        }
            //    }
            //}).
            //when('/eventsEditor', {
            //    templateUrl: 'template/app/eventsEditor',
            //    controller: 'EventsEditorCtrl'
            //}).
            otherwise({
                redirectTo: '/'
            });
        $locationProvider.html5Mode(true);
    }]);
webApp.run(['$rootScope', '$location', '$route', 'security', function ($rootScope, $location, $route, Security) {

    $rootScope.checkUserInRoles = function (roles) {
        if ($rootScope.user) {
            var checkRoles = [];
            // роли могут быть переданы как массивом, так и строкой, с разделителем ","
            if (typeof roles === "object" && roles instanceof Array) {
                checkRoles = roles;
            }
            else if (typeof roles === "string") {
                checkRoles = roles.split(",");
            }

            if (checkRoles.filter(function (role) { return $rootScope.user.Roles.indexOf(role.trim().toLowerCase()) !== -1; }).length > 0) {
                return true;
            }
        }
        return false;
    };
    $rootScope.CheckActiveNav = function (href) {
        if (href === $location.path()) return true;
        else return false;
    };
    $rootScope.$on('$locationChangeStart', function (event, next, current) {
        for (var i in $route.routes) {
            if (next.indexOf(i) !== -1) {
                if ($route.routes[i].resolve && $route.routes[i].resolve.auth) {
                    Security.getCurrentUserInfoLoadPromise().then(function (data) {
                        if (!$rootScope.user) {
                            $location.path('/login');
                        }
                        event.preventDefault();

                    });
                }
                else if ($route.routes[i].resolve && $route.routes[i].resolve.access) {
                    if (!Security.checkUserInRoles($route.routes[i].resolve.access())) {
                        $location.path('/');
                    }
                    event.preventDefault();
                }
            }
        }
        //}
    });
}]);