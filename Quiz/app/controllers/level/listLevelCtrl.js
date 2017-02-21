(function () {
    'use strict';

    var app = angular.module('QuizApp');

    app.controller('levelListCtrl', levelListCtrl);

    levelListCtrl.$inject = ['$http', '$routeParams', '$scope', 'levelService'];
    function levelListCtrl($http, $routeParams, $scope, levelService) {

        $scope.Level = {
            Id: "",
            Name: "",
            Score:"",
            Description: ""
        };
        //$scope.getRoles = getRoles;

        (function () {

            getLevels();

        })();

        function getLevels() {
            levelService.getAllLevels(function (response) {
                $scope.levels = response;
            });
        }
    };
})();