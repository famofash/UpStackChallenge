(function (angular) {
    'use strict';
    angular.module('app', ['ngComponentRouter', 'user',  'ngResource'])

        .config(function ($locationProvider) {
            $locationProvider.html5Mode(false);
        })

        .value('$routerRootComponent', 'app')
       
        .component('app', {
            
            $routeConfig: [
                { path: '/user/', name: 'User', component: 'user', useAsDefault: true },
                { path: '/user/', name: 'User', component: 'user', useAsDefault: true },
                
            ]
        });

})(window.angular);
