var app = angular.module('miApp', []);

app.controller('MiControlador', ['$scope', '$http', function ($scope, $http) {
    $scope.peliculas = [];

    $scope.cargarPeliculas = function () {
        $http.get('https://localhost:7008/api/PeliculasControlador/GetPeliculas')  // URL del backend
            .then(function (response) {
                $scope.peliculas = response.data.value;
                console.log("Películas cargadas:", $scope.peliculas);
            }, function (error) {
                console.error('Error al cargar las películas', error);
            });
    };

    $scope.cargarUsuarios = function () {
        $http.get
    }

    $scope.buscarPorCategoria = function () {
        if ($scope.categoriaSeleccionada) {
            $http.get('https://localhost:7008/api/PeliculasControlador/GetCategoria/' + $scope.categoriaSeleccionada)
            .then(function (response) {
                $scope.peliculas = response.data.value;
                console.log("Películas cargadas:", $scope.peliculas);
            }, function (error) {
                console.error("Error al cargar las películas:", error);
            });
        }
    }


    $scope.cargarPeliculas();
}]);