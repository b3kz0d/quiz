(function () {
    'use strict';

    var app = angular.module('QuizApp');

    app.controller('addCategoryCtrl', addCategoryCtrl);

    addCategoryCtrl.$inject = ['$http', '$routeParams', '$scope', 'categoryService'];
    function addCategoryCtrl($http, $routeParams, $scope, categoryService) {

        $scope.Category = {
            Id: "",
            Name: "",
            Description: ""
        };

        $scope.add = add;
        function add() {
            categoryService.addCategory($scope.Category).then(function (response) {
                $scope.Category = null;
                $location.path('/categories');
            }, function (response) {

                $scope.error = response;
            });
        }
    };

})();