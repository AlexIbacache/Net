# 📚 BibliotecaAPI – Sistema de Gestión de Biblioteca Municipal
### **Integrantes: Russell Madariaga-Gabriel Escárate-Alex Ibacache**

Este proyecto es una Web API RESTful desarrollada en ASP.NET Core con Entity Framework Core para la Biblioteca Municipal **“Letras Libres”**, permitiendo gestionar libros, usuarios y préstamos de forma eficiente, moderna y segura.

## 🛠️ Tecnologías Utilizadas

- ASP.NET Core
- Entity Framework Core
- SQL Server
- Swagger
- C#

## 🧠 Estructura de Datos

### Entidades Principales

#### 📘 Libro
- `Id` (int): Identificador único.
- `Titulo` (string, máx. 100): Título del libro.
- `Autor` (string, máx. 100): Autor del libro.
- `ISBN` (string, máx. 50): Código ISBN del libro.
- `AnioPublicacion` (int): Año de publicación.
- `Disponible` (bool): Indica si el libro está disponible para préstamo.

#### 👤 Usuario
- `Id` (int): Identificador único.
- `Nombre` (string, máx. 50): Nombre del usuario.
- `Apellido` (string, máx. 50): Apellido del usuario.
- `Email` (string, máx. 100): Correo electrónico del usuario.

#### 🔄 Préstamo
- `Id` (int): Identificador único.
- `LibroId` (int): Clave foránea al libro prestado.
- `UsuarioId` (int): Clave foránea al usuario que realiza el préstamo.
- `FechaPrestamo` (datetime): Fecha en que se realizó el préstamo.
- `FechaDevolucion` (datetime, nullable): Fecha en que se devolvió el libro.

### 🔗 Relaciones

- Un **usuario** puede tener **muchos préstamos**.
- Un **libro** puede estar en muchos préstamos, pero solo **uno activo a la vez**.

## ⚙️ Configuración

1. Clona el repositorio:
   ```bash
   git clone https://github.com/AlexIbacache/Net
2. Abre el proyecto en Visual Studio 2022 o superior.

3. Asegúrate de tener acceso a SQL Server.

4. Ejecuta migraciones:
    Update-Database
   
6. Ejecuta el proyecto. Swagger estará disponible en:
https://localhost:{puerto}/swagger/index.html

## 🛠️ Migraciones ORM con Entity Framework

Para gestionar la base de datos a través de Entity Framework Core desde Visual Studio, puedes utilizar la Consola del Administrador de Paquetes.

### 📌 Pasos para aplicar migraciones

1. Abre Visual Studio y selecciona tu proyecto principal (`BibliotecaAPI`).
2. Ve a:  
   `Herramientas` → `Administrador de paquetes NuGet` → `Consola del Administrador de paquetes`.

3. En la consola, ejecuta los siguientes comandos según el caso:

#### ➕ Crear una nueva migración
```powershell
Add-Migration NombreDeLaMigracion
```
🗃️ Aplicar la migración a la base de datos
```powershell
Update-Database
```

## 📸 Pruebas y Validaciones

Este apartado documenta las pruebas realizadas a los endpoints principales de la API, incluyendo errores encontrados, sus correcciones y casos exitosos. Se adjuntan capturas de pantalla como evidencia.

---

### ❌ Casos con errores

#### 🔻 POST `/api/Libros`

**Error detectado:**
- **400 Bad Request** – `"The Prestamos field is required."`  
  Este error ocurrió al intentar crear un libro sin especificar el campo `Prestamos`, el cual era requerido por defecto.

**Capturas del error:**

![Error Prestamos Requerido 1](https://github.com/user-attachments/assets/0e8fb944-7ed4-4f56-baac-b825cb5a8c86)  
![Error Prestamos Requerido 2](https://github.com/user-attachments/assets/84ffad40-8e2f-44f2-b503-b26e39dd14e6)

**Corrección aplicada:**
- Se actualizó el modelo `Libro.cs` para que la colección `Prestamos` sea opcional:

**Evidencia de código corregido:**

![Código Prestamos Opcional](https://github.com/user-attachments/assets/09a8bbb2-9097-4066-b430-6eedc4fd5c8c)

**Cuerpo JSON corregido usado en pruebas:**
```json
{
  "titulo": "Harry Potter y la Piedra Filosofal",
  "autor": "J.K. Rowling",
  "isbn": "978-84-675-8034-1",
  "anioPublicacion": 1997,
  "disponible": true
}
```

---

#### 🔻 POST `/api/Prestamoes`

**Errores detectados:**
- **400 Bad Request** – `"The Libro field is required."`, `"The Usuario field is required."`  
  Ocurrió al enviar objetos anidados en lugar de solo los IDs relacionados.

- **500 Internal Server Error** – `"No route matches the supplied values."`  
  El método `CreatedAtAction()` fallaba porque `GetPrestamo(int id)` estaba comentado.

**Captura del error 400:**

![Error Libro y Usuario](https://github.com/user-attachments/assets/38ea4fc9-84f2-44ab-8767-6cb9ba410608)

**Correcciones aplicadas:**
- Se actualizó el modelo `Prestamo.cs` para que `Libro` y `Usuario` sean opcionales:
```csharp
public Libro? Libro { get; set; }
public Usuario? Usuario { get; set; }
```

- Se reactivó el método `GetPrestamo(int id)` en `PrestamoesController` para que el `CreatedAtAction()` funcione correctamente.

**Cuerpo JSON corregido usado en pruebas:**
```json
{
  "libroId": 1,
  "usuarioId": 1,
  "fechaPrestamo": "2025-06-10T10:00:00Z",
  "fechaDevolucion": null
}
```

---

### ✅ Casos exitosos

Una vez aplicadas las correcciones mencionadas, ambos endpoints funcionaron correctamente.

**Capturas de pruebas exitosas:**

- Registro exitoso de libro:  
![Libro creado correctamente](https://github.com/user-attachments/assets/40f7d2a5-ba9d-4b98-9479-eedb1bdef62f)

- Registro exitoso de préstamo:  
![Préstamo creado correctamente](https://github.com/user-attachments/assets/074f6b1c-3619-4bdf-828b-a65b1fab3bed)
