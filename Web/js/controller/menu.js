/**
 * Created by Arsham on 6/6/15.
 */
// Loading Data
graphStore.controller('menu', function ($scope, $http) {
    // Loading Ajax Data


    $scope.getData = function () {
        $http({
            url:  "/api/menu/getall",
            method: "get"
        }).success(function (response) {
            $scope.menuData = response;
            $(".mega-menu .dropdown").each(function () {
                var thisWidth = $('.container').width();
                var $dropdownToggle = $(this).find(".dropdown-toggle");
                var $menuTriangle = $(this).find(".menu-triangle");
                var pos = $dropdownToggle.position().left + ($dropdownToggle.width() / 2) + 10;
                $menuTriangle.css({
                    right: thisWidth-pos + 'px'
                })
            });
        });
    };
    $scope.getData();


});
