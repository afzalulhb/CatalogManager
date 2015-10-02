'use strict';


catalogManager.controller('CategoryController', ['$scope', 'CMApi', '$http', '$location', '$state', '$routeParams', function ($scope, CMApi, $http, $location, $state, $routeParams) {

    // Controller code
    $scope.cm = {};
    $scope.cm.categories = [];
    $scope.CategoryList = [];
    $scope.ProductList = [];
    $scope.cm.id;

    $scope.tableProductColumns = [
             { index: 0, type: 'text', name: 'Name', text: 'Name', style: 'col-sm-1' },
             { index: 1, type: 'text', name: 'Description', text: 'Description', style: 'col-sm-1' },
             { index: 2, type: 'currency', name: 'Price', text: 'Price', style: 'col-sm-1' },
             { index: 5, type: 'nav', name: 'Action', text: 'Action', style: 'col-sm-5' }
    ];

    $scope.tableCategoryColumns = [
			  { index: 0, type: 'text', name: 'Name', text: 'Name', style: 'col-sm-1' },
			  { index: 5, type: 'nav', name: 'Action', text: 'Action', style: 'col-sm-5' }
    ];

    $scope.init = function () {
        $scope.cm.id = $routeParams.id;
        CMApi.CategoriesByParentId.query({ id: $scope.cm.id }).$promise.then(function (data) {
            if ($scope.CategoryList) $scope.CategoryList.splice(0, $scope.CategoryList.length);
            $scope.CreateCategoryList(data);
        })
        CMApi.ProductByCategoryId.query({ categoryId: $scope.cm.id }).$promise.then(function (data) {
            if ($scope.ProductList) $scope.ProductList.splice(0, $scope.ProductList.length);
            $scope.CreateProductList(data);
        })
    };


    $scope.CreateCategoryList = function (data) {
        $scope.CategoryList.push.apply($scope.CategoryList, data);
        $scope.cvCategoryData = new wijmo.collections.CollectionView($scope.CategoryList);
    };
    $scope.CreateProductList = function (data) {
        $scope.ProductList.push.apply($scope.ProductList, data);
        $scope.cvProductData = new wijmo.collections.CollectionView($scope.ProductList);
    };

    $scope.init();
}]);