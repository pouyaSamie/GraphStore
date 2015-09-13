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
    $scope.download = function (product) {
        $http({
            url: "/api/files/" + product.downloadToken,
            method: "get"
        })
            .success(function (data, status, headers) {
                var octetStreamMime = 'application/octet-stream';
                var success = false;
                // Get the filename from the x-filename header or default to "download.bin"
                var filename = product.productId;//headers['x-filename'] || 'download.bin';
                // Determine the content type from the header or default to "application/octet-stream"
                var contentType = headers['content-type'] || octetStreamMime;

                try {
                    // Try using msSaveBlob if supported
                    //console.log("Trying saveBlob method ...");

                    var blob = new Blob([data], { type: contentType });
                    if (navigator.msSaveBlob)
                        navigator.msSaveBlob(blob, filename);
                    else {
                        // Try using other saveBlob implementations, if available
                        var saveBlob = navigator.webkitSaveBlob || navigator.mozSaveBlob || navigator.saveBlob;
                        if (saveBlob === undefined) throw "Not supported";
                        saveBlob(blob, filename);
                    }
                    //console.log("saveBlob succeeded");
                    success = true;
                } catch (ex) {
                    //console.log("saveBlob method failed with the following exception:");
                    //console.log(ex);
                }

                if (!success) {
                    // Get the blob url creator
                    var urlCreator = window.URL || window.webkitURL || window.mozURL || window.msURL;
                    if (urlCreator) {
                        // Try to use a download link
                        var link = document.createElement('a');
                        if ('download' in link) {
                            // Try to simulate a click
                            try {
                                // Prepare a blob URL
                                //console.log("Trying download link method with simulated click ...");
                                var blob = new Blob([data], { type: contentType });
                                var url = urlCreator.createObjectURL(blob);
                                link.setAttribute('href', url);

                                // Set the download attribute (Supported in Chrome 14+ / Firefox 20+)
                                link.setAttribute("download", filename);

                                // Simulate clicking the download link
                                var event = document.createEvent('MouseEvents');
                                event.initMouseEvent('click', true, true, window, 1, 0, 0, 0, 0, false, false, false, false, 0, null);
                                link.dispatchEvent(event);

                                //console.log("Download link method with simulated click succeeded");
                                success = true;

                            } catch (ex) {
                                //console.log("Download link method with simulated click failed with the following exception:");
                                //console.log(ex);
                            }
                        }

                        if (!success) {
                            // Fallback to window.location method
                            try {
                                // Prepare a blob URL
                                // Use application/octet-stream when using window.location to force download
                                //console.log("Trying download link method with window.location ...");
                                var blob = new Blob([data], { type: octetStreamMime });
                                var url = urlCreator.createObjectURL(blob);
                                window.location = url;
                                //console.log("Download link method with window.location succeeded");
                                success = true;
                            } catch (ex) {
                                //console.log("Download link method with window.location failed with the following exception:");
                                //console.log(ex);
                            }
                        }

                    }
                }

                if (!success) {
                    // Fallback to window.open method
                    //console.log("No methods worked for saving the arraybuffer, using last resort window.open");
                    window.open(httpPath, '_blank', '');
                }


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
