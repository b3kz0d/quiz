(function () {
    'use strict';
    var app = angular.module('QuizApp');
    app.factory('questionService', questionService);

    questionService.$inject = ['$http', 'levelService', 'categoryService'];
    function questionService($http, levelService, categoryService) {
        var service = {};
        service.getAllQuestions = getAllQuestions;
        service.addQuestion = addQuestion;
        service.edit = edit;
        service.deleteLevel = deleteLevel;

        return service;

        function getAllQuestions(response) {
            return $http.get('http://localhost:11117/Questions').success(response);
        };

        function addQuestion(response) {

            var token = localStorage.getItem('Token');

            return $http({
                url: 'http://localhost:11117/Question/Create',
                method: 'POST',
                data: response,
                headers: { 'Token': token }
            }).then(function (response) {
                return response;
            });
        };

        function edit(id, questionData) {
            return $http.put('http://localhost:11117/Question/Update/' + id, questionData).then(function (response) {
                return response;
            });
        };

        function deleteLevel(id) {
            return $http.delete('http://localhost:11117/Question/Delete/' + id);
        };
    }
})();