(function () {

    "use strict";

    var app = angular.module("QuizApp");

    //controllers
    app.controller("loginController", loginCtrl);

    //injections
    loginCtrl.$inject = ["$rootScope", "$scope", "utilityService", "accountService", "noCAPTCHA"];

    function loginCtrl($rootScope, $scope, utilityService, accountService, noCAPTCHA) {

        $scope.gRecaptchaResponse = "";

        $scope.loginData = {
            email: "",
            password: ""
        };

        $scope.$watch('gRecaptchaResponse', function () {
            $scope.expired = false;
        });

        $scope.expiredCallback = function expiredCallback() {
            $scope.expired = true;
        };

        $scope.login = login;

        // initialize your users data
        (function () {

            $rootScope.title = "Login";

        })();

        function login() {

            if ($.trim($scope.gRecaptchaResponse).length === 0) {

                $rootScope.error = "The captcha is required and can't be empty!";

                return;
            }

            if ($scope.expired == true) {

                $rootScope.error = "The captcha is not valid";

                return;
            }

            accountService.login($scope.loginData).then(function (response) {

                $scope.gRecaptchaResponse = "";

                utilityService.redirectTo("profile");

            },
                function (err) {

                    try {

                        var data = JSON.parse(err.error);

                        $scope.email = data.Email;
                        $scope.noconfirmed = true;
                        $rootScope.error = "User no confirmed.";

                    } catch (e) {
                        $rootScope.error = err.error;
                    }

                });
        }

    }

})();
