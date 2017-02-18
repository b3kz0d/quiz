var app = angular.module('QuizApp');

app.controller('loginCtrl', loginCtrl);
loginCtrl.$inject = ['$http', '$scope', '$location', 'accountService'];

function loginCtrl($http, $scope, $location, accountService) {

    $scope.User = {
        UserName: "",
        Password: ""
    };

    $scope.login = login;
    $scope.logout = logout;

    function login() {

        return accountService.login($scope.User.UserName, $scope.User.Password, function (data, status, headers, config) {


        });

    };

    function logout() {
        return accountService.logout().then(function (response) {
            $location.path('/roles');
            return console.log(response);
        });
    };
}