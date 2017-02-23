var app = angular.module('QuizApp');

app.factory('accountService', accountService);
app.factory('Base64', function () {


    var keyStr = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=';

    return {
        encode: function (input) {
            var output = "";
            var chr1, chr2, chr3 = "";
            var enc1, enc2, enc3, enc4 = "";
            var i = 0;

            do {
                chr1 = input.charCodeAt(i++);
                chr2 = input.charCodeAt(i++);
                chr3 = input.charCodeAt(i++);

                enc1 = chr1 >> 2;
                enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
                enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
                enc4 = chr3 & 63;

                if (isNaN(chr2)) {
                    enc3 = enc4 = 64;
                } else if (isNaN(chr3)) {
                    enc4 = 64;
                }

                output = output +
                    keyStr.charAt(enc1) +
                    keyStr.charAt(enc2) +
                    keyStr.charAt(enc3) +
                    keyStr.charAt(enc4);
                chr1 = chr2 = chr3 = "";
                enc1 = enc2 = enc3 = enc4 = "";
            } while (i < input.length);

            return output;
        },

        decode: function (input) {
            var output = "";
            var chr1, chr2, chr3 = "";
            var enc1, enc2, enc3, enc4 = "";
            var i = 0;

            // remove all characters that are not A-Z, a-z, 0-9, +, /, or =
            var base64test = /[^A-Za-z0-9\+\/\=]/g;
            if (base64test.exec(input)) {
                window.alert("There were invalid base64 characters in the input text.\n" +
                    "Valid base64 characters are A-Z, a-z, 0-9, '+', '/',and '='\n" +
                    "Expect errors in decoding.");
            }
            input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");

            do {
                enc1 = keyStr.indexOf(input.charAt(i++));
                enc2 = keyStr.indexOf(input.charAt(i++));
                enc3 = keyStr.indexOf(input.charAt(i++));
                enc4 = keyStr.indexOf(input.charAt(i++));

                chr1 = (enc1 << 2) | (enc2 >> 4);
                chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
                chr3 = ((enc3 & 3) << 6) | enc4;

                output = output + String.fromCharCode(chr1);

                if (enc3 != 64) {
                    output = output + String.fromCharCode(chr2);
                }
                if (enc4 != 64) {
                    output = output + String.fromCharCode(chr3);
                }

                chr1 = chr2 = chr3 = "";
                enc1 = enc2 = enc3 = enc4 = "";

            } while (i < input.length);

            return output;
        }
    };

});

accountService.$inject = ['$http', '$rootScope', '$location', '$timeout', '$cookies', '$cookieStore', 'Base64'];

function accountService($http, $rootScope, $location, $timeout, $cookies, $cookieStore, Base64) {

    var service = {};

    service.signUp = signUp;
    service.login = login;
    service.logout = logout;

    return service;


    function signUp(signUpData) {
        return $http.post('http://localhost:11117/User/Create', signUpData).then(function (response) {
            return response;
        });

    };



    function login(username, password, callback) {
        ClearCredentials();
        $timeout(function () {
            var authdata = Base64.encode(username + ':' + password);

            $rootScope.globals = {
                currentUser: {
                    username: username,
                    authdata: authdata
                }
            };

            $http.defaults.headers.common['Authorization'] = 'Basic ' + authdata; // jshint ignore:line

            // $cookieStore.put('globals', $rootScope.globals);
            localStorage.setItem('globals', JSON.stringify($rootScope.globals));
            localStorage.setItem('authorData', authdata);
            // window.localStorage.setItem('Token', token);
            /* Use this for real authentication
             —--------------------------------------------*/
            var res = $http.post('http://localhost:11117/Login', authdata);

            res.success(function (data, status, headers, config) {

                localStorage.setItem('Token', headers('Token'));
                callback(data, status, headers, config);

            });
            res.error(function (data, status, headers, config) {

                callback(data, status, headers, config);
            });
        }, 1000);

    };

    function logout() {

        var token = localStorage.getItem('Token');
        return $http({
            url: 'http://localhost:11117/Logout',
            method: 'POST',
            headers: { 'Token': token }
        }).then(function (repsonse) {

            localStorage.removeItem('Token');
        });

    };

    function ClearCredentials() {
        $rootScope.globals = {};
        // $cookies.remove('globals');
        localStorage.removeItem('globals');
        $http.defaults.headers.common.Authorization = 'Basic';
        localStorage.removeItem('authorData');
    };



};