graphStore.controller('LoginCtrl', function ($scope, $alert, $auth, $state, $translate) {


    $scope.login = function () {
        $auth.login({email: $scope.email, password: $scope.password})
            .then(function () {
                $alert({
                    content: $translate.instant('xhr.login'),
                    duration: 6,
                    container: 'body',
                    placement: 'bottom-right',
                    type: 'success'
                });
                $state.go("home",true);
            })
            .catch(function (response) {
                $alert({
                    content: $translate.instant('xhr.'+response.data.message),
                    duration: 6,
                    container: 'body',
                    placement: 'bottom-right',
                    type: 'danger'
                });
            });
    };
    $scope.authenticate = function (provider) {
        $auth.authenticate(provider)
            .then(function () {
                ////$alert({
                ////    content: 'You have successfully logged in',
                ////    animation: 'fadeZoomFadeDown',
                ////    type: 'material',
                ////    duration: 3
                ////});
                //log('authenticate success')
                //log($auth)
                //log('-------------------------')
            })
            .catch(function (response) {
                ////$alert({
                ////    content: response.data ? response.data.message : response,
                ////    animation: 'fadeZoomFadeDown',
                ////    type: 'material',
                ////    duration: 3
                ////});
                //log('authenticate error')
                //log($auth)
                //log('-------------------------')
            });
    };
});