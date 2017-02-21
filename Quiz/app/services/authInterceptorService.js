var app = angular.module('QuizApp');

app.factory('authInterceptorService', authInterceptorService);

authInterceptorService.$inject = ['$q', '$location'];

function authInterceptorService($q, $location) {
    var authservice = {};

    function request(config) {
        config.headers = config.headers || {};

        var token = localStorage.getItem('Token');
        var authdata = localStorage.getItem('authorData');
        if (authdata) {
            config.headers['Token'] = token
            config.headers.Authorization ='Basic '+ authdata;
            console.log(config.headers.Authorization);
            console.log(config.headers);
            console.log('Token bilan  '+JSON.stringify(config.headers));
        }   
        return config;
    }

    function response(rejection) {
        console.log(rejection.status);
        if (rejection.status === 401) {
            $location.path('/401');
        }
        if (rejection.status === 404) {
            $location.path('/404');
        }
        return $q.reject(rejection);
    }

    authservice.request = request;
    authservice.responseError = response;

    return authservice;
};