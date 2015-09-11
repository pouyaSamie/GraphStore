graphStore.controller('SignupCtrl', function ($scope, $alert, $auth, $state) {
    $scope.signup = function () {
        $auth.signup({
            displayName: $scope.displayName,
            email: $scope.email,
            password: $scope.password
        }).then(function () {
            $state.go("login");
        }).catch(function (response) {
            $alert({
                content: response.data.message,
                duration: 6,
                container: 'body',
                placement: 'bottom-right',
                type: 'danger'
            });
        });
    };
});