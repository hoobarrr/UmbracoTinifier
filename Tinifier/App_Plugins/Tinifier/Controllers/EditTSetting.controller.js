﻿angular.module('umbraco').controller('Tinifier.EditTSetting.Controller', function ($scope, $http) {

    $scope.tsetting = null;
    $http.get('/umbraco/backoffice/api/Tinifier/GetTSetting').success(function (response) {
        $scope.tsetting = response;
    });
});