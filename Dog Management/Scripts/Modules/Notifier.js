webApp.factory('notifier', ['systemNotifier', function (systemNotifier) {

    var notifier, messages = [];

    notifier = systemNotifier.getNotifier();

    var AddMessage = function (incomeMessages) {
        if (incomeMessages instanceof Array) {
            messages = incomeMessages;
        }
        else {
            messages.push(incomeMessages);
        }
        if (typeof notifier === "function") {
            notifier(messages);
            messages = [];
            //if (!$scope.$$phase) {
            //    $scope.$apply();
            //}
        }
    };

    constructor = function (message, type) {
        AddMessage({ Body: message, Type: type });
    };


    return constructor;
}]);