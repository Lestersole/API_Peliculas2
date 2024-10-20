var app = angular.module('miApp', []);

app.controller('MiControlador', ['$scope', '$http', function ($scope, $http) {
    $scope.categorias = ["Accion", "Comedia", "Drama", "Terror"];
    $scope.peliculas = [];
    $scope.categoriaSeleccionada = "";

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
            $http.get('https://localhost:7008/api/PeliculasControlador/GetCategoria?categoria=' + $scope.categoriaSeleccionada)
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

app.controller('CrearUsuarioController', ['$scope', '$http', function ($scope, $http) {
    $scope.nuevoUsuario = {};  // Objeto para almacenar la información del nuevo usuario
    $scope.mensaje = '';  // Mensaje para mostrar después de la creación

    $scope.crearUsuario = function () {
        console.log("Creando usuario:", $scope.nuevoUsuario); // Mensaje para verificar los datos

        // Reemplaza esta URL con la URL de tu API para crear usuarios
        $http.post('https://localhost:7008/api/UsuariosControlador/CrearUser', $scope.nuevoUsuario)
            .then(function (response) {
                $scope.mensaje = "Usuario creado exitosamente!";
                console.log("Respuesta del servidor:", response);
            }, function (error) {
                $scope.mensaje = "Error al crear el usuario: " + error.data;
                console.error("Error al crear el usuario:", error);
            });
    };
}]);