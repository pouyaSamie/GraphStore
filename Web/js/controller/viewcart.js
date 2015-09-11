/**
 * Created by Arsham on 6/4/15.
 */
// Loading Data
graphStore.controller('viewcart', function ($scope, $http, $rootScope, Service, localStorageService, $alert,$state) {
    $scope.tabs = {};
    $scope.tabs.activeTab = 0;

    $scope.loadOrdersArchive = function () {
        $http.get('/api/basket').then(function (response) {
            $scope.ordersArchive = response.data;
        });
    };

    $scope.bootstrap = function () {
        $scope.count = localStorageService.get('buyBasketCount');
        $scope.totalPrice = localStorageService.get('buyBasketTotalPrice');
        var proDict = angular.fromJson(localStorageService.get('buyBasketProductDict'));
        $scope.productDict = proDict ? proDict : {};
    };
    $scope.removeFromBasket = function (product) {

        $rootScope.$broadcast('removeFromBasket', {productData: product});
        $scope.bootstrap();
    };

    $scope.submitBasket = function () {
        var list = []
        $.each($scope.productDict, function (key, value) {
            list.push(parseInt(key))
        });
        $http.post('/api/basket', {
            productIds: list
        }).then(function () {
            $alert({
                content: 'basket submit successfully',
                duration: 6,
                container: 'body',
                placement: 'bottom-right',
                type: 'success'
            });
            $rootScope.$broadcast('resetBasket');
            $state.reload();
        });
    };
    $scope.bootstrap();
});
