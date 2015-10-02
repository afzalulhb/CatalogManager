'use strict';


catalogManager.controller('ProductController', ['$scope', 'CMApi', '$http', '$location', '$state', '$routeParams', function ($scope, CMApi, $http, $location, $state, $routeParams) {

    $scope.cm = {};
    $scope.cm.Product = {};
    $scope.cm.catid = null;
    $scope.cm.id = null;

    $scope.init = function () {
        $scope.cm.catid = $routeParams.catid;
        $scope.cm.id = $routeParams.id;
        if ($scope.cm.id) {
            CMApi.ProductById.query({ id: $scope.cm.id }).$promise.then(function (data) {
                $scope.cm.Product = data;
            })
        }
    }
    $scope.save = function () {
        CMApi.Product.save($scope.cm.Product);
    }
    $scope.update = function () {
        CMApi.Product.update($scope.cm.Product);
    }

    $scope.submitForm = function () {
        if ($scope.cm.id) {
            $scope.update();
        }
        else {
            $scope.save();
        }
    }
    $scope.init();
}]);