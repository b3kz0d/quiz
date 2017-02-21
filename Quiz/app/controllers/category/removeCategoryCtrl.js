var app = angular.module('QuizApp');

app.controller('categoryDeleteCtrl', categoryDeleteCtrl);
categoryDeleteCtrl.$inject = ['$http', '$scope', '$rootScope', '$routeParams', '$location', 'categoryService'];

function categoryDeleteCtrl($http, $scope, $rootScope, $routeParams, $location, categoryService) {

    $scope.deleteCategory = deleteCategory;
    var id = $routeParams.id;
    $scope.Category = {
        Id: "",
        Name: "",
        Description:""
    };

    $http.get('http://localhost:11117/Category/' + $routeParams.id).success(function (response) {
        $scope.Category.Id = response.Id;
        $scope.Category.Name = response.Name;
        $scope.Category.Description = response.Description;
    })

    function deleteCategory() {
        categoryService.deleteCategory(id).then(function (response) {
            $rootScope.status = "deleted Successfully";
            $location.path('/categories');
        })
    };
};