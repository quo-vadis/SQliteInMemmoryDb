(function() {

  angular.module("app")
      .controller("ItemsController", ItemsController);

  function ItemsController($http) {
    var vm = this;

    vm.item = {
      Name: "",
      Type: ""
    };

    vm.inputButton = "add";

    $http.get("http://localhost:54325/api/Items/get").then(function (resp) {
        vm.items = resp.data;
    });

    vm.title = "Items";

    vm.save = function () {
      if (vm.inputButton === "update") {
        $http.put("http://localhost:54325/api/Items/put/" + vm.item.Id, vm.item)
          .then(function(resp) {
              vm.items = resp.data;
              vm.inputButton = "add";
              vm.item = {};
            },
            function(err) {
              console.log(err);
              vm.inputButton = "add";
            });
      } else {
        $http.post("http://localhost:54325/api/Items/post", vm.item)
          .then(
            function(resp) {
              vm.items = resp.data;
              vm.item = {};
            },
            function(err) {
              console.warn(err);
            }
          );
      }
    }

    vm.delete = function (id) {
        $http.delete("http://localhost:54325/api/Items/delete/" + id, id)
            .then(function (resp) {
                vm.items = resp.data;
          });
    }


    vm.edit = function (id) {
        var found = vm.items.filter(function (obj) {
          return obj.Id === id;
        });

        vm.item = found[0];
        vm.inputButton = "update";
    }
  };
})();