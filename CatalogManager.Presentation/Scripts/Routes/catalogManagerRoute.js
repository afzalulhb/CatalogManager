//var catalogManager = angular.module('catalogManager', ['ngRoute']);

 //configure our routes
catalogManager.config(function ($routeProvider) {
    $routeProvider

        .when('/', {
            templateUrl: 'scripts/templates/home.html',
            controller: 'HomeController'
        })



        .when('/cat/subcat/:parentid?/:id?', {
            templateUrl: 'scripts/templates/subcategory.html',
            controller: 'SubcategoryController'
        })

        .when('/cat/new', {
            templateUrl: 'scripts/templates/editcategory.html',
            controller: 'EditcategoryController'
        })

        .when('/cat/edit/:parentid?/:id?', {
            templateUrl: 'scripts/templates/editcategory.html',
            controller: 'EditcategoryController'
        })
        .when('/cat', {
            templateUrl: 'scripts/templates/category.html',
            controller: 'CategoryController'
        })

        .when('/cat/:id?', {
            templateUrl: 'scripts/templates/category.html',
            controller: 'CategoryController'
        })

        .when('/product/:catid?/:id?', {
            templateUrl: 'scripts/templates/product.html',
            controller: 'ProductController'
        })
;
});
