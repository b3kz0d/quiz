(function () {
    'use strict';

    var app = angular.module('QuizApp');

    app.controller('questionListCtrl', questionListCtrl);

    questionListCtrl.$inject = ['$http', '$routeParams', '$scope', 'questionService'];
    function questionListCtrl($http, $routeParams, $scope, questionService) {

        $scope.Question = {
            Id: "",
            QuestionContent: "",
            
        };
        //$scope.getRoles = getRoles;

        (function () {

            getQuestions();

        })();

        function getQuestions() {
            questionService.getAllQuestions(function (response) {
                $scope.questions = response;
            });
        }
    };
})();