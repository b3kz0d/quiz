(function () {
    'use strict';
    var app = angular.module('QuizApp');
    app.factory('categoryService', categoryService);

    categoryService.$inject = ['$http'];
    function categoryService($http) {
        var service = {};
        service.getAllCategories = getAllCategories;
        service.addCategory = addCategory;
        service.details = details;
        service.edit = edit;
        service.deleteCategory = deleteCategory;

        return service;



        function getAllCategories(response) {
            return $http.get('http://localhost:11117/Categories').success(response);
        };

        function addCategory(response) {

            var token = localStorage.getItem('Token');

            return $http({
                url: 'http://localhost:11117/Category/Create',
                method: 'POST',
                data: response,
                headers: { 'Token': token }
            }).then(function (response) {
                return response;
            });
        };

        function details(id) {
            return $http.get('http://localhost:11117/Category/' + id).then(function (response) {
                return response;
            });
        };

        function edit(id, categoryData) {
            return $http.put('http://localhost:11117/Category/Update/' + id, categoryData).then(function (response) {
                return response;
            });
        };

        function deleteCategory(id) {
            return $http.delete('http://localhost:11117/Category/Delete/' + id);
        };
    }
})();