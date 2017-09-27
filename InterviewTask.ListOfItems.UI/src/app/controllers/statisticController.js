(function() {

  angular.module("app")
      .controller("StatisticController", StatisticController);

  function StatisticController($http) {

      var vm = this;

    vm.itemsTypes = [];
    vm.items = [];
     $http.get("http://localhost:54325/api/Items/get").then(function (response) {
         vm.items = response.data;
        var itemsTypes= {};
         for (var i = 0; i < vm.items.length; i++) {
             itemsTypes[vm.items[i].Type] = 1 + (itemsTypes[vm.items[i].Type] || 0);
       }
         
       vm.typeList = [];
       for (var propertyName in itemsTypes) {
         var obj = {
           name: propertyName,
           count: itemsTypes[propertyName]
         }
         vm.typeList.push(obj);
       }

     });
     vm.title = "Statistic";
  }
})();
