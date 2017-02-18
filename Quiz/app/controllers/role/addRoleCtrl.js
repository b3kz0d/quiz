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

    //app.controller('detailsCtrl', detailsCtrl);
    //detailsCtrl.$inject = ['$scope', '$http', 'roleService', '$routeParams'];

    //function detailsCtrl($scope, $http, roleService, $routeParams) {
    //    var id = $routeParams.id;
    //    roleService.details(id).then(function (data) {
    //        $scope.roleDetails = data;
    //    })
    //};


})();