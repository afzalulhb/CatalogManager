'use strict';


catalogManager.controller('ProductController', ['$scope', 'CMApi', '$http', '$location', '$state', '$routeParams', function ($scope, CMApi, $http, $location, $state, $routeParams) {

    $scope.cm = {};
    $scope.cm.Product = {};
    $scope.cm.catid = null;
    $scope.cm.id = null;
    $scope.alerts = [];

    $scope.init = function () {
        $scope.alerts = [];
        $scope.cm.catid = $routeParams.catid;
        $scope.cm.id = $routeParams.id;
        if ($scope.cm.id) {
            CMApi.ProductById.query({ id: $scope.cm.id }).$promise.then(function (data) {
                $scope.cm.Product = data;
            })
        }
    }
    $scope.save = function () {
        $scope.cm.Product.CategoryId = $scope.cm.catid;
        $scope.clearAlert();
        var outcome = CMApi.Product.save($scope.cm.Product, function () {
            $scope.cm.Product.Id = outcome.Id;
            $scope.alerts.push({ type:'success', msg: 'Saved!', show: true });

        }, function error(err){

            $scope.alerts.push({ type: 'danger', msg: 'Error Saving!', show: true });

        });
    }
    $scope.update = function () {
        $scope.clearAlert();
        CMApi.Product.update($scope.cm.Product, function (outcome) {
            $location.path('/');

        }, function error(e) {

            $scope.alerts.push({ type: 'danger', msg: 'Error Updating!', show: true });
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
        CMApi.Product.remove({ id: $scope.cm.id }, function (outcome) {
            $location.path('/');

        }, function error(e) {

            $scope.alerts.push({ type: 'danger', msg: 'Error Deleting!', show: true });
        });
    }
    $scope.init();
}]);