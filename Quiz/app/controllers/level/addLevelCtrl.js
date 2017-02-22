(function () {
    'use strict';

    var app = angular.module('QuizApp');

    app.controller('addLevelCtrl', addLevelCtrl);

    addLevelCtrl.$inject = ['$http', '$routeParams', '$scope', 'levelService'];
    function addLevelCtrl($http, $routeParams, $scope, levelService) {

        $scope.Level = {
            Id: "",
            Name: "",
            Score: "",
            Description:""
        };

        $scope.add = add;
        function add() {
            levelService.addLevel($scope.Level).then(function (response) {
                $scope.Level = null;
                $location.path('/levels');
            }, function (response) {

                $scope.error = response;
            });
        }
    };

})();