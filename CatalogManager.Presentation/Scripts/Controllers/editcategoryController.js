'use strict';


catalogManager.controller('EditcategoryController', ['$scope', '$rootScope', 'CMApi', '$http', '$location', '$state', '$routeParams', 'SyncApi',
    function ($scope, $rootScope, CMApi, $http, $location, $state, $routeParams, SyncApi) {

    $scope.cm = {};
    $scope.cm.Category = {};
    $scope.cm.parentid = null;
    $scope.cm.id = null;
    $scope.alerts = [];

    $scope.init = function () {
        $scope.alerts = [];
        $scope.cm.parentid = $routeParams.parentid;
        $scope.cm.id = $routeParams.id;
        if ($scope.cm.id) {
            CMApi.CategoryById.query({ id: $scope.cm.id }).$promise.then(function (data) {
                $scope.cm.Category = data;
            })
        }
    }
    $scope.save = function () {
        $scope.cm.Category.ParentCategoryId = $scope.cm.parentid;
        $scope.clearAlert();
        var outcome = CMApi.Category.save($scope.cm.Category, function () {
            $scope.cm.Category.Id = outcome.Id;
            $scope.cm.id = outcome.Id;
            $scope.alerts.push({ type: 'success', msg: 'Saved!', show: true });
            Sync();

        }, function error(err){

            $scope.alerts.push({ type: 'danger', msg: 'Error Saving!', show: true });

        });
    }
    $scope.update = function () {
        $scope.clearAlert();
        CMApi.Category.update($scope.cm.Category, function (outcome) {
            $location.path('/');
            Sync();

        }, function error(e) {

            $scope.alerts.push({ type: 'danger', msg: 'Error Saving!', show: true });
        });
    }

    $scope.submitForm = function () {
        if ($scope.cm.id) {
            $scope.update();
        }
        else {
            $scope.save();
        }
    }
    $scope.closeAlert = function (index) {
        $scope.alerts.splice(index, 1);
    };
    $scope.clearAlert = function () {
        if ($scope.alerts) $scope.alerts.splice(0, $scope.alerts.length);
    }

    $scope.cancel = function () {
        $location.path('/');
    }
    $scope.delete = function () {
        $scope.clearAlert();
        CMApi.Category.remove({ id: $scope.cm.id }, function (outcome) {
            Sync();
            $location.path('/');

        }, function error(e) {

            $scope.alerts.push({ type: 'danger', msg: 'Error Saving!', show: true });
        });
    }
    
    function Sync() {
        $rootScope.$broadcast('Update');
    };
    $scope.init();
}]);