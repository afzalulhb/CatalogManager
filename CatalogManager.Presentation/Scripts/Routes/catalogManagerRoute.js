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

        .when('/cat/:id?', {
            templateUrl: 'scripts/templates/category.html',
            controller: 'CategoryController'
        })


        .when('/cat/subcat/:parentid?/:id?', {
            templateUrl: 'scripts/templates/subcategory.html',
            controller: 'SubCategoryController'
        })


        .when('/product/:catid?/:id?', {
            templateUrl: 'scripts/templates/product.html',
            controller: 'ProductController'
        })
;
});
