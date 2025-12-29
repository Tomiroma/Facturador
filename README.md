# Sistema de Gestión de Cuenta Corriente



Aplicación web para la gestión de facturas, cuenta corriente de clientes y cálculo de débito fiscal (IVA) para un Despachante de Aduana.



## 🚀 Sobre el Proyecto



Este proyecto es un sistema de facturación y gestión diseñado para digitalizar y simplificar la cuenta corriente de clientes. Permite registrar clientes, cargar sus facturas pendientes y marcarlas como pagadas, ofreciendo además un resumen mensual del IVA (Débito Fiscal) generado.



---



## 🛠️ Stack Tecnológico



Este es un proyecto Full-Stack desacoplado, construido con un enfoque profesional.



* **Backend (API):** .NET 8 Web API

* **Frontend (SPA):** React (con Vite y TypeScript)

* **Base de Datos:** SQL Server Express

* **ORM:** Entity Framework Core 8



### 🏛️ Arquitectura



El backend está construido siguiendo los principios de **Arquitectura Limpia (Clean Architecture)**, separando el proyecto en las siguientes capas:



* **`Domain`:** Contiene las entidades de negocio y lógica pura.

* **`Application`:** Contiene los casos de uso y la lógica de la aplicación.

* **`Infrastructure`:** Implementa los detalles técnicos (Base de datos, servicios externos).

* **`Api`:** El punto de entrada (Controladores) que expone la API REST.



---



## 🏁 Cómo Empezar



Para levantar el entorno de desarrollo, necesitarás tener instalados .NET 8 SDK, Node.js y SQL Server Express.



### 1. Backend (.NET API)



```bash

# Ir a la carpeta de la API (ej: Api/)

cd ruta/a/tu/Api



# Instalar dependencias

dotnet restore



# (Si es necesario) Aplicar migraciones de EF Core

dotnet ef database update



# Correr la API

dotnet run
