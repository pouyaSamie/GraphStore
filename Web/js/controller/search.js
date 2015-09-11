// Loading Data
graphStore.controller('search', function ($rootScope,$scope, $state, publicSearchDataService) {
    $scope.searchTerm = '';
    $scope.submitsearch = function () {
        publicSearchDataService.term = $scope.searchTerm;
        if($state.current.name == 'gallery'){
            $rootScope.$broadcast('searchTermChange', {
                term: $scope.searchTerm
            });
        }else{
            $state.transitionTo('gallery',{reload:true});
        }
    }
});
