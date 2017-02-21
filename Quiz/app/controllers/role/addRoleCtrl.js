(function () {
    'use strict';

    var app = angular.module('QuizApp');

    app.controller('addRoleCtrl', addroleCtrl);

    addroleCtrl.$inject = ['$http', '$routeParams', '$scope', 'roleService'];
    function addroleCtrl($http, $routeParams, $scope, roleService) {

        $scope.Role = {
            Id: "",
            Name: ""
        };

        $scope.add = add;
        function add() {
            roleService.addRole($scope.Role).then(function (response) {
                $scope.Role = null;

            }, function (response) {

                $scope.error = response;
            });
        }
    };

})();