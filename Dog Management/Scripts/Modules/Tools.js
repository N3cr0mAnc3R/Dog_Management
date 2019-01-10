webApp.factory('tools', function () {
    copyProperties = function (sourceObject, targetObject, map) {
        if (typeof sourceObject !== "object" || typeof targetObject !== "object") {
            return;
        }

        for (var key in sourceObject) {
            if (sourceObject.hasOwnProperty(key) && (!(map instanceof Array) || map.indexOf(key) !== -1)) {
                targetObject[key] = sourceObject[key];
            }
        }
        return targetObject;
    };
    Array.prototype.remove = function (item) {
        var index = this.indexOf(item);
        if (index !== -1) {
            this.splice(index, 1);
        }
    };
    Array.prototype.removeById = function (Id) {
        var self = this;
        var obj;
        self.forEach(function (item) {
            if (item.Id === Id) {
                obj = item;
            }
        })
        var index = this.indexOf(obj);
        if (index !== -1) {
            this.splice(index, 1);
        }
    };
    Array.prototype.findById = function (Id) {
        var result = {};
        this.forEach(function (item) {
            if (item.Id === Id) {
                result = item;
            }
        })
        return result;
    };
    return {
        copyProperties: copyProperties

    };
});