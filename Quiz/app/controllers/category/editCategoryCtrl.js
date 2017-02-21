var app = angular.module('QuizApp');

app.controller('categoryEditCtrl', categoryEditCtrl);
categoryEditCtrl.$inject = ['$http', '$scope', '$rootScope', '$routeParams', '$location', 'categoryService'];

function categoryEditCtrl($http, $scope, $rootScope, $routeParams, $location, categoryService) {

    $scope.edit = edit;
    var id = $routeParams.id;
    $scope.Category = {
        Id: "",
        Name: "",
        Description:""
    };

    $http.get('http://localhost:11117/Category/' + $routeParams.id).success(function (response) {
        $scope.Category.Name = response.Name;
        $scope.Category.Description = response.Description;
    })

    function edit() {
        return categoryService.edit(id, $scope.Category).then(function (response) {
            $rootScope.status = "Edit Successfuly";
            $location.path('/categories');
        })
    };
};