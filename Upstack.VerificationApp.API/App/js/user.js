angular.module('user', [])
    .component('user', {
        templateUrl: './registration.html',

    })
    .controller('UserCtrl', ['$scope', '$http', 'SaveUserFactory', function ($scope, $http, SaveUserFactory) {
        $scope.user = {};
        $scope.Register = function (e) {
            alert(JSON.stringify($scope.user))
            SaveUserFactory.SaveRecord($scope.user)           
                .then(function (response) {
                    SaveUserFactory.SendEmail($scope.user.Email);
                    $scope.status = 'Thank you for signing up';
                    $scope.user = {};
                }, function (error) {
                    $scope.status = 'Unable to insert customer: ';
                    alert("Error" + JSON.stringify(error))
                });
          
        }
    }])
    .factory('SaveUserFactory', function ($http) {
       
        var Info = {};
        Info.SaveRecord = function (data) {
            var url = "http://localhost:50255/api/1.0/user";
            return $http.post(url, data, {
                responseType: "json",
                
            });
        };
        Info.SendEmail = function (email) {
           
            var url = "http://localhost:50255/api/1.0/User/Send-Email?baseUrl=https://localhost:3002&email=" + email;
            return $http.post(url, "", {
                responseType: "json",                
                params: {
                    type: "json",
                    url: _data.urlString,
                }
            });
            Info.ActivateUser = function (email) {
                var _data = data || {};
                var url = "http://localhost:50255/api/1.0/User/?email=" + email;
                return $http.put(url, "", {
                    responseType: "json",
                    params: {
                        type: "json",
                        url: _data.urlString,
                    }
                });
        return Info;
    });
    

