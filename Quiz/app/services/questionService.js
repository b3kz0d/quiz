(function () {
    'use strict';
    var app = angular.module('QuizApp');
    app.factory('questionService', questionService);

    questionService.$inject = ['$http'];
    function questionService($http) {
        var service = {};
        service.getAllLevels = getAllLevels;
        service.addQuestion = addQuestion;
        service.edit = edit;
        service.deleteLevel = deleteLevel;

        return service;



        function getAllLevels(response) {
            return $http.get('http://localhost:11117/Levels').success(response);
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

        function edit(id, levelData) {
            return $http.put('http://localhost:11117/Level/Update/' + id, levelData).then(function (response) {
                return response;
            });
        };

        function deleteLevel(id) {
            return $http.delete('http://localhost:11117/Level/Delete/' + id);
        };
    }
})();