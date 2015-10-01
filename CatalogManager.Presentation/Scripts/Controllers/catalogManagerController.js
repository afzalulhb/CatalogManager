'use strict';

var catalogManager = angular.module('catalogManager', ['catalogManagerApp.catalogManagerService']);

catalogManager.controller('CatalogManagerController', ['$scope','CMApi',  function ($scope, CMApi) {

    // Controller code
    $scope.cm = {};
    $scope.cm.categories = [];



    $scope.init = function () {

            if (CMApi.Categories.query().$promise.then(function (data) {
                $scope.cm.categories = data;

            }));
        };


    $scope.init();

}]);