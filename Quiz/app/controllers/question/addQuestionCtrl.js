(function () {
    'use strict';

    var app = angular.module('QuizApp');

    app.controller('addQuestionCtrl', addQuestionCtrl);

    addQuestionCtrl.$inject = ['$http', '$routeParams', '$scope', 'questionService','levelService', 'categoryService'];
    function addQuestionCtrl($http, $routeParams, $scope, questionService, levelService, categoryService) {

        $scope.Question = {
            Id: "",
            QuestionContent: "",
            CategoryId: "",
            QuestionLevelId: "",
            Answers: [{
                AnswerContent: "",
                IsCorrect:"",
            }]
        };

        (function () {
            getLevel();

            getCategory();
        })()


        function getLevel() {
            levelService.getAllLevels(function (response) {
                $scope.levels = response;
            })
        }

        function getCategory() {
            categoryService.getAllCategories(function (response) {
                $scope.categories = response;
            })
        }

        $scope.add = add;
        function add() {
            questionService.addQuestion($scope.Question).then(function (response) {
               // $scope.Level = null;
                // $location.path('/levels');
            }, function (response) {

                $scope.error = response;
            });
        }
    };

})();