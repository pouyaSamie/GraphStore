/**
 * Created by Arsham on 6/4/15.
 */
// Loading Data
graphStore.controller('product', function ($scope, $http, $rootScope, Service, $stateParams, $auth) {
    $scope.buyBasketText = "Add To Basket";
    $scope.addToFavText = "Add To Favorite";
    $scope.mainImageUrl = "";
    $scope.isAuthenticated = function () {
        return $auth.isAuthenticated();
    };
    $scope.bootstrap = function () {
        if ($scope.isInBasket) {
            $scope.buyBasketText = "Remove From Basket";
        } else {
            $scope.buyBasketText = "Add To Basket";
        }
    };
    log($stateParams.product_Id)
    $scope.getData = function () {
        $http({
            url: "/api/product/getproductinfo/" + $stateParams.product_id,
            method: "get"
        })
            .success(function (response) {
                $scope.data = response;
                $scope.mainImageUrl = response.images[0];
                $scope.isInBasket = Service.isProductInBasket(response.productId);
                if ($scope.data.isFavorite == true) {
                    $scope.addToFavText = "Remove From Favorite";
                } else {
                    $scope.addToFavText = "Add To Favorite";
                }
                $scope.bootstrap();
            });
    };
    $scope.getData();
    $scope.changeMainImage = function (url) {
        $scope.mainImageUrl = url;
    };
    $scope.addToFavorit = function () {
        log($scope.data.productId)
        if ($scope.data.isFavorite == true) {
            $http({
                url: "/api/unfavorite?productId=" + $scope.data.productId,
                method: "post"
            })
                .success(function (response) {
                    $scope.data.isFavorite = false;
                    $scope.addToFavText = "Add To Favorite";
                });
        } else {
            $http({
                url: "/api/favorite?productId=" + $scope.data.productId,
                method: "post",
            })
                .success(function (response) {
                    $scope.addToFavText = "Remove From Favorite";
                    $scope.data.isFavorite = true;
                });
        }
    };
    $scope.addToBasket = function () {
        if ($scope.isInBasket) {
            $scope.isInBasket = false;
            $scope.buyBasketText = "Add To Basket";
            $rootScope.$broadcast('removeFromBasket', {productData: $scope.data});
        } else {
            $scope.isInBasket = true;
            $scope.buyBasketText = "Remove From Basket";
            $rootScope.$broadcast('addToBasket', {productData: $scope.data});
        }
    };
    $scope.download = function (data) {

        $http({
            url: "/api/files/127485bd-6b8c-4058-a7c8-20e1aadb352d",
            method: "get"
        })
            .success(function (response) {
                log("Doiwnload success")
                log(response)

                Downloadify.create('downloadify', {
                    filename: function () {
                        return "sss";
                    },
                    data: function () {
                        return "sssssss";
                    },
                    onComplete: function () {
                        alert('Your File Has Been Saved!');
                    },
                    onCancel: function () {
                        alert('You have cancelled the saving of this file.');
                    },
                    onError: function () {
                        alert('You must put something in the File Contents or there will be nothing to save!');
                    },
                    transparent: false,
                    swf: 'lib/downloadify/media/downloadify.swf',
                    downloadImage: 'lib/downloadify/images/download.png',
                    width: 100,
                    height: 30,
                    transparent: true,
                    append: false
                });
            });

    };
    var openPhotoSwipe = function () {
        var pswpElement = document.querySelectorAll('.pswp')[0];
        var items = [];
        $.each($scope.data.images, function (index, item) {
            var img = new Image();
            img.src = '/data/img/' + item;
            items.push({
                src: '/data/img/' + item,
                w: img.width,
                h: img.height
            });
        });
        var options = {
            history: false,
            focus: false,
            showAnimationDuration: 0,
            hideAnimationDuration: 0
        };
        var gallery = new PhotoSwipe(pswpElement, PhotoSwipeUI_Default, items, options);
        gallery.init();
    };
    $scope.zoomImage = function () {
        openPhotoSwipe();
    };
});
