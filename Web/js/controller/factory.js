graphStore.factory('Service', function (localStorageService) {
    var Service = {
        isProductInBasket: function (key) {
            var proDict = angular.fromJson(localStorageService.get('buyBasketProductDict'))
            productDict = proDict ? proDict : {};
            if (productDict[key]) {
                return true
            } else {
                return false;
            }
        }
    };
    return Service;
});