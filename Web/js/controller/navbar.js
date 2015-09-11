// buy Basket
graphStore.controller('navBar', function ($scope,$auth,Account) {
    $scope.isAuthenticated = function() {
        return $auth.isAuthenticated();
    };
    $scope.getProfile = function () {
        Account.getProfile()
            .success(function (data) {
                $scope.user = data;
            });
    };
    if($auth.isAuthenticated()){
        $scope.getProfile();
    }

});
