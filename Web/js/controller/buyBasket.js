// buy Basket
graphStore.controller('buyBasket', function ($scope, $http, $rootScope, localStorageService) {
    $scope.count = localStorageService.get('buyBasketCount');
    $scope.totalPrice = localStorageService.get('buyBasketTotalPrice');
    var proDict = angular.fromJson(localStorageService.get('buyBasketProductDict'));
    $scope.productDict = proDict ? proDict : {};
    $scope.$on('addToBasket', function (event, args) {
        $scope.count += 1;
        $scope.shakeBasket(args.productData, "+");
    });
    $scope.$on('removeFromBasket', function (event, args) {
        $scope.count -= 1;
        $scope.shakeBasket(args.productData, "-");
    });
    $scope.$on('resetBasket', function () {
        localStorageService.set('buyBasketProductDict', {});
        localStorageService.set('buyBasketCount', 0);
        localStorageService.set('buyBasketTotalPrice', 0);
        $scope.count = 0;
        $scope.totalPrice = 0;
    });
    $scope.shakeBasket = function (product, action) {
        if (action == "+") {
            $scope.productDict[product.productId] = product;
            $scope.totalPrice += product.price;
        } else {
            $scope.totalPrice -= product.price;
            delete $scope.productDict[product.productId];
        }
        basketDataString = angular.fromJson($scope.productDict);
        localStorageService.set('buyBasketProductDict', basketDataString);
        localStorageService.set('buyBasketCount', $scope.count);
        localStorageService.set('buyBasketTotalPrice', $scope.totalPrice);
    };
});
