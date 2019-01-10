webApp.controller('profileCtrl', ["$scope", "profileService", "tools", function ($scope, profileService, tools) {

    $scope.init = function () {
        profileService.getDogs().then(function (data) {
            $scope.dogs = data;
        });
        profileService.getDogCatalogs().then(function (data) {
            $scope.colors = data.Colors;
            $scope.breeds = data.Breeds;
        });
    };
    $scope.selectDog = function (Id) {
        $scope.selectedRow = $scope.selectedRow === Id ? undefined : Id;
    };
    $scope.edit = function () {
        profileService.getDogById($scope.dogs.findById($scope.selectedRow).Id).then(function (data) {
            $modalForm = $('#myModal');
            $modalForm.modal("show");
            $scope.selectedDog = data;
            $scope.selectedDog.DateOfBirth = new Date(data.DateOfBirth);
            console.log($scope.selectedDog);
        });
    };
    $scope.changeDogInfo = function () {
        profileService.changeDogInfo($scope.selectedDog).then(function (data) {
            $modalForm = $('#myModal');
            $modalForm.modal("hide");
        });
    };
    $scope.remove = function () {
        if (confirm('Вы уверены, что хотите удалить собаку?')) {
            console.log(1);
        }
    };
}]);