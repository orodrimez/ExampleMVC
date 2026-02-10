🍸 Cocktail Explorer — ASP.NET Core MVC

Aplicación web desarrollada con ASP.NET Core MVC que consume la API pública TheCocktailDB para permitir la búsqueda y exploración de cócteles, con la capacidad adicional de almacenar favoritos en una base de datos local.

El proyecto demuestra el consumo de APIs externas, uso del patrón Repository, manejo de ViewModels y una arquitectura MVC limpia y mantenible.

🧠 Funcionalidad principal

La aplicación permite:

Buscar cócteles por nombre

Buscar cócteles por ingrediente

Visualizar el detalle completo de un cóctel

Marcar y desmarcar cócteles como favoritos

Consultar la lista de favoritos almacenados localmente

🧱 Arquitectura aplicada
/Controllers          → Flujo MVC
/Repository           → Consumo de API externa + persistencia local
/ViewModels           → Modelos específicos para vistas
/Views                → Razor Views


El ICocktailRepository abstrae:

Llamadas HTTP a TheCocktailDB

Manejo de datos

Persistencia de favoritos

📡 Rutas reales de la aplicación
Método	Ruta	Descripción
GET	/Cocktails	Pantalla principal
POST	/Cocktails/SearchByName	Buscar por nombre
POST	/Cocktails/SearchByIngredient	Buscar por ingrediente
GET	/Cocktails/Details/{id}	Detalle del cóctel
GET	/Cocktails/Favorites	Lista de favoritos
POST	/Cocktails/AddFavorite	Agregar a favoritos
POST	/Cocktails/RemoveFavorite	Eliminar de favoritos
🛠️ Tecnologías utilizadas
Tecnología	Uso
ASP.NET Core MVC	Framework web
Razor Views	Renderizado del lado del servidor
Repository Pattern	Abstracción de datos
HttpClient	Consumo de API externa
Entity Framework Core	Persistencia de favoritos
C#	Lenguaje principal
🔐 Buenas prácticas aplicadas

Uso de async/await en todas las operaciones I/O

ViewModels para separar datos de vistas

Repository para desacoplar consumo de API

Separación clara entre UI, lógica y datos

Persistencia local de favoritos

🚀 Cómo ejecutar
git clone <repo>
cd <repo>
dotnet run


Abrir en navegador:

https://localhost:{puerto}/Cocktails

🎯 Qué demuestra este proyecto

Este proyecto demuestra la capacidad de integrar una API pública en una aplicación MVC real, aplicando patrones de arquitectura y manteniendo una separación clara entre presentación, lógica y acceso a datos.
