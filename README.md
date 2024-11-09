# EmplyManager.API

EmplyManager.API es un proyecto API desarrollado con C# y .NET 8, diseñado para gestionar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) para empleados. Este proyecto utiliza ADO.NET y procedimientos almacenados para interactuar con la base de datos, asegurando un rendimiento eficiente y seguro.

## Arquitectura del Proyecto

El proyecto sigue una arquitectura de N capas, que incluye las siguientes capas:

1. **Capa API**: Esta capa expone los endpoints de la API y maneja las solicitudes HTTP.
2. **Capa BL (Business Logic)**: Contiene la lógica de negocio de la aplicación.
3. **Capa DAL (Data Access Layer)**: Maneja la interacción con la base de datos utilizando ADO.NET y procedimientos almacenados.
4. **Capa Entities**: Contiene las entidades o DTOs (Data Transfer Objects) para el mapeo de datos. Se utiliza mapeo manual con métodos personalizados.
5. **Capa Models**: Contiene los modelos del dominio correspondientes a la base de datos.

## Características

- **Inyección de Dependencias**: Se utiliza para adherirse a los principios SOLID, mejorando la mantenibilidad y escalabilidad del código.
- **FluentValidation**: Se emplea para la validación de datos de entrada, asegurando que los datos sean correctos antes de ser procesados.

## Tecnologías Utilizadas

- **C#**
- **.NET 8**
- **ADO.NET**
- **SQL Server** (Base de datos)
- **Procedimientos Almacenados** (Stored Procedures)
- **Inyección de Dependencias** (Dependency Injection)
- **FluentValidation**

## Endpoints

La API proporciona los siguientes endpoints para gestionar empleados y departamentos:

### Departamentos

- **GET /Departments/Get-All**: Recupera una lista de todos los departamentos.

### Empleados

- **POST /Employees/Create**: Crea un nuevo empleado.
- **PUT /Employees/Update**: Actualiza la información de un empleado existente.
- **DELETE /Employees/Delete/{employeeId}**: Elimina un empleado por su ID.
- **GET /Employees/Get-by-Id/{employeeID}**: Recupera la información de un empleado por su ID.
- **GET /Employees/Get-All**: Recupera una lista de todos los empleados.

## Esquemas

El proyecto incluye los siguientes esquemas:

- **BaseResponseModel**
- **DepartmentEntity**
- **EmployeeCreateEntity**
- **EmployeeEntity**
- **EmployeeListEntity**

