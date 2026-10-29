# Documento de Diseño Preliminar: Evaluación de Estructuras

## Contexto y Objetivo
El sistema de la biblioteca escolar requiere almacenar títulos que poseen diferente número de ejemplares (desde 1 hasta 5 copias) sin dejar celdas vacías en memoria, además de permitir el ordenamiento histórico por préstamos y búsquedas por ISBN.

---

## Comparación de Estructuras Candidatas

### 1. Arreglo Escalonado (`int[][]` o `object[][]`)
* **Ventajas:**
  * Permite reservar únicamente el tamaño exacto necesario para las copias de cada título, garantizando cero celdas vacías en memoria.
  * Acceso directo mediante índices numéricos.
* **Desventajas:**
  * Al manejar datos de distintos tipos (ISBN como string, cantidad como int, título como string), obliga a usar un arreglo de tipo `object[][]` o múltiples arreglos paralelos.
  * Reduce drásticamente la legibilidad y mantenibilidad del código al carecer de encapsulamiento orientado a objetos.

### 2. Lista de Arreglos (`List<int[]>`)
* **Ventajas:**
  * Su naturaleza dinámica facilita agregar nuevos títulos a la colección sin redimensionar manualmente el contenedor principal.
  * Permite que cada elemento contenga un arreglo de tamaño personalizado para sus ejemplares.
* **Desventajas:**
  * No permite encapsular los atributos propios del libro (Título, ISBN, Préstamos Históricos) dentro del mismo elemento.
  * Requiere estructuras paralelas complejas para asociar la información textual con las matrices numéricas.

### 3. Lista de Objetos Clase `Título` (`List<Título>`) con arreglo interno
* **Ventajas:**
  * Sigue los principios de la Programación Orientada a Objetos (POO), agrupando en una sola entidad atributos como `ISBN`, `Nombre`, `PrestamosHistoricos` y un arreglo interno `Ejemplares[]`.
  * Permite que el arreglo interno `Ejemplares[]` de cada objeto tenga la dimensión exacta de sus copias disponibles, satisfaciendo el requisito de no dejar celdas vacías.
  * Simplifica los algoritmos de ordenamiento por inserción y búsqueda binaria, al permitir comparar objetos directamente por sus propiedades.
* **Desventajas:**
  * Requiere una pequeña sobrecarga de memoria por la instanciación de objetos, la cual es insignificante comparada con los beneficios de diseño y legibilidad.

---

## Decisión de Diseño
Se selecciona la opción **`List<Título>`** (o un arreglo de objetos `Título[]` con arreglo interno para ejemplares). Esta estructura satisface la restricción estricta de no dejar celdas vacías en memoria, mientras mantiene un código limpio, legible y modular para la implementación manual del ordenamiento por inserción y la búsqueda binaria.
