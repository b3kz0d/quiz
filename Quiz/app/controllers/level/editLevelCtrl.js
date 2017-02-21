var app = angular.module('QuizApp');

app.controller('levelEditCtrl', levelEditCtrl);
levelEditCtrl.$inject = ['$http', '$scope', '$rootScope', '$routeParams', '$location', 'levelService'];

function levelEditCtrl($http, $scope, $rootScope, $routeParams, $location, levelService) {

    $scope.edit = edit;
    var id = $routeParams.id;
    $scope.Level = {
        Id: "",
        Name: "",
        Score:"",
        Description: ""
    };

    $http.get('http://localhost:11117/Level/' + $routeParams.id).success(function (response) {
        $scope.Level.Name = response.Name;
        $scope.Level.Score = response.Score;
        $scope.Level.Description = response.Description;
    })

    function edit() {
        return levelService.edit(id, $scope.Level).then(function (response) {
            $rootScope.status = "Edit Successfuly";
            $location.path('/levels');
        })
    };
};