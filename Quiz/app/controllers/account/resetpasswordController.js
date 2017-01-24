(function () {

    "use strict";

    var app = angular.module("QuizApp");

    //controllers
    app.controller("resetpasswordController", resetpasswordCtrl);

    //injections
    resetpasswordCtrl.$inject = ["$rootScope", "$scope", "$stateParams", "accountService", "utilityService", "noCAPTCHA"];

    function resetpasswordCtrl($rootScope, $scope, $stateParams, accountService, utilityService, noCAPTCHA) {

        $scope.gRecaptchaResponse = "";

        $scope.resetpasswordData = {
            email: "",
            token: $stateParams.token,
            newpassword: "",
            confirmpassword: ""
        };

        $scope.$watch('gRecaptchaResponse', function () {
            $scope.expired = false;
        });

        $scope.expiredCallback = function expiredCallback() {
            $scope.expired = true;
        };

        $scope.resetpassword = resetpassword;

        // initialize your users data
        (function () {

            $rootScope.title = "Reset password";

        })();

        function resetpassword() {

            if ($.trim($scope.gRecaptchaResponse).length === 0) {

                $rootScope.error = "The captcha is required and can't be empty!";

                return;
            }

            if ($scope.expired == true) {

                $rootScope.error = "The captcha is not valid";

                return;
            }

            accountService.resetpassword($scope.resetpasswordData).then(function (response) {

                $rootScope.message = "Your password has been reset.";
                $scope.confirmData = null;
                $scope.gRecaptchaResponse = "";

                utilityService.redirectTo("login");
            },
                function (response) {
                    $rootScope.error = utilityService.throwErrors(response);
                });
        }

    }

})();
