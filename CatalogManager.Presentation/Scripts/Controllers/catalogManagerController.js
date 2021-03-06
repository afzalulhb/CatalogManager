﻿'use strict';

var catalogManager = angular.module('catalogManager', ['catalogManager.catalogManagerService', 'ui.router', 'ngRoute', 'ngResource', 'ui.bootstrap']);

catalogManager.controller('CatalogManagerController', ['$scope', '$rootScope', 'CMApi', '$http', '$location', '$state', 'SyncApi', function ($scope, $rootScope, CMApi, $http, $location, $state, SyncApi) {

    // Controller code
    $scope.cm = {};
    $scope.cm.categories = [];
    $scope.menuitems = [];
    $scope.cm.dataLoading = true;



    $scope.init = function () {
        $scope.cm.dataLoading = true;
            refreshMenu();
        };

    var refreshMenu = function () {        

        $scope.menuitems = [];
        if (CMApi.Menu.query().$promise.then(function (data) {

            $scope.cm.dataLoading = false;
                     if (angular.isArray(data)) {
                            $scope.menuitems = data;
                    }

        }));

    };


    $scope.init();

    $rootScope.$on('Update', function (event) {
        refreshMenu();
    })
  

    $scope.LinkFor = function (item) {
        return item.ChildCategories ? '#/cat/' + item.Id : '#/cat/subcat/' + item.ParentCategoryId + '/' + item.Id;
    };

  
 
}]);