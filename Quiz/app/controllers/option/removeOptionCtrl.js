var app = angular.module('QuizApp');

app.controller('optionDeleteCtrl', optionDeleteCtrl);
optionDeleteCtrl.$inject = ['$http', '$scope', '$rootScope', '$routeParams', '$location', 'optionService'];

function optionDeleteCtrl($http, $scope, $rootScope, $routeParams, $location, optionService) {

    $scope.deleteOption = deleteOption;
    var id = $routeParams.id;
    $scope.Option = {
        Id: "",
        Name: "",
        Amount: "",
        RequiredPercentage:""
    };

    $http.get('http://localhost:11117/QuizOption/' + $routeParams.id).success(function (response) {
        $scope.Option.Id = response.Id;
        $scope.Option.Name = response.Name;
        $scope.Option.Amount = response.Amount;
        $scope.Option.RequiredPercentage = response.RequiredPercentage;

    })

    function deleteOption() {
        optionService.deleteOption(id).then(function (response) {
            $rootScope.status = "deleted Successfully";
         //   $location.path('/categories');
        })
    };
};