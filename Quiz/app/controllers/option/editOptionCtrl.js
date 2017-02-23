var app = angular.module('QuizApp');

app.controller('optionEditCtrl', optionEditCtrl);
optionEditCtrl.$inject = ['$http', '$scope', '$rootScope', '$routeParams', '$location', 'optionService'];

function optionEditCtrl($http, $scope, $rootScope, $routeParams, $location, optionService) {

    $scope.edit = edit;
    var id = $routeParams.id;
    $scope.Option = {
        Id: "",
        Name: "",
        Amount: "",
        RequierdPercentage: ""
    };

    $http.get('http://localhost:11117/QuizOption/' + $routeParams.id).success(function (response) {
        $scope.Option.Name = response.Name;
        $scope.Option.Amount = response.Amount;
        $scope.Option.RequierdPercentage = response.RequierdPercentage;
    })

    function edit() {
        return optionService.edit(id, $scope.Option).then(function (response) {
            $rootScope.status = "Edit Successfuly";
           // $location.path('/levels');
        })
    };
};