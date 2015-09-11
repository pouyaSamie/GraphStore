// Loading Data
graphStore.controller('gallery', function ($rootScope, $scope, $http, publicSearchDataService, $stateParams, Service) {
    // View Info
    $scope.currentView = "This is current View Text";
    $scope.currentPage = 1;
    $scope.sortingKey = 'createDate';
    $scope.pageCount = 16;
    $scope.keyword = publicSearchDataService.term;
    $scope.bigCurrentPage = 1;
    $scope.totalItems = 100;
    $scope.bigTotalItems = 100;

    $scope.addToBasket = function (product) {
        if (product.isInBasket == true) {
            product.isInBasket = false;
            $rootScope.$broadcast('removeFromBasket', {productData: product});
        } else {
            product.isInBasket = true;
            $rootScope.$broadcast('addToBasket', {productData: product});
        }
    }

    $rootScope.$on('searchTermChange', function (event, data) {
        $scope.keyword = data.term;
        $scope.getData();
    });
    $scope.changeFiltering = function (key) {
        $scope.getData();
    };
    $scope.getFilteringData = function () {
        $http({
            url: defaultDataUrl + "/json/filtering.json",
            //url: "http://localhost:63342/graphStore/www/data/json/filtering.json",
            method: "get"
        }).success(function (response) {
            $scope.filteringData = response;
        });
    };
    $scope.getFilteringData();

    // Loading Ajax Data
    $scope.getData = function () {
        $scope.loading = true;
            $http({
                url: "/api/Product/getlist/" + $stateParams.category_Id,
                method: "get",
                params: {
                    searchKey: $scope.keyword,
                    pageNumber: $scope.currentPage,
                    pagesize: $scope.pageCount,
                    sortField: $scope.sortingKey,
                    sortDirection: 'desc'
                }
            })
                .success(function (response) {
                    $.each(response, function (key, item) {
                        item['isInBasket'] = Service.isProductInBasket(item.productId);
                    })
                    $scope.galleryData = response;
                    $scope.loading = false;
                });
    };
    $scope.getData();
    // Paging
    $scope.pageChanged = function () {
        $scope.getData();
    };
    $scope.setPageCount = function (pageCount) {
        $scope.pageCount = pageCount;
        $scope.getData();
    };
    $scope.setSortingKey = function (sortingKey) {
        $scope.sortingKey = sortingKey;
        $scope.getData();
    };
});
