(function (app) {
    app.controller('orderEditController', orderEditController);

    orderEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService', '$stateParams'];

    function orderEditController(apiService, $scope, notificationService, $state, commonService, $stateParams) {
        $scope.order = {};
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }

        $scope.UpdateOrder = UpdateOrder;

        function loadOrderDetail() {
            apiService.get('api/product/getbyorderid/' + $stateParams.id, null, function (result) {
                console.log(result.data);
                $scope.order = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }
        function UpdateOrder() {
            apiService.put('api/product/updateorder', $scope.product,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật.');
                    $state.go('products');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }
        
        loadOrderDetail();
    }

})(angular.module('tedushop.orders'));