(function () {

    "use strict";

    var app = angular.module('QuizApp');

    //controllers
    app.controller('signupController', signupCtrl);

    //injections
    signupCtrl.$inject = ['$rootScope', '$scope', 'accountService'];

    function signupCtrl($rootScope, $scope, accountService) {

        

        $scope.registrationData = {
            RoleId: "",
            Name:"",
            Username: "",
            Password: "",
              
        };
                
        $scope.signup = signup;

        // initialize your users data
                  

        function signup() {

            accountService.signUp($scope.registrationData).then(function (response) {

                $rootScope.message = "An email has been sent to your account.Please view the email and confirm your account to complete the registration process.";

                $scope.registrationData = null;
                

               // utilityService.redirectTo("login");
            }, function (response) {
                
            });
        }

    }

})();
