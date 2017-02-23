(function () {
    'use strict';

    var app = angular.module('QuizApp');

    app.controller('optionListCtrl', optionListCtrl);

    optionListCtrl.$inject = ['$http', '$routeParams', '$scope', 'optionService'];
    function optionListCtrl($http, $routeParams, $scope, optionService) {

        $scope.Option = {
            Id: "",
            Name: "",
            Amount: "",
            RequiredPercentage: ""
        };
        //$scope.getRoles = getRoles;

        (function () {

            getOptions();

        })();

        function getOptions() {
            optionService.getAllOptions(function (response) {
                $scope.options = response;
            });
        }
    };
})();