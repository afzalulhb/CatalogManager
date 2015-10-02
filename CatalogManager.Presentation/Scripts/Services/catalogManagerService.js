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
            Menu: $resource(apiroot + '/category/hierarchy', {}, {
                query: { method: 'GET', isArray: true }
            }),
            Product: $resource(apiroot + '/product/:id', {}, {
                query: { method: 'GET', isArray: false },
                update: { method: 'PUT', isArray: false },
                save: { method: 'POST', isArray: false }
            }),
            ProductById: $resource(apiroot + '/product/byid/:id', {}, {
                query: { method: 'GET', params: { id: '@id' }, isArray: false }
            }),
            ProductByCategoryId: $resource(apiroot + '/product/bycategory/:categoryId', {}, {
                query: { method: 'GET', params: { categoryId: '@categoryId' }, isArray: true }
            })
        }
    }]);