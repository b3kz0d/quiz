(function () {
    'use strict';
    var app = angular.module('QuizApp');
    app.factory('roleService', roleService);

    roleService.$inject = ['$http'];
    function roleService($http) {
        var service = {};
        service.getAllRoles = getAllRoles;
        service.addRole = addRole;
        service.details = details;
        service.edit = edit;
        service.deleteRole = deleteRole;

        return service;

        

        function getAllRoles(response) {
            return $http.get('http://localhost:11117/Roles').success(response);
        };

        function addRole(response) {

            var token = localStorage.getItem('Token');

            return $http({
                url: 'http://localhost:11117/Role/Create',
                method: 'POST',
                data: response,
                headers: { 'Token': token }
            }).then(function (response) {
                return response;
            });
        };

        function details(id) {
            return $http.get('http://localhost:11117/Role/' + id).then(function (response) {
                return response;
            });
        };

        function edit(id, roleData) {
            return $http.put('http://localhost:11117/Role/Update/' + id, roleData).then(function (response) {
                return response;
            });
        };

        function deleteRole(id) {
            return $http.delete('http://localhost:11117/Role/Delete/' + id);
        };
    }
})();