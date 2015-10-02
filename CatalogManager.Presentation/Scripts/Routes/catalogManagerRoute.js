//var catalogManager = angular.module('catalogManager', ['ngRoute']);

 //configure our routes
catalogManager.config(function ($routeProvider) {
    $routeProvider

        .when('/', {
            templateUrl: 'home.html',
            controller: 'mainController'
        })

        .when('/cat', {
            templateUrl: 'scripts/templates/category.html',
            controller: 'CategoryController'
        })


        .when('/cat/subcat', {
            templateUrl: 'scripts/templates/subcategory.html',
            controller: 'mainController'
        });
});

catalogManager.controller('mainController', function ($scope) {
    // create a message to display in our view
    $scope.message = 'Everyone come and see how good I look!';
});
