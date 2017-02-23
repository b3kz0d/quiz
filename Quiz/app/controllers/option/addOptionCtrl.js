(function () {
    'use strict';

    var app = angular.module('QuizApp');

    app.controller('addOptionCtrl', addOptionCtrl);

    addOptionCtrl.$inject = ['$http', '$routeParams', '$scope', 'optionService'];
    function addOptionCtrl($http, $routeParams, $scope, optionService) {

        $scope.Option = {
            Id: "",
            Name: "",
            Amount: "",
            RequiredPercentage: ""
        };

        $scope.add = add;
        function add() {
            optionService.addOption($scope.Option).then(function (response) {
                $scope.Option = null;
              //  $location.path('/levels');
            }, function (response) {

                $scope.error = response;
            });
        }
    };

})();