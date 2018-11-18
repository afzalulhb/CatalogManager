angular.module('catalogManager.catalogManagerService', ['ngResource']).
    factory('CMApi', ['$resource', function ($resource) {
        //api path
        var apiroot = "http://localhost:28742";
        return {

            Categories: $resource(apiroot + '/category/top', {}, {
                query: { method: 'GET', isArray: true }
            }),
            CategoriesByParentId: $resource(apiroot + '/category/byparent/:id', {}, {
                query: { method: 'GET', params: { id: '@id' }, isArray: true }
            }),
            CategoryById: $resource(apiroot + '/category/byid/:id', {}, {
                query: { method: 'GET', params: { id: '@id' }, isArray: false }
            }),
            Menu: $resource(apiroot + '/category/hierarchy', {}, {
                query: { method: 'GET', isArray: true }
            }),
            Product: $resource(apiroot + '/product/:id', {}, {
                query: { method: 'GET', isArray: false },
                update: { method: 'PUT', isArray: false },
                remove: { method: 'DELETE', params: { id: '@id' }, isArray: false },
                save: { method: 'POST', isArray: false }
            }),
            Category: $resource(apiroot + '/category/:id', {}, {
                update: { method: 'PUT', isArray: false },
                remove: { method: 'DELETE', params: { id: '@id' }, isArray: false },
                save: { method: 'POST', isArray: false }
            }),
            ProductById: $resource(apiroot + '/product/byid/:id', {}, {
                query: { method: 'GET', params: { id: '@id' }, isArray: false }
            }),
            ProductByCategoryId: $resource(apiroot + '/product/bycategory/:categoryId', {}, {
                query: { method: 'GET', params: { categoryId: '@categoryId' }, isArray: true }
            })
        };
    }]).
factory('SyncApi', function () {
    var number = 1;
    function getNumber() {
        return number;
    }
    function setNumber(newNumber) {
        number = newNumber;
    }
    return {
        getNumber: getNumber,
        setNumber: setNumber
    };
});