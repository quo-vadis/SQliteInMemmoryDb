(function() {
  angular.module("app", ['angularUtils.directives.dirPagination', "ngRoute"])
    .config(function($routeProvider) {
      $routeProvider.when('/home',
          {
            templateUrl: 'app/pages/items.html',
            controller: 'ItemsController',
            controllerAs: "vm"
          })
          .when('/statistic',
          {
              templateUrl: 'app/pages/statistic.html',
              controller: 'StatisticController',
            controllerAs: "vm"
          });
      $routeProvider.otherwise({ redirectTo: "/home" });
    });
})();

