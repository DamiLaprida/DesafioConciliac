CONCILIAC WEB APP
-
**ARQUITECTURA - Domain Driven Design**

Capas involucradas: 

  -Capa de Aplicación(Web APP): donde se encuentra la presentación de la aplicación, las vistas y los controladores.
  
  -Capa de Dominio: donde se encuentra el core del negocio, servicios del dominio, entidades y contratos.
  
  -Capa de Persistencia: donde se encuentra las implementaciones del negocio, repositorios, context, migrations. Todo lo relativo a la comunicación con los datos.
  
  -Tests: se encuentran los test unitarios y de integración de toda la lógica de negocio.

-------------------------------------------
  **TECNOLOGÍAS INVOLUCRADAS:**

  -NET 6

  -C#

  -MVC

  -SQL Server

  -Entity Framework Core

  -Razor Pages

  -Bootstrap

  -API Rest

  -UNIT TEST and Integration Test con XUnit y MOQ

-------------------------------------------
**PASO A PASO PARA EJECUTAR LA APLICACIÓN:**

1) Abrir el archivo script.sql que se encuentra en la ruta \DesafioConciliac\ConciliacDesafio.WebAPP\ConciliacDesafio.Persistence\TableCreationScripts con SQL Server Management Studio.

2) Una vez abierto verá todo el script generado para crear la tabla y la data de la misma.

3) Borre la primera linea del script "USE [Tareas]".

4) Cree una base de datos. Una vez creada en la parte superior izquierda verifique que este seleccionada su base de datos recién creada.

5) Ejecute el script generado.

6) Ya tiene la tabla generada con su data correspondiente.

7) Para ejecutar la solución abra Visual studio y en la parte superior media elija la opción ConciliacDesafio.WebAPP para ejecutar el sitio web.

8) Si desea testear los endpoints por cuenta propia puede elegir la opción IIS Express para ejecutar Swagger.
