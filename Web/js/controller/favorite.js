/**
 * Created by Arsham on 9/4/15.
 */

// Loading Data
graphStore.controller('favorite', function ($rootScope, $scope, $http, publicSearchDataService) {
    // View Info
    $scope.currentView = "This is current View Text";
    $scope.currentPage = 1;
    $scope.sortingKey = 'date';
    $scope.pageCount = 16;
    $scope.keyword = publicSearchDataService.term;
    $scope.totalItems = 64;
    $scope.bigTotalItems = 175;
    $scope.bigCurrentPage = 1;
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
            method: "get"
        }).success(function (response) {
            $scope.filteringData = response;
        });
    };
    $scope.getFilteringData();

    // Loading Ajax Data
    $scope.getData = function () {
        $scope.loading = true;
        setTimeout(function () {
            $http({
                url: "/api/myfavorites",
                method: "get",
                data: {
                    searchKey: $scope.keyword,
                    currentPage: $scope.currentPage,
                    pageCount: $scope.pageCount,
                    sortingKey: $scope.sortingKey
                }
            })
                .success(function (response) {
                    $scope.galleryData = response;
                    //$scope.totalItems = response.totalItems;
                    $scope.loading = false;
                });
        }, 2000);
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
