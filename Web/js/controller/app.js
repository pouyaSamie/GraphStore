var graphStore = angular.module('graphStore', ['ui.router','ngMessages','mgcrea.ngStrap',
    'ui.bootstrap', 'LocalStorageModule', 'pascalprecht.translate', 'satellizer', 'angularFileUpload']);

graphStore.config(function ($stateProvider, $urlRouterProvider, localStorageServiceProvider, $translateProvider, $authProvider) {

    defaultDataUrl = '/data';

    $urlRouterProvider.otherwise("/home");
    $stateProvider
        .state('home', {
            url: "/home",
            templateUrl: "pages/home.html"
        }).state('gallery', {
            url: "/gallery:category_Id",
            templateUrl: "pages/gallery.html",
            controller: 'gallery'
        }).state('product', {
            url: "/product:product_id",
            templateUrl: "pages/product.html"
        }).state('viewcart', {
            url: "/viewcart",
            templateUrl: "pages/viewcart.html"
        }).state('favorite', {
            url: "/favorite",
            templateUrl: "pages/gallery.html",
            controller: 'favorite'
        }).state('login', {
            url: '/login',
            templateUrl: 'pages/login.html',
            controller: 'LoginCtrl'
        }).state('signup', {
            url: '/signup',
            templateUrl: 'pages/signup.html',
            controller: 'SignupCtrl'
        })
        .state('logout', {
            url: '/logout',
            template: null,
            controller: 'LogoutCtrl'
        })
        .state('uploader', {
            url: '/uploader',
            templateUrl: 'pages/uploader.html',
            controller: 'uploaderCtrl'
        })
        .state('profile', {
            url: '/profile',
            templateUrl: 'pages/profile.html',
            controller: 'ProfileCtrl',
            resolve: {
                authenticated: function ($q, $location, $auth) {
                    var deferred = $q.defer();

                    if (!$auth.isAuthenticated()) {
                        $location.path('/login');
                    } else {
                        deferred.resolve();
                    }

                    return deferred.promise;
                }
            }
        });

    localStorageServiceProvider
        .setPrefix('graphStore')
        .setStorageType('localStorage')
        .setNotify(true, true)
        .setStorageCookieDomain('/');


    $translateProvider.useStaticFilesLoader({
        prefix: 'i18n/locale-',
        suffix: '.json'
    });

    $translateProvider.preferredLanguage('fa');

    // Authentication
    //=================================================================================


    $authProvider.httpInterceptor = true; // Add Authorization header to HTTP request
    $authProvider.loginOnSignup = true;
    $authProvider.baseUrl = '' // API Base URL for the paths below.
    $authProvider.loginRedirect = '/home';
    $authProvider.logoutRedirect = '/home';
    $authProvider.signupRedirect = '/login';
    $authProvider.loginUrl = '/auth/login';
    $authProvider.signupUrl = '/auth/signup';
    $authProvider.loginRoute = '/login';
    $authProvider.signupRoute = '/signup';
    $authProvider.tokenRoot = false; // set the token parent element if the token is not the JSON root
    $authProvider.tokenName = 'token';
    $authProvider.tokenPrefix = 'satellizer'; // Local Storage name prefix
    $authProvider.unlinkUrl = '/auth/unlink/';
    $authProvider.unlinkMethod = 'get';
    $authProvider.authHeader = 'Authorization';
    $authProvider.authToken = 'Bearer';
    $authProvider.withCredentials = true;
    $authProvider.platform = 'browser'; // or 'mobile'
    $authProvider.storage = 'localStorage'; // or 'sessionStorage'

    $authProvider.facebook({
        clientId: '624059410963642'
    });
    $authProvider.google({
        clientId: '631036554609-v5hm2amv4pvico3asfi97f54sc51ji4o.apps.googleusercontent.com'
    });
    $authProvider.github({
        clientId: '0ba2600b1dbdb756688b'
    });
    $authProvider.linkedin({
        clientId: '77cw786yignpzj'
    });
    $authProvider.yahoo({
        clientId: 'dj0yJmk9dkNGM0RTOHpOM0ZsJmQ9WVdrOVlVTm9hVk0wTkRRbWNHbzlNQS0tJnM9Y29uc3VtZXJzZWNyZXQmeD0wMA--'
    });
    $authProvider.live({
        clientId: '000000004C12E68D'
    });
    $authProvider.twitter({
        url: '/auth/twitter'
    });
    $authProvider.oauth2({
        name: 'foursquare',
        url: '/auth/foursquare',
        redirectUri: window.location.origin,
        clientId: 'MTCEJ3NGW2PNNB31WOSBFDSAD4MTHYVAZ1UKIULXZ2CVFC2K',
        authorizationEndpoint: 'https://foursquare.com/oauth2/authenticate',
    });
});