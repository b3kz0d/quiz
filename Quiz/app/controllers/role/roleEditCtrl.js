var app = angular.module('QuizApp');

app.controller('roleEditCtrl', roleEditCtrl);
roleEditCtrl.$inject = ['$http', '$scope', '$rootScope', '$routeParams','$location', 'roleService'];

function roleEditCtrl($http, $scope, $rootScope, $routeParams,$location, roleService) {

    $scope.edit = edit;
    var id = $routeParams.id;
    $scope.Role = {
        Id: "",
        Name: ""
    };

    $http.get('http://localhost:11117/Role/' + $routeParams.id).success(function (response) {
        $scope.Role.Name = response.Name;
    })

    function edit() {
        return roleService.edit(id, $scope.Role).then(function (response) {
            $rootScope.status = "Edit Successfuly";
            $location.path('/roles');
        })
    };
};