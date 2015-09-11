//graphStore.directive('multipleFileUploader', function () {
//    return {
//        scope: true,
//        restrict: "A",
//        controller: function ($config, $http, $scope, $element, $attrs, FileUploader, authFactory) {
//            $scope.deleteFile = function (idx) {
//                var apiUrl = $config.API_URL + '/file/remove';
//                var file = $scope.model.filesList[idx];
//                $http.post(apiUrl, file).
//                    success(function (response, status, headers, config) {
//                        $scope.model.filesList.splice(idx, 1);
//                    }).
//                    error(function (response, status, headers, config) {
//                        $scope.message = response;
//                    });
//            };
//            var authData = authFactory.getAuthenticationData();
//            var uploader = $scope.uploader = new FileUploader({
//                url: $config.API_URL + '/file/upload',
//                headers: { 'Authorization': 'Bearer ' + authData.token },
//                autoUpload: true,
//                withCredentials: true
//            });
//            uploader.onCompleteItem = function (fileItem, response, status, headers) {
//                $element.find('[multiple-file-id]')[0].value += response[0].fileId + ',';
//                $element.find('[multiple-file-id]').trigger('change');
//            };
//
//        }
//    }
//});