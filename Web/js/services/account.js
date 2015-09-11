graphStore.factory('Account', function ($http) {

    return {
        getProfile: function () {
            log('Account getProfile')
            return $http.get('/api/me');
        },
        updateProfile: function (profileData) {
            return $http.post('/api/me', profileData);
        }
    };
});