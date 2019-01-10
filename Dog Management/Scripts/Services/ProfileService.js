webApp.factory("profileService", ["requestPromise", function (requestPromise) {
    var service = function () {
        var self = this;
        self.getDogs = function () {
            return requestPromise({
                method: "post",
                url: "/api/profile/getdogs"
            });
        };
        self.getDogCatalogs = function () {
            return requestPromise({
                method: "post",
                url: "/api/profile/GetDogCatalogs"
            });
        };
        self.getDogById = function (dogId) {
            return requestPromise({
                method: "post",
                url: "/api/profile/getDogById",
                params: {
                    dogId: dogId
                }
            });
        };
        self.changeDogInfo = function (model) {
            console.log(model);
            return requestPromise({
                method: "post",
                url: "/api/profile/ChangeDogInfo",
                data: { model }

            });
        };

    };

    return new service();

}]);