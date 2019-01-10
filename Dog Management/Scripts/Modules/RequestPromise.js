webApp.factory('requestPromise', ['$http', '$q', 'notifier', function ($http, $q, notifier) {

    return function (data) {
        var httpPromise = $http(data);

        var deferred = $q.defer();
        httpPromise.then(function (result) {
            if (result.data.Messages.length > 0) {
                result.data.Messages.forEach(function (msg) {
                    notifier(msg.Body, msg.Type);
                });
            }
            deferred.resolve(result.data.Data);

        }, function () {
            console.log('error');
            deferred.reject(false);
        });
        return deferred.promise;
    };
}]);