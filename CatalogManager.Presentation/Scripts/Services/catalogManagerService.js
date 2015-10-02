﻿angular.module('catalogManager.catalogManagerService', ['ngResource']).
    factory('CMApi', ['$resource', function ($resource) {
        //api path
        var apiroot = "http://localhost:28742";
        return {

            Categories: $resource(apiroot + '/category/top', {}, {
                query: { method: 'GET', isArray: true }
            }),
            Menu: $resource(apiroot + '/category/hierarchy', {}, {
                query: { method: 'GET', isArray: true }
            })
        }
    }]);