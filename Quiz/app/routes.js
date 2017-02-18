var app = angular.module('QuizApp', ['ngRoute','ngCookies']);

app.config(['$routeProvider', function ($routeProvider) {

    $routeProvider
    .when('/roles', {
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
    .when('/signup', {
        templateUrl: 'views/account/signUp.html',
        controller: 'signupController',

    })
    .when('/login', {
        templateUrl: 'views/account/login.html',
        controller: 'loginCtrl',

    })
    .otherwise({ redirectTo: '/login' });

}]);