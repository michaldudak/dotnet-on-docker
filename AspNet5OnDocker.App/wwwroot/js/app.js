(function (ng) {
	"use strict";

	var app = ng.module("app", []);

	app.controller("todosController", function ($scope, $http) {

		$scope.newTodo = {
			Id: 0
		};

		function refresh() {
			$http.get("/api/todos").then(function (response) {
				$scope.todos = response.data;
			});
		}

		refresh();

		$scope.save = function () {
			if (!$scope.newTodo.Task) {
				return;
			}

			$http.post("/api/todos", $scope.newTodo).then(refresh);
			$scope.newTodo = {
				Id: 0
			};
		};

		$scope.update = function (todo) {
			todo.IsCompleted = !todo.IsCompleted;
			$http.put("/api/todos/" + todo.Id, todo).then(refresh);
		};

		$scope.remove = function (todo) {
			$http.delete("/api/todos/" + todo.Id).then(refresh);
		};

		$scope.changeStateText = function (todo) {
			return todo.IsCompleted ? "Uncomplete" : "Complete";
		};
	});

	app.directive("stateChangeButton", function () {
		return {
			template: "<button class='btn {{btnClass}}'>{{btnText}}</button>",
			replace: true,
			$scope: { todo: "=" },
			link: function ($scope) {
				if ($scope.todo.IsCompleted) {
					$scope.btnClass = "btn-warning";
					$scope.btnText = "Uncomplete";
				} else {
					$scope.btnClass = "btn-success";
					$scope.btnText = "Complete";
				}
			}
		};
	});

	app.directive("statusMarker", function () {
		return {
			template: "<i class='fa fa-lg fa-{{icon}}'></i>",
			scope: { todo: "=" },
			link: function ($scope) {
				$scope.$watch("todo", function () {
					if ($scope.todo.IsCompleted) {
						$scope.icon = "check-square-o";
					} else {
						$scope.icon = "square-o";
					}
				});
			}
		}
	});

}(window.angular));