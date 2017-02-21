(function () {
    'use strict';

    var app = angular.module('QuizApp');

    app.controller('addQuestionCtrl', addQuestionCtrl);

    addQuestionCtrl.$inject = ['$http', '$routeParams', '$scope', 'levelService'];
    function addQuestionCtrl($http, $routeParams, $scope, levelService) {

        $scope.Question = {
            Id: "",
            QuestionContent: "",
            CategoryId: "",
            QuestionLevelId: "",
            Answers:[]
        };

        $scope.add = add;
        function add() {
            levelService.addLevel($scope.Question).then(function (response) {
                $scope.Level = null;
                $location.path('/levels');
            }, function (response) {

                $scope.error = response;
            });
        }
    };

})();