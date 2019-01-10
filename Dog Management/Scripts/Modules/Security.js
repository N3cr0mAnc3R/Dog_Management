webApp.factory('security', ['$rootScope', '$location', 'requestPromise', function ($rootScope, $location, requestPromise) {
    function SecurityScope() {
        var self = this;
        self.user = null;
    }
    function SecurityService() {
        var self = this;
        self.scope = new SecurityScope();
        var navListOrganization = [
            { href: '/organization', Name: 'Профиль' },
            { href: '/eventsEditor', Name: 'Мероприятия' },
            { href: '/eventCreator', Name: 'Создать мероприятие' },
            { href: '/requestApproval', Name: 'Заявки' },
            { href: '/documentCreator', Name: 'Документы' },
            { href: '/logout', Name: 'Выйти' }
        ];
        var navListUser = [
            { href: '/user', Name: 'Профиль' },
            { href: '/dogList', Name: 'Мои собаки' },
            { href: '/addDog', Name: 'Добавить собаку' },
            { href: '/request', Name: 'Подать заявку' },
            { href: '/logout', Name: 'Выйти' }
        ];
        var navListAdmin = [
            { href: '/admin', Name: 'Профиль' },
            { href: '/registerRequestApproval', Name: 'Заявки на регистрауию' },
            { href: '/logout', Name: 'Выйти' }
        ];
        var calculateLinks = function () {
            $rootScope.navLinkList = [];
            if ($rootScope.checkUserInRoles('organization')) {
                $rootScope.navLinkList = navListOrganization;
            }
            else if ($rootScope.checkUserInRoles('admin')) {
                $rootScope.navLinkList = navListAdmin;
            }
            else if ($rootScope.checkUserInRoles('user')) {
                $rootScope.navLinkList = navListUser;
            }
        };
        self.login = function (login, password) {
            if ($rootScope.user !== null) {
                return;
            }
            requestPromise({
                method: 'POST',
                url: '/api/account/Login',
                data: {
                    Login: login,
                    Password: password
                }
            }).then(function (data) {
                userPromise = null;
                if (data !== null) {
                    $rootScope.user = data;
                    console.log(data);
                    $location.path('/profile');
                    calculateLinks();
                }
            });
        };
        self.register = function (login, password) {
            if ($rootScope.user !== null) return;
            requestPromise({
                method: "POST",
                url: "/api/account/register",
                data: {
                    Password: password,
                    Login: login
                }
            }).then(function (Data) {
                self.getCurrentUser();
            });
        };
        self.logout = function () {
            requestPromise({
                method: "POST",
                url: "/api/account/logout"
            }).then(function () {
                $rootScope.user = null;
            });
        };
        self.getCurrentUser = function () {
            if (!$rootScope.user || $rootScope.user === null) {
                requestPromise({
                    method: "POST",
                    url: "/api/account/GetCurrentUser"
                }).then(function (data) {
                    $rootScope.user = data;
                    calculateLinks();

                });
            }
        };
        self.isAuthorized = function () {
            return $rootScope.user ? true : false;
        };
    }

    var securityService = new SecurityService();
    // общая функция проверки наличия ролей у пользователя
    //#region Новое
    //Это позволит динамически менять пункты меню

    //#endregion
    securityService.getCurrentUser();

    return securityService;
}]);