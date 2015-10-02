'use strict';

var catalogManager = angular.module('catalogManager', ['catalogManager.catalogManagerService', 'ui.router', 'ngRoute', 'ngResource']);

catalogManager.controller('CatalogManagerController', ['$scope', 'CMApi', '$http', '$location', '$state', function ($scope, CMApi, $http, $location, $state) {

    // Controller code
    $scope.cm = {};
    $scope.cm.categories = [];



    $scope.init = function () {

            if (CMApi.Categories.query().$promise.then(function (data) {
                $scope.cm.categories = data;

            }));
        };


    $scope.init();

    $scope.LinkFor = function (item) {
        return item.ChildCategories ? '#/cat/subcat' : '#/cat';
    };

    function unhighlightParentMenu(item) {
        $(item).parents('ul.nav-second-level').prev().prev().addClass('unhighlight');
    }
    var setMenuItemStatus = function (item, status, fn) {
        if (!item.click) {
            item.click = function ($event) {
                if (item.link == $location.$$path) {
                    $state.transitionTo($state.current, {}, {
                        reload: true,
                        inherit: false,
                        notify: true
                    });
                }

                var target = $event.currentTarget || $event.target;
                unhighlightParentMenu(target);
            };
        }

        if (fn(item)) {
            item.active = status;
        }

        if (item.children && item.children.length) {
            angular.forEach(item.children, function (childItem) {
                setMenuItemStatus(childItem, status, fn);
            });
        }
    };

    $scope.setItemActive = function (item) {
        angular.forEach($scope.menuitems, function (i) {
            setMenuItemStatus(i, false, function () {
                return true;
            });
        });
        item.active = true;
    };

    $scope.mainMenuItemClick = function (item) {
        if (item.link)
            $scope.setItemActive(item);
    };

    $scope.$on('$stateChangeSuccess', function () {
        angular.forEach($scope.menuitems, function (i) {
            setMenuItemStatus(i, false, function () {
                return true;
            });
        });

        var itemFound = false;
        angular.forEach($scope.menuitems, function (item) {
            setMenuItemStatus(item, true, function (i) {
                if (itemFound) return false;
                else {
                    if (i.link == $location.$$path ||
                       (i.highlightByStartWith && $location.$$path.indexOf(i.link) == 0)) {
                        itemFound = true;
                        return true;
                    }
                }
                return false;
            });
        });
    });

    $scope.menuitems = [];
    if (CMApi.Menu.query().$promise.then(function (data) {
                 if (angular.isArray(data)) {
                        $scope.menuitems = data;
                        var itemFound = false;
                        angular.forEach($scope.menuitems, function (item) {
                            setMenuItemStatus(item, true, function (i) {
                                    if (itemFound) return false;
                                    else {
                                            if (i.link == $location.$$path ||
                                                (i.highlightByStartWith && $location.$$path.indexOf(i.link) == 0)) {
                                                itemFound = true;
                                                return true;
                                            }
                                        }
                                    return false;
                                    });
                            });
                }

            }));
 
}]);