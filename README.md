# Trabajo Práctico Integrador
## Desarrollo de Software
### Backend

# DSW2025TPI - Backend API para Gestión de Productos y Órdenes

Este proyecto corresponde al Trabajo Práctico Integrador de la materia **Desarrollo de Software**, y consiste en una API RESTful desarrollada con ASP.NET
Core para la gestión de productos y órdenes en una plataforma de e-commerce.

---

## Integrantes del grupo

- **Sosa Leandro Martin** – Legajo: 48397 
- **Chumba Fernando Javier** – Legajo: 53951  
- **Navarro Carolina** – Legajo: 50201

---

## Endpoints de Productos

Ruta base: `/api/products`

| Método | Ruta                  | Descripción                                      |
|--------|-----------------------|--------------------------------------------------|
| GET    | `/api/products`       | Obtiene todos los productos activos              |
| GET    | `/api/products/{id}`  | Obtiene un producto por su ID                    |
| POST   | `/api/products`       | Crea un nuevo producto                           |
| PUT    | `/api/products/{id}`  | Modifica todos los campos de un producto         |
| PATCH  | `/api/products/{id}`  | Cambia el estado de activación (`isActive`)      |

##  Endpoints de Órdenes

Ruta base: `/api/orders`

| Método | Ruta                            | Descripción                                                         |
|--------|----------------------------------|----------------------------------------------------------------------|
| GET    | `/api/orders`                   | Obtiene todas las órdenes registradas                               |
| GET    | `/api/orders/{id}`              | Obtiene una orden con todos sus detalles por su ID                  |
| POST   | `/api/orders`                   | Crea una nueva orden                                                |
| PUT    | `/api/orders/{id}/status`       | Actualiza el estado de una orden (por ejemplo: "Confirmado", etc.)  |


