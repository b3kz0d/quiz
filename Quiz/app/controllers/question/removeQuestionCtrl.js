var app = angular.module('QuizApp');

app.controller('questionDeleteCtrl', questionDeleteCtrl);
questionDeleteCtrl.$inject = ['$http', '$scope', '$rootScope', '$routeParams', '$location', 'questionService', 'levelService', 'categoryService'];

function questionDeleteCtrl($http, $scope, $rootScope, $routeParams, $location, questionService, levelService, categoryService) {

    $scope.deleteQuestion = deleteQuestion;
    var id = $routeParams.id;
    $scope.Question = {
        Id: "",
        QuestionContent: "",
        CategoryId: "",
        QuestionLevelId: "",
        Answers: [{
            AnswerContent: "",
            IsCorrect: "",
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

    $http.get('http://localhost:11117/Question/' + $routeParams.id).success(function (response) {
        $scope.Question.QuestionContent = response.QuestionContent;
        $scope.Question.CategoryId = response.CategoryId;
        $scope.Question.QuestionLevelId = response.QuestionLevelId;
        $scope.Question.Answers = response.Answers;
    })

    function deleteQuestion() {
        return questionService.deleteQuestion(id).then(function (response) {
            // $rootScope.status = "Edit Successfuly";
            //  $location.path('/levels');
        })
    };
};