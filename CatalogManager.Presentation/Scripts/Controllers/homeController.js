'use strict';


catalogManager.controller('HomeController', ['$scope', 'CMApi', '$http', '$location', '$state', '$routeParams', function ($scope, CMApi, $http, $location, $state, $routeParams) {

    // Controller code
    $scope.cm = {};
    $scope.CategoryList = [];

    $scope.tableCategoryColumns = [
			  { index: 0, type: 'text', name: 'Name', text: 'Name', style: 'col-sm-6' },
			  { index: 1, type: 'nav', name: 'Action', text: 'Action', style: 'col-sm-6' }
    ];

    $scope.init = function () {
        CMApi.Categories.query().$promise.then(function (data) {
            if ($scope.CategoryList) $scope.CategoryList.splice(0, $scope.CategoryList.length);
            $scope.CreateCategoryList(data);
        })
    };


    $scope.CreateCategoryList = function (data) {
        $scope.CategoryList.push.apply($scope.CategoryList, data);
        $scope.cvCategoryData = new wijmo.collections.CollectionView($scope.CategoryList);
    };
    $scope.init();
}]);