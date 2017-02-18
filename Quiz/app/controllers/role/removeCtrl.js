var app = angular.module('QuizApp');

app.controller('roleDeleteCtrl', roleDeleteCtrl);
roleDeleteCtrl.$inject = ['$http', '$scope', '$rootScope', '$routeParams', '$location', 'roleService'];

function roleDeleteCtrl($http, $scope, $rootScope, $routeParams, $location, roleService) {

      $scope.deleteRole = deleteRole;
    var id = $routeParams.id;
    $scope.Role = {
        Id: "",
        Name: ""
    };

    $http.get('http://localhost:11117/Role/' + $routeParams.id).success(function (response) {
        $scope.Role.Id = response.Id;
        $scope.Role.Name = response.Name;
    })

    function deleteRole() {
            roleService.deleteRole(id).then(function (response) {
            $rootScope.status = "deleted Successfully";
            $location.path('/roles');
        })
    };
};