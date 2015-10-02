angular.module('catalogManager.uiDirectives', ['ngResource', 'wj'])
     .factory('uiUtilities', [function () {

         var _generateUuid = function () {
             var d = new Date().getTime();
             var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                 var r = (d + Math.random() * 16) % 16 | 0;
                 d = Math.floor(d / 16);
                 return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
             });
             return uuid;
         };

         var _lookupItem = function (collection, fieldname, value) {
             if (collection) {
                 for (var i = 0; i < collection.length; i++) {
                     if (collection[i][fieldname] == value) {
                         return collection[i];
                     }
                 }
             }
             return null;
         };
         var _removeItem = function (collection, fieldname, value) {
             if (collection) {
                 for (var i = 0; i < collection.length; i++) {
                     if (collection[i][fieldname] == value) {
                         collection.splice(i, 1);
                         break;
                     }
                 }
             }
         };
         var _selectItems = function (collection, selections, fieldname1, fieldname2) {
             if (collection && selections) {
                 for (var i = 0; i < selections.length; i++) {
                     for (var j = 0; j < collection.length; j++) {
                         var val2 = (fieldname2 != null) ? selections[i][fieldname2] : selections[i];
                         var val1 = (fieldname1 != null) ? collection[j][fieldname1] : collection[j];
                         if (val1 == val2) {
                             collection[j].selected = true;
                             break;
                         }
                     }
                 }
             }
         };
         var _collectSelections = function (collection, fieldname) {
             var selections = [];
             if (collection) {
                 for (var i = 0; i < collection.length; i++) {
                     if (collection[i].selected) {
                         if (fieldname) selections.push(collection[i][fieldname]);
                         else selections.push(collection[i]);
                     }
                 }
             }
             return selections;
         };
         var _clearSelections = function (collection) {
             collection.forEach(function (item) {
                 item.selected = false;
             });
         };
         var _resolvePath = function (item, path) {
             var obj = item;
             if (path) {
                 if (path.indexOf('.') > 0) {
                     var parts = path.split('.');
                     for (var i = 0; i < parts.length; i++) {
                         var i0 = parts[i].indexOf('[') + 1;
                         var i1 = parts[i].indexOf(']');
                         if (i0 > 0 && i1 > 0) {
                             var index = parts[i].substr(i0, i1 - i0);
                             index = !isNaN(index) ? parseInt(index) : index;
                             parts[i] = parts[i].substr(0, i0 - 1);
                             obj = obj[parts[i]][index];
                         } else obj = obj[parts[i]];
                         if (angular.isUndefined(obj) || obj == null) break;
                     }
                 } else obj = obj[path];
             }
             return obj;
         };
         var _lookupObjectValue = function (rootObj, rootPath, rootId, item, path, id) {
             var collection = _resolvePath(rootObj, rootPath);
             if (collection) {
                 var idObject = item[id];
                 var obj = _lookupItem(collection, rootId, idObject);
                 if (obj) {
                     var ret = _resolvePath(obj, path);
                     return ret;
                 }
             }
             return '';
         };
         var _assignValue = function (item, path, property, value) {
             var obj = item;
             if (path) {
                 if (path.indexOf('.') > 0) {
                     var parts = path.split('.');
                     for (var i = 0; i < parts.length; i++) {
                         var i0 = parts[i].indexOf('[') + 1;
                         var i1 = parts[i].indexOf(']');
                         if (i0 > 0 && i1 > 0) {
                             var index = parts[i].substr(i0, i1 - i0);
                             index = !isNaN(index) ? parseInt(index) : index;
                             parts[i] = parts[i].substr(0, i0 - 1);
                             obj = obj[parts[i]][index];
                         } else obj = obj[parts[i]];
                         if (angular.isUndefined(obj) || obj == null) break;
                     }
                 } else obj = obj[path];
             }
             if (obj) obj[property] = value;
         };
         var _cloneObject = function (obj) {
             if (null == obj || "object" != typeof obj) return obj;
             var copy = obj.constructor();
             for (var attr in obj) {
                 if (obj.hasOwnProperty(attr)) copy[attr] = obj[attr];
             }
             return copy;
         };
         var _extendObject = function (objOld, objNew) {
             for (var prop in objNew) {
                 if (objNew.hasOwnProperty(prop)) {
                     objOld[prop] = objNew[prop];
                 }
             }
             return objOld;
         };

         return {
             generateUuid: _generateUuid,
             lookupItem: _lookupItem,
             lookupObjectValue: _lookupObjectValue,
             removeItem: _removeItem,
             selectItems: _selectItems,
             clearSelections: _clearSelections,
             collectSelections: _collectSelections,
             resolvePath: _resolvePath,
             assignValue: _assignValue,
             cloneObject: _cloneObject,
             extendObject: _extendObject
         };
     }]);