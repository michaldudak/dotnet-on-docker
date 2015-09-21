(function (ng) {
	"use strict";

	var app = ng.module("app", []);

	app.controller("projectsController", function($scope, $http) {

		$scope.newProject = {
			Id: 0
		};

		function refresh() {
			$http.get("/api/projects").then(function(response) {
				$scope.projects = response.data;
			});
		}

		refresh();

		$scope.saveProject = function () {
			$http.post("/api/projects", $scope.newProject).then(refresh);
		}
	});

}(window.angular));