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

app.controller('LoginController', ['$scope', '$http', function ($scope, $http) {
    $scope.loginData = {
        Usuario: '',
        Pass: ''
    };
    // Verificar si el usuario está almacenado en localStorage
    $scope.isLoggedIn = localStorage.getItem('isLoggedIn') === 'true';
    $scope.loggedInUser = localStorage.getItem('username') || '';

    $scope.login = function () {
        $http.post('https://localhost:7008/api/UsuariosControlador/Login', $scope.loginData)
            .then(function (response) {
                $scope.isLoggedIn = true; // Usuario ha iniciado sesión
                $scope.loggedInUser = $scope.loginData.Usuario; // Almacena el nombre de usuario
                localStorage.setItem('isLoggedIn', 'true'); // Guardar el estado de sesión
                localStorage.setItem('username', $scope.loginData.Usuario); // Guardar el nombre de usuario
                alert('Login exitoso');
                // Puedes redirigir a otra página si el login es exitoso
                window.location.href = '/index.html';
            }, function (error) {
                alert('Error en el login: ' + error.data);
            });
    };
    $scope.logout = function () {
        // Hacer una solicitud al backend para eliminar la cookie de sesión
        $http.post('https://localhost:7008/api/UsuariosControlador/Logout')
            .then(function (response) {
                // Borrar la información del localStorage
                localStorage.removeItem('isLoggedIn');
                localStorage.removeItem('username');

                // Cambiar el estado de inicio de sesión en el frontend
                $scope.isLoggedIn = false;
                $scope.loggedInUser = '';

                // Mostrar un mensaje de confirmación
                alert('Has cerrado sesión');

                // Redirigir a la página de login u otra página según sea necesario
                window.location.href = '/index.html';
            }, function (error) {
                console.error('Error cerrando sesión:', error);
            });
    };
    // Fetch user-specific movies if logged in
    if ($scope.isLoggedIn) {
        $scope.cargarPeliculasPreferidas = function () {
            $http.get('https://localhost:7008/api/PeliculasControlador/GetPreferenciasUsuario?usuario=' + $scope.loggedInUser)
                .then(function (response) {
                    $scope.peliculasPreferidas = response.data.value;  // Store user's preferred movies
                    console.log("Películas preferidas del usuario cargadas:", $scope.peliculasPreferidas);
                }, function (error) {
                    console.error("Error al cargar las películas preferidas:", error);
                });
        };

        // Load user preferences on page load if logged in
        $scope.cargarPeliculasPreferidas();
    }
}]);