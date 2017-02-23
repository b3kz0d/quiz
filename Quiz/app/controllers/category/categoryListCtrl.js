(function () {
    'use strict';

    var app = angular.module('QuizApp');

    app.controller('categoryListCtrl', categoryListCtrl);

    categoryListCtrl.$inject = ['$http', '$routeParams', '$scope', 'categoryService'];
    function categoryListCtrl($http, $routeParams, $scope, categoryService) {

        $scope.Category = {
            Id: "",
            Name: "",
            Description: ""
        };
        //$scope.getRoles = getRoles;

        (function () {

            getCategories();

        })();

        function getCategories() {
            categoryService.getAllCategories(function (response) {
                $scope.categories = response;
            });
        }
    };
})();