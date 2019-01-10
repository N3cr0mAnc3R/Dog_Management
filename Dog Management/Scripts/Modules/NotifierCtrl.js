webApp.controller("notifierCtrl", [
    "$scope",
    "systemNotifier",
    function (
        $scope,
        notifierConstructor
    ) {
        $scope.notification = notifierConstructor.getScope();

        $scope.loaded = true;
    }
]);