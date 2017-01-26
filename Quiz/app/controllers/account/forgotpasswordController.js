(function () {

    "use strict";

    var app = angular.module("QuizApp");

    //controllers
    app.controller("forgotpasswordController", forgotpasswordCtrl);

    //injections
    forgotpasswordCtrl.$inject = ["$rootScope", "$scope", "accountService", "utilityService", "noCAPTCHA"];

    function forgotpasswordCtrl($rootScope, $scope, accountService, utilityService, noCAPTCHA) {

        $scope.gRecaptchaResponse = "";

        $scope.forgotpasswordData = {
            email: "",
            callbackUrl: utilityService.getUrl("resetpassword")
        };

        $scope.$watch('gRecaptchaResponse', function () {
            $scope.expired = false;
        });

        $scope.expiredCallback = function expiredCallback() {
            $scope.expired = true;
        };

        $scope.forgotpassword = forgotpassword;

        // initialize your users data
        (function () {

            $rootScope.title = "Forgot your password?";

        })();

        function forgotpassword() {


            if ($.trim($scope.gRecaptchaResponse).length === 0) {

                $rootScope.error = "The captcha is required and can't be empty!";

                return;
            }

            if ($scope.expired == true) {

                $rootScope.error = "The captcha is not valid";

                return;
            }

            accountService.forgotpassword($scope.forgotpasswordData).then(function (response) {

                $rootScope.message = "Please check your email to reset your password.";
                $scope.forgotpasswordData = null;
                $scope.gRecaptchaResponse = "";

                utilityService.redirectTo("login");
            },
                 function (response) {
                     $rootScope.error = utilityService.throwErrors(response);
                 });
        }

    }

})();
