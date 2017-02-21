var app = angular.module('QuizApp', ['ngRoute', 'ngCookies']);

app.config(['$routeProvider', function ($routeProvider) {

    $routeProvider

    .when('/login', {
        templateUrl: 'assets/pages/login.html',
        controller: 'loginCtrl',
    })
    .when('/signup', {
        templateUrl: 'views/account/signUp.html',
        controller: 'signupController',

    })

    .when('/roles', {      //Roles    route
        templateUrl: 'views/role/list.html',
        controller: 'roleCtrl',

    })
    .when('/new', {
        templateUrl: 'views/role/add.html',
        controller: 'addRoleCtrl',

    })
    .when('/delete/:id', {
        templateUrl: 'views/role/delete.html',
        controller: 'roleDeleteCtrl',

    })
    .when('/edit/:id', {
        templateUrl: 'views/role/edit.html',
        controller: 'roleEditCtrl',

    })
     
    .when('/categories', {            ////  category routes
        templateUrl: 'views/category/categoryList.html',
        controller: 'categoryListCtrl',
    })

    .when('/new/category', {
        templateUrl: 'views/category/addCategory.html',
        controller: 'addRoleCtrl',

    })
    .when('/category/delete/:id', {
        templateUrl: 'views/category/deleteCategory.html',
        controller: 'roleDeleteCtrl',

    })
    .when('/category/edit/:id', {
        templateUrl: 'views/category/editCategory.html',
        controller: 'categoryEditCtrl',

    })
    .when('/levels', {            ////    levels routes
        templateUrl: 'views/level/listLevel.html',
        controller: 'levelListCtrl',
    })

    .when('/new/level', {
        templateUrl: 'views/level/addLevel.html',
        controller: 'addLevelCtrl',

    })
    .when('/level/delete/:id', {
        templateUrl: 'views/level/removeLevel.html',
        controller: 'levelDeleteCtrl',

    })
    .when('/level/edit/:id', {
        templateUrl: 'views/level/editCategory.html',
        controller: 'levelEditCtrl',

    })

    .when('/401', {
        templateUrl: 'views/401.html',
        controller: 'loginCtrl',

    })
    .otherwise({ redirectTo: '/401' });

}]);

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
})
