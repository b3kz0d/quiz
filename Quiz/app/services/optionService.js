(function () {
    'use strict';
    var app = angular.module('QuizApp');
    app.factory('optionService', optionService);

    optionService.$inject = ['$http'];
    function optionService($http) {
        var service = {};
        service.getAllOptions = getAllOptions;
        service.addOption = addOption;
        service.edit = edit;
        service.deleteOption = deleteOption;

        return service;



        function getAllOptions(response) {
            return $http.get('http://localhost:11117/QuizOptions').success(response);
        };

        function addOption(response) {

            var token = localStorage.getItem('Token');

            return $http({
                url: 'http://localhost:11117/QuizOption/Create',
                method: 'POST',
                data: response,
                headers: { 'Token': token }
            }).then(function (response) {
                return response;
            });
        };

        function edit(id, optionData) {
            return $http.put('http://localhost:11117/QuizOption/Update/' + id, optionData).then(function (response) {
                return response;
            });
        };

        function deleteOption(id) {
            return $http.delete('http://localhost:11117/QuizOption/Delete/' + id);
        };
    }
})();