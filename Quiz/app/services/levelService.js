(function () {
    'use strict';
    var app = angular.module('QuizApp');
    app.factory('levelService', levelService);

    levelService.$inject = ['$http'];
    function levelService($http) {
        var service = {};
        service.getAllLevels = getAllLevels;
        service.addLevel = addLevel;
        service.edit = edit;
        service.deleteLevel = deleteLevel;

        return service;



        function getAllLevels(response) {
            return $http.get('http://localhost:11117/Levels').success(response);
        };

        function addLevel(response) {

            var token = localStorage.getItem('Token');

            return $http({
                url: 'http://localhost:11117/Level/Create',
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