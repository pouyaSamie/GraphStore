graphStore.controller('LogoutCtrl', function ($auth, $state, $state) {
    if (!$auth.isAuthenticated()) {
        return;
    }
    $auth.logout()
        .then(function () {
            //$alert({
            //    content: 'You have been logged out',
            //    animation: 'fadeZoomFadeDown',
            //    type: 'material',
            //    duration: 3
            //});
            $state.go("home")
        });
});